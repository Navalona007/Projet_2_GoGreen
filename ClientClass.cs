using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2_GoGreen
{
    internal class ClientClass
    {
     
        public string id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string adress { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime dateInscription { get; set; }
        public string mobile { get; set; }
        public string status { get; set; }

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
        }
            //reference.createReference();

         public ClientClass(String name, string lastname, string email, string password, string status)
            {
                this.name = name;
                this.lastname = lastname;
                this.email = email;
                this.password = password;
                this.status = status;

            }
        public ClientClass()
        {

        }

        private void liste_client()
        {
            //var cmd = conx.createRequest("SELECT * FROM client_table;");
            //var read = cmd.ExecuteReader();

            while (conx.read.Read())
            {
                string id = conx.read["id"].ToString();
                string nom = (string)conx.read["nom_client"];
                string prenom = (string)conx.read["prenom_client"];
                string mail = (string)conx.read["mail_client"];
                string pass = (string)conx.read["pass_client"];
                string adresse = (string)conx.read["adresse_client"];
                DateTime dateInscription = (DateTime)conx.read["date_inscrip"];
                string mobile = (string)conx.read["mobile_client"];
                string status = (string)conx.read["status_client"];

                list_client.Add(new ClientClass
                {
                    id = id,
                    name = nom,
                    lastname = prenom,
                    email = mail,
                    password = pass,
                    adress = adresse,
                    dateInscription = dateInscription,
                    mobile = mobile,
                    status = status
                });
                list_pass_client.Add(password, new ClientClass(nom, prenom, mail, pass));
                list_login_client.Add(email, new ClientClass(nom, prenom, mail, pass));
            }

            conx.read.Close();
        }
    }
}
