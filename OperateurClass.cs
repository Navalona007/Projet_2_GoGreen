//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Projet_2_GoGreen
//{
//    internal class OperateurClass
//    {
//        private string name;
//        private string lastname;
//        private string workplace;
//        private string email;
//        private string password;
//        private string mobile;

//        ConnectDB conx = new ConnectDB();

//        public static List<OperateurClass> list_operateur = new List<OperateurClass>();
//        public static Dictionary<String, OperateurClass> list_pass_operateur = new Dictionary<string, OperateurClass>();
//        public static Dictionary<String, OperateurClass> list_login_operateur = new Dictionary<string, OperateurClass>();

//        public OperateurClass(string name, string lastname, string email, string password, string workplace)
//        {
//            this.name = name;
//            this.lastname = lastname;
//            this.email = email;
//            this.password = password;
//            this.workplace = workplace;

//        }
//        public string getWorkplace()
//        {
//            return workplace;
//        }
//        public void setWorkplace(string value)
//        {
//            this.workplace = value;
//        }
//            public string getName()
//        {
//            return name;
//        }

//            public void setName (string value)
//        {
//            this.name = value;
//        }
//        public string getLastname()
//        {
//            return lastname;
//        }

//        public void setLastname(string value)
//        {
//            this.lastname = value;
//        }
//        public string getEmail()
//        {
//            return email;
//        }

//        public void setEmail(string value)
//        {
//            this.email = value;
//        }
//        public string getPassword()
//        {
//            return password;
//        }

//        public void setPassword(string value)
//        {
//            this.password = value;
//        }

//        public OperateurClass()
//        {

//        }

//        private void liste_operateur()
//        {
//            var cmd = conx.createRequest("Select * from opérateur_de_saisi;");
//            var read = cmd.ExecuteReader();

//            while (read.Read())
//            {
//                string mail = (string)read["mail_oper"];
//                string pass = (string)read["pass_oper"];
//                string nom = (string)read["nom_oper"];
//                string prenom = (string)read["prenom_oper"];
//                string workplace = (string)read["mobile_oper"];
//                list_pass_operateur.Add(pass, new OperateurClass(nom, prenom, mail, pass, workplace));
//                list_login_operateur.Add(mail, new OperateurClass(nom, prenom, mail, pass, workplace));
//            }
//        }

//        //private void lecture_ecriture()
//        //{


//        //    String query = "SELECT * FROM opérateur_de_saisi";

//        //    var cmd = conx.createRequest(query);
//        //    var reader = cmd.ExecuteReader();

//        //    //List<OperateurClass> listeOperateurs = new List<OperateurClass>();
//        //    ObservableCollection<OperateurClass> listeOperateurs = new ObservableCollection<OperateurClass>();

//        //    //NpgsqlDataReader reader = command.ExecuteReader();
//        //    while (reader.Read())
//        //    {
//        //        OperateurClass operateur = new OperateurClass();

//        //        BindingList<OperateurClass> bindingList = new BindingList<OperateurClass>(listeOperateurs);
//        //        grid_oper.DataSource = bindingList;

//        //        operateur.setName(reader.GetString(reader.GetOrdinal("nom_oper")));
//        //        operateur.setLastname(reader.GetString(reader.GetOrdinal("prenom_oper")));
//        //        operateur.setEmail(reader.GetString(reader.GetOrdinal("mail_oper")));
//        //        operateur.setWorkplace(reader.GetString(reader.GetOrdinal("mobile_oper")));

//        //        listeOperateurs.Add(operateur);

//        //    }
//        //    grid_oper.ItemsSource(listeOperateurs);
//        //    reader.Close();
//        //}
//    }
//}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2_GoGreen
{
    internal class OperateurClass
    {
        public string id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string workplace { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string statut { get; set; }

        public void setWorkplace(string v)
        {
            workplace = v;
        }

        public void setStatut(string v)
        {
            statut = v;
        }

        ConnectDB conx = new ConnectDB();

        public static List<OperateurClass> list_operateur = new List<OperateurClass>();
        public static Dictionary<String, OperateurClass> list_pass_operateur = new Dictionary<string, OperateurClass>();
        public static Dictionary<String, OperateurClass> list_login_operateur = new Dictionary<string, OperateurClass>();

        public OperateurClass(string id, string name, string lastname, string email, string password, string workplace)
        {
            this.name = name;
            this.lastname = lastname;
            this.email = email;
           
            this.workplace = workplace;
        }

        public string getWorkplace()
        {
            return workplace;
        }

        public void setMobile(string value)
        {
            mobile = value;
        }

        public string getName()
        {
            return name;
        }

        public string getId()
        {
            return id;
        }

        public void setId(string value)
        {
            id = value;
        }

        public void setName(string value)
        {
            name = value;
        }

        public string getLastname()
        {
            return lastname;
        }

        public void setLastname(string value)
        {
            lastname = value;
        }

        public string getEmail()
        {
            return email;
        }

        public void setEmail(string value)
        {
            email = value;
        }


        public OperateurClass()
        {

        }

        private void liste_operateur()
        {
            var cmd = conx.createRequest("Select * from opérateur_de_saisi;");
            var read = cmd.ExecuteReader();

            while (read.Read())
            {
                string mail = (string)read["mail_oper"];
                string pass = (string)read["pass_oper"];
                string nom = (string)read["nom_oper"];
                string prenom = (string)read["prenom_oper"];
                string workplace = (string)read["mobile_oper"];
                list_pass_operateur.Add(pass, new OperateurClass(id,nom, prenom, mail, pass, workplace));
                list_login_operateur.Add(mail, new OperateurClass(id,nom, prenom, mail, pass, workplace));
            }
        }

        // Code for creating DataGridTextColumns

        public static ObservableCollection<OperateurClass> GetOperateurs()
        {
            ObservableCollection<OperateurClass> listeOperateurs = new ObservableCollection<OperateurClass>();

            // Retrieve data from your data source
            // For example, fetch data from list_operateur or list_pass_operateur dictionaries

            foreach (var operateur in list_operateur)
            {
                listeOperateurs.Add(operateur);
            }

            return listeOperateurs;
        }
    }
}
