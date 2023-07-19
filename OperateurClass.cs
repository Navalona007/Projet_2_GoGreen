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
        public int id_op { get; set; } //used by rakoto
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

        

        public static List<OperateurClass> list_oper = new List<OperateurClass>();
        //public static Dictionary<String, OperateurClass> list_pass_operateur = new Dictionary<string, OperateurClass>();
        public static Dictionary<String, OperateurClass> list_login_operateur = new Dictionary<string, OperateurClass>();


        public OperateurClass(string id, string name, string lastname, string email, string mobile, string workplace, string statut)
        {
            this.id = id;
            this.name = name;
            this.lastname = lastname;
            this.email = email;
            this.mobile = mobile;
            this.workplace = workplace;
            this.statut = statut;
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



        public int getId_op()
        {
            return id_op;
        }

        public void setId_op(int id_op)
        {
            this.id_op = id_op;
        }

        public string get_Name_Oper()
        {
            return name;
        }
        public void set_Name_Oper(string nom)
        {
            name = nom;
        }
        public string get_Lastname_Oper()
        {
            return lastname;
        }
        public void set_Lastname_Oper(string prenom)
        {
            lastname = prenom;
        }
        public string get_Workplace_Oper()
        {
            return workplace;
        }
        public void set_Workplace_Oper(string lieu)
        {
            workplace = lieu;
        }
        public string get_Email_Oper()
        {
            return email;
        }
        public void set_Email_Oper(string mail)
        {
            email = mail;
        }
        public string get_Mobile_Oper()
        {
            return mobile;
        }
        public void set_Mobile_Oper(string mobile)
        {
            this.mobile = mobile;
        }
        //public string get_Status_Oper()
        //{
        //    return status;
        //}
        //public void set_Status_Oper(string statut)
        //{
        //    this.status = statut;
        //}
        public List<OperateurClass> getList_oper()
        {
            liste_operateur_fromDB();
            return list_oper;

        }


        private void liste_operateur_fromDB()
        {
            ConnectDB conx = new ConnectDB();
            list_oper.Clear();
            list_login_operateur.Clear();

            conx.launchReader("Select opérateur_de_saisi.id, opérateur_de_saisi.nom_oper, opérateur_de_saisi.prenom_oper, opérateur_de_saisi.mail_oper,"+
                " opérateur_de_saisi.mobile_oper, lieu_travail.name_lieu, statut_opérateur.satus from opérateur_de_saisi"+
                " join lieu_travail on opérateur_de_saisi.lieu_travailid = lieu_travail.id "+
                "join statut_opérateur on opérateur_de_saisi.statut_opérateurid = statut_opérateur.id;");

            while (conx.reader.Read())
            {


              int id_oper = (int)conx.reader["id"];//used by samira
                string nom = (string)conx.reader["nom_oper"];
                string prenom = (string)conx.reader["prenom_oper"];
                string mail = (string)conx.reader["mail_oper"];
                string mobile = (string)conx.reader["mobile_oper"];
                string lieu = (string)conx.reader["name_lieu"];
                string state = (string)conx.reader["satus"];


                OperateurClass oper = new OperateurClass(id, nom, prenom, mail, mobile, lieu, statut) ;

                list_oper.Add(oper);
                list_login_operateur.Add(mail, oper);
            }
            conx.close();
        }


        // Code for creating DataGridTextColumns

        public static ObservableCollection<OperateurClass> GetOperateurs()
        {
            ObservableCollection<OperateurClass> listeOperateurs = new ObservableCollection<OperateurClass>();

            // Retrieve data from your data source
            // For example, fetch data from list_operateur or list_pass_operateur dictionaries

            foreach (var operateur in listeOperateurs)
            {
                listeOperateurs.Add(operateur);
            }

            return listeOperateurs;
        }

        public void insert_operateur_db(OperateurClass oper)
        {
            ConnectDB conx = new ConnectDB();
            oper = new OperateurClass();
            conx.executeRequest("INSERT INTO public.opérateur_de_saisi(statut_opérateurid, lieu_travailid, nom_oper, prenom_oper, mail_oper, pass_oper, mobile_oper)"+
                " VALUES('', '', '', '', '', '', ''); ");
            string lieu = (string)conx.reader["name_lieu"];
            
            conx.close();
        }

        public string toString()
        {
            string oper = name + " " + lastname + " " + workplace + " " + email + " " + mobile + " " + statut;
            oper.ToLower();
            return oper;
        }
    }
}
