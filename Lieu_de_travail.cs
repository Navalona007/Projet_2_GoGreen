using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet_2_GoGreen
{
    internal class Lieu_de_travail
    {
        private string nom_lieu;
        private int id_lieu;

        ConnectDB conx = new ConnectDB();

        public static Dictionary<string, Lieu_de_travail> list_lieu = new Dictionary<string, Lieu_de_travail>();

        public Lieu_de_travail(int id_lieu, string nom_lieu)
        {
            this.id_lieu = id_lieu;
            this.nom_lieu = nom_lieu;
        }

        public int getIdlieu()
        {
            return id_lieu;
        }
        public string getNomlieu()
        {
            return nom_lieu;
        }



        public void selectLieu()
        {
            list_lieu.Clear();
            conx.launchReader("SELECT * FROM public.lieu_travail");

            while (conx.reader.Read())
            {
                int id = (int)conx.reader["id"];
                string lieu = (string)conx.reader["id"];
                Lieu_de_travail workplace = new Lieu_de_travail(id, lieu);
                list_lieu.Add(lieu, workplace);
            }
        }

        public void insertDB_lieu(string lieu)
        {
            try
            {
                conx.executeRequest("INSERT INTO public.lieu_travail(name_lieu)	VALUES ('" + lieu + "');");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de creation de lieu de travail", "ERREUR", MessageBoxButton.OK);
            }
        }

        public void createLieu(string lieu)
        {
            Lieu_de_travail workplace = null;
            if (list_lieu.ContainsKey(lieu))
            {
                MessageBox.Show("Lieu de travail", "ERREUR", MessageBoxButton.OK);
            }
            else
            {
                insertDB_lieu(lieu);
            }


        }
    }
}
