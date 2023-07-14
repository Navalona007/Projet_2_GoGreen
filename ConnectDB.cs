using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Projet_2_GoGreen
{
    internal class ConnectDB
    {
        NpgsqlConnection conx;
               
        public ConnectDB()
        {
            try
            {
                conx = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;");
                conx.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

        public NpgsqlCommand createRequest(string reques)
        {
            var cmd = new NpgsqlCommand(reques, conx);
            return cmd;
        }

        public NpgsqlDataReader createReader(NpgsqlCommand cmd)
        {
            NpgsqlDataReader read = cmd.ExecuteReader();
            return read;
        }
            
    }
}
