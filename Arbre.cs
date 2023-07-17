using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2_GoGreen
{
    internal class Arbre
    {
        public string id_arbre { get; set; }
        public DateTime date_plantation { get; set; }
        public DateTime date_creation { get; set; }
        public DateTime date_mise_a_jour { get; set; }
        public string statut { get; set; }
        public string diametre { get; set; }
        public string hauteur { get; set; }
        public string etat_de_feuillage { get; set; }
        public string espece { get; set; }
        public string type { get; set; }
        public string zone { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public Arbre(string id_arbre, DateTime date_plantation, DateTime date_creation, DateTime date_mise_a_jour, string statut, string diametre, string hauteur, string etat_de_feuillage, string espece,
            string type, string zone, string latitude, string longitude)
        {
            this.id_arbre = id_arbre;
            this.date_plantation = date_plantation;
            this.date_creation = date_creation;
            this.date_mise_a_jour = date_mise_a_jour;
            this.statut = statut;
            this.diametre = diametre;
            this.hauteur = hauteur;
            this.etat_de_feuillage = etat_de_feuillage;
            this.espece = espece;
            this.type = type;
            this.zone = zone;
            this.latitude = latitude;
            this.longitude = longitude;

        }
        public Arbre ()
        {

        }


    }
}
