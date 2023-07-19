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
            ConnectDB conx = new ConnectDB();
            conx.launchReader("SELECT * FROM client_table;");

            while (conx.reader.Read())
            {
                string id = conx.reader["id"].ToString();
                string nom = (string)conx.reader["nom_client"];
                string prenom = (string)conx.reader["prenom_client"];
                string mail = (string)conx.reader["mail_client"];
                string pass = (string)conx.reader["pass_client"];
                string adresse = (string)conx.reader["adresse_client"];
                DateTime dateInscription = (DateTime)conx.reader["date_inscrip"];
                string mobile = (string)conx.reader["mobile_client"];
                string status = (string)conx.reader["status_client"];

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

            conx.reader.Close();
            conx.close();
        }

        public string toString()
        {
            string client = name +" "+lastname+" "+ adress +" "+email+" "+dateInscription+" "+mobile+" "+status;
            client.ToLower();
            return client;
        }
    }
}
