using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projet_2_GoGreen
{
    internal class OperateurClass : INotifyPropertyChanged
    {
        public int id_op { get; set; } 
        public string name;
        public string lastname;
        public string workplace;
        public string email;
        //private string password;
        public string mobile;
        private string statut;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Lastname
        {
            get { return lastname; }
            set
            {
                if (lastname != value)
                {
                    lastname = value;
                    OnPropertyChanged(nameof(Lastname));
                }
            }
        }

        //public string Workplace
        //{
        //    get { return workplace; }
        //    set
        //    {
        //        if (workplace != value)
        //        {
        //            workplace = value;
        //            OnPropertyChanged(nameof(Workplace));
        //        }
        //    }
        //}

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Mobile
        {
            get { return mobile; }
            set
            {
                if (mobile != value)
                {
                    mobile = value;
                    OnPropertyChanged(nameof(Mobile));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ConnectDB conx = new ConnectDB();

        public static List<OperateurClass> list_operateur = new List<OperateurClass>();
        public static Dictionary<String, OperateurClass> list_pass_operateur = new Dictionary<string, OperateurClass>();
        public static Dictionary<String, OperateurClass> list_login_operateur = new Dictionary<string, OperateurClass>();

        public OperateurClass(string name, string lastname, string email, string password, string workplace)
        {
            this.Name = name;
            this.Lastname = lastname;
            this.Email = email;

            this.id_op = id_op;
        }


        public void setMobile(string value)
        {
            Mobile = value;
        }

        public string getName()
        {
            return Name;
        }

        public void setName(string value)
        {
            Name = value;
        }

        public string getLastname()
        {
            return Lastname;
        }

        public void setLastname(string value)
        {
            Lastname = value;
        }

        public string getEmail()
        {
            return Email;
        }

        public void setEmail(string value)
        {
            Email = value;
        }

        public int getId_op()
        {
            return id_op;
        }

        public void setId_op(int id_op)
        {
            this.id_op = id_op;
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
                int id_op = (int)read["id"];
                string mail = (string)read["mail_oper"];
                string pass = (string)read["pass_oper"];
                string nom = (string)read["nom_oper"];
                string prenom = (string)read["prenom_oper"];
                string workplace = (string)read["mobile_oper"];
                list_pass_operateur.Add(pass, new OperateurClass(nom, prenom, mail, pass, workplace));
                list_login_operateur.Add(mail, new OperateurClass(nom, prenom, mail, pass, workplace));
            }
        }

        // Code for creating DataGridTextColumns

        public static ObservableCollection<OperateurClass> GetOperateurs()
        {
            ObservableCollection<OperateurClass> listeOperateurs = new ObservableCollection<OperateurClass>();

            

            foreach (var operateur in list_operateur)
            {
                listeOperateurs.Add(operateur);
            }

            return listeOperateurs;
        }
    }
}
