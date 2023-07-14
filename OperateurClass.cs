using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2_GoGreen
{
    internal class OperateurClass
    {
        private int id_oper;
        private string name;
        private string lastname;
        private string workplace;
        private string email;
        private string mobile;
        private string status;

        ConnectDB conx = new ConnectDB();

        public static List<OperateurClass> list_oper = new List<OperateurClass>();
        //public static Dictionary<String, OperateurClass> list_pass_operateur = new Dictionary<string, OperateurClass>();
        public static Dictionary<String, OperateurClass> list_login_operateur = new Dictionary<string, OperateurClass>();

        public OperateurClass(int id, string name, string lastname, string email, string mobile, string workplace, string status)
        {
            id_oper = id;
            this.name = name;
            this.lastname = lastname;
            this.email = email;
            this.mobile = mobile;
            this.workplace = workplace;
        }
        public OperateurClass()
        {

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
        public string get_Status_Oper()
        {
            return status;
        }
        public void set_Status_Oper(string statut)
        {
            this.status = statut;
        }
        public List<OperateurClass> getList_oper()
        {
            liste_operateur_fromDB();
            return list_oper;

        }


        private void liste_operateur_fromDB()
        {
            list_oper.Clear();
            list_login_operateur.Clear();

            conx.launchReader("Select opérateur_de_saisi.id, opérateur_de_saisi.nom_oper, opérateur_de_saisi.prenom_oper, opérateur_de_saisi.mail_oper,"+
                " opérateur_de_saisi.mobile_oper, lieu_travail.name_lieu, statut_opérateur.satus from opérateur_de_saisi"+
                " join lieu_travail on opérateur_de_saisi.lieu_travailid = lieu_travail.id "+
                "join statut_opérateur on opérateur_de_saisi.statut_opérateurid = statut_opérateur.id;");

            while (conx.read.Read())
            {
                int id_oper = (int)conx.read["id"];
                string nom = (string)conx.read["nom_oper"];
                string prenom = (string)conx.read["prenom_oper"];
                string mail = (string)conx.read["mail_oper"];
                string mobile = (string)conx.read["mobile_oper"];
                string lieu = (string)conx.read["name_lieu"];
                string state = (string)conx.read["satus"];

                OperateurClass oper = new OperateurClass(id_oper, nom, prenom, mail, mobile, lieu, state) ;
                list_oper.Add(oper);
                list_login_operateur.Add(mail, oper);
            }
        }

        public void insert_operateur_db(OperateurClass oper)
        {
            oper = new OperateurClass();
            conx.executeRequest("INSERT INTO public.opérateur_de_saisi(statut_opérateurid, lieu_travailid, nom_oper, prenom_oper, mail_oper, pass_oper, mobile_oper)"+
                " VALUES('', '', '', '', '', '', ''); ");
            string lieu = (string)conx.read["name_lieu"];
            //return lieu;
        }
    }
}
