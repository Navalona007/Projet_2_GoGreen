using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2_GoGreen
{
    internal class Etat_arbre
    {
        public string zone { get; set; }
        public string date_mis_a_jour { get; set; }
        public string type { get; set; }
        public float hauteur { get; set; }
        public float diametre_tronc { get; set; }
        public string etat_feuillage { get; set; }

        public double latitude { get; set; }

        public void setLatitude (float value)
        {
            latitude = value;
        }

        public double longitude { get; set; }

        public Etat_arbre(string zone, string type, float latitude, float longitude)
        {
            this.zone = zone;
            this.type = type;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public void setLongitude(float value)
        {
            longitude = value;
        }

        public void setZone(string value)
        {
            zone = value;
        }

        public void setDate_maj(string value)
        {
            date_mis_a_jour = value;
        }

        public void setType(string value)
        {
            type = value;
        }

        public void setHauteur(float value)
        {
            hauteur = value;
        }

        public void setDiametre(float value)
        {
            diametre_tronc = value;
        }

        //public void setEtat_feuillage(float value)
        //{
        //    etat_feuillage = value;
        //}

        public Etat_arbre()
        {

        }


    }
}
