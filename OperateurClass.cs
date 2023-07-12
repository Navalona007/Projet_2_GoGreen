using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2_GoGreen
{
    internal class OperateurClass
    {
        private string name;
        private string lastname;
        //private string workplace;
        private string email;
        private string password;
        private string mobile;

        ConnectDB conx = new ConnectDB();

        public static List<OperateurClass> list_operateur = new List<OperateurClass>();
        public static Dictionary<String, OperateurClass> list_pass_operateur = new Dictionary<string, OperateurClass>();
        public static Dictionary<String, OperateurClass> list_login_operateur = new Dictionary<string, OperateurClass>();

        public OperateurClass(String name, string lastname, string email, string password)
        {
            this.name = name;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
            //this.workplace = workplace;
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
                //string 
                list_pass_operateur.Add(pass, new OperateurClass(nom, prenom, mail, pass));
                list_login_operateur.Add(mail, new OperateurClass(nom, prenom, mail, pass));
            }
        }
    }
}
