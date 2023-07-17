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
        public NpgsqlCommand cmd;
        public NpgsqlDataReader read;
        
               
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

        public NpgsqlCommand getCmd()
        {
            return cmd;
        }
        public NpgsqlDataReader getRead()
        {
            if (read == null)
                return null;
            return read;
        }
        public NpgsqlConnection getConx()
        {
            return conx;
        }

        public void executeRequest(string request)
        {
            cmd = new NpgsqlCommand(request, conx);
            cmd.ExecuteNonQuery();

        }

        public void launchReader(string request)
        {
            cmd = new NpgsqlCommand(request, conx); 
            read = cmd.ExecuteReader();
        }

        public void close()
        {
            conx.Close();
        }
            
    }
}
