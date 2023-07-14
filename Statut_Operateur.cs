using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2_GoGreen
{
    internal class Statut_Operateur
    {
        private string status;
        private DateTime date;
        private int id;

        public Statut_Operateur()
        {

        }
        public string getStatus()
        {
            return status;
        }
        public void setStatus(string v)
        {
            this.status = v;
        }
        public int getId()
        {
            return id;
        }
        public void setId(int v)
        {
            id = v;
        }
        public DateTime getDate()
        {
            return date;
        }
        public void setDate(DateTime d)
        {
            date = d;
        }
    }
}
