using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2_GoGreen
{
    internal class ClientClass
    {
        private string name;
        private string lastname;
        private string email;
        private string password;
        private string mobile;
        private int key_ref;

        private Reference_entreprise reference;
        ConnectDB conx = new ConnectDB();

        public static List<ClientClass> list_client = new List<ClientClass>();
        public static Dictionary<String, ClientClass> list_pass_client = new Dictionary<string, ClientClass>();
        public static Dictionary<String, ClientClass> list_login_client = new Dictionary<string, ClientClass>();

        public ClientClass(String name, string lastname, string email, string password)
        {
            this.name = name;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
            //reference.createReference();

        }
        public ClientClass()
        {

        }

        private void liste_client()
        {
            var cmd = conx.createRequest("Select * from administateur;");
            var read = cmd.ExecuteReader();

            while (read.Read())
            {
                string mail = (string)read["mail_client"];
                string pass = (string)read["pass_client"];
                string nom = (string)read["nom_client"];
                string prenom = (string)read["prenom_clients"];
                //string 
                list_pass_client.Add(pass, new ClientClass(nom, prenom, mail, pass));
                list_login_client.Add(mail, new ClientClass(nom, prenom, mail, pass));
            }
        }
    }
}
