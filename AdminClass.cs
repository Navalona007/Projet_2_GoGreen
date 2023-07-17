using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Projet_2_GoGreen
{
    internal class AdminClass
    {
        public string id { get; set; }
        private string name;
        private string lastname;
        private string email;
        private string mobile;
        private string password;
        private Reference_entreprise reference;
        ConnectDB conx = new ConnectDB();

        public static List<AdminClass> list_admin = new List<AdminClass>();
        public static Dictionary<String, AdminClass> list_pass_admin = new Dictionary<string, AdminClass>();
        public static Dictionary<String,AdminClass> list_login_admin = new Dictionary<string, AdminClass>();

        public AdminClass(String name, string lastname, string email, string password)
        {
            this.name = name;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
            //reference.createReference();
            
        }
        public AdminClass()
        {

        }
        

        public string getName()
        {
            return name;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public object getList_pass_admin()
        {
            if (list_pass_admin == null)
            {
                return list_pass_admin;
            }
                
            else
                return null;
        }
        public object getList_login_admin()
        {
            if (list_login_admin == null)
            {
                return list_login_admin;
            }

            else
                return null;
        }

        private void liste_admin()
        {
            //conx.executeRequest("Select * from administateur;");
            conx.launchReader("Select * from administateur;");

            while (conx.read.Read())
            {
                string mail = (string)conx.read["mail_admin"];
                string pass = (string)conx.read["pass_admin"];
                string nom = (string)conx.read["nom_admin"];
                string prenom = (string)conx.read["prenom_admin"];
                //string 
                list_pass_admin.Add(pass, new AdminClass(nom, prenom, mail, pass));
                list_login_admin.Add(mail, new AdminClass(nom, prenom, mail, pass));
            }
        }

        private AdminClass createAdmin(String name, string lastname, string email, string password)
        {
            AdminClass admin = null;
            if (!list_login_admin.ContainsKey(email) && !list_pass_admin.ContainsKey(password))
            {
                admin = new AdminClass(name, lastname, email, password);
                list_login_admin.Add(email, admin);
                list_pass_admin.Add(password, admin);
            }
            else
            {
                MessageBox.Show("Erreur d'inscription!! \nMerci d'assurer la conformité de vos valeurs", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return admin;
        }

    }
}
