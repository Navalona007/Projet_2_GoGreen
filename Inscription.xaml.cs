using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Projet_2_GoGreen
{
    /// <summary>
    /// Interaction logic for Inscription.xaml
    /// </summary>
    public partial class Inscription : Window
    {
        NpgsqlConnection conx;
        public Inscription()
        {
            InitializeComponent();
            connectDB();
        }

        private void connectDB()
        {
            conx = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=admin13;Database=gg_db;");
            conx.Open();
            if (conx.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("database connect success");
            }
            else
            {
                Console.WriteLine("echec connection database");
            }
        }

        public void addInscription()
        {
            string name = tb_nom_inscription.Text;
            string lastname = tb_prenom_inscription.Text;
            string mail = tb_mail_inscription.Text;
            string mdp = tb_mdp_inscription.Text;
            string confirm_mdp = tb_confirmer_mdp_inscription.Text;
            string reference = tb_reference_inscription.Text;
            string hashedPassword = "";

            if(reference != null)
            {
                connectDB();
                int i = 1;
                var cmd = conx.CreateCommand();
                //var res = cmd.AllResultTypesAreUnknown;
                var res = new List<String>();
                cmd.CommandText = "SELECT count(*) FROM public.reference_entreprise ;";
                
                NpgsqlDataReader readcount = cmd.ExecuteReader();
                
                //int count = readcount.; 
                do
                {
                    cmd.CommandText = "SELECT label_ref 	FROM public.reference_entreprise Where id=" + i + ";";
                    NpgsqlDataReader reader =  cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                    
                    i++;
                } while (i<3);
                
            }

            if (mdp == confirm_mdp)
            {
                hashedPassword = (string)CalculateMD5Hash(mdp);
                Console.Out.WriteLine("Mot de passe haché en MD5 : " + hashedPassword);
            }
            else
            {
                Console.Out.WriteLine("erreur mot de passe");
            }


            if ((bool)rb_admin.IsChecked)
            {
                try
                {
                    connectDB();

                    string requ = @"INSERT INTO public.administateur(reference_entrepriseid, nom_admin, prenom_admin, mail_admin, pass_admin)"
                                + "VALUES( ' 1', '" + name + "', '" + lastname + "', '" + mail + "', '" + hashedPassword + "'); ";
                    var cmd = conx.CreateCommand();
                    cmd.CommandText = requ;
                    cmd.ExecuteNonQuery();
                    conx.Wait();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        // Fonction pour calculer le hachage MD5 d'une chaîne de caractères
        private string CalculateMD5Hash(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        } //end CalculateMD5Hash
        private void isExist()
        {

        }
        private void insertReference()
        {
            string reference = tb_reference_inscription.Text ;

            try
            {
                connectDB();
                var cmd = conx.CreateCommand();
                cmd.CommandText = @"INSERT INTO public.reference_entreprise(label_ref) VALUES('"+reference+"'); ";
                cmd.ExecuteNonQuery();
                conx.Wait();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }
        private void insertAdmin()
        {

        }
    }
}
