using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for Authentification.xaml
    /// </summary>
    public partial class Authentification : Window
    {

      
        public Authentification()
        {
            InitializeComponent();
        }

        public static NpgsqlConnection GetConnection()
        {
            //return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=gg_db");
            return new NpgsqlConnection("Host=localhost;Database=gg_db;Username=postgres;Password=root");
        }
        

        private void bt_connect_Click(object sender, RoutedEventArgs e)
        {
            string hash_password = CalculateMD5Hash(pwd_auth.Password);
            if (!string.IsNullOrEmpty(tb_login.Text) || !string.IsNullOrEmpty(pwd_auth.Password))
            {
                var conn = GetConnection();
                {
                    conn.Open();

                    // Vérifier si l'utilisateur est un administrateur
                    using (NpgsqlCommand adminCmd = new NpgsqlCommand("SELECT mail_admin, pass_admin FROM administrateur WHERE mail_admin = '" + tb_login.Text + "' AND pass_admin = '" + hash_password + "'", conn))
                    {
                        adminCmd.Parameters.AddWithValue("@mail_admin", tb_login.Text);
                        adminCmd.Parameters.AddWithValue("@pass_admin", hash_password);

                        using (var adminReader = adminCmd.ExecuteReader())
                        {
                            if (adminReader.Read())
                            {
                                Administrateur administrateur = new Administrateur();
                                administrateur.Show();
                                //MessageBox.Show("Mety", "Ok");
                                tb_login.Text = "";
                                pwd_auth.Password = "";
                                return;
                            }
                        }
                    }

                    // Vérifier si l'utilisateur est un client
                    using (NpgsqlCommand ClientCmd = new NpgsqlCommand("SELECT mail_client, pass_client FROM client WHERE mail_client = '" + tb_login.Text + "' AND pass_client = '" + pwd_auth.Password + "'", conn))
                    {
                        ClientCmd.Parameters.AddWithValue("@mail_client", tb_login.Text);
                        ClientCmd.Parameters.AddWithValue("@pass_client", pwd_auth.Password);

                        using (var clientReader = ClientCmd.ExecuteReader())
                        {
                            if (clientReader.Read())
                            {
                                //Administrateur administrateur = new Administrateur();
                                //administrateur.Show();
                                MessageBox.Show("Bienvenu sur votre plateforme", "Reussi");
                                tb_login.Text = "";
                                pwd_auth.Password = "";
                                return;
                            }
                        }
                    }
                    // Si aucun utilisateur correspondant n'est trouvé
                    MessageBox.Show("Votre identifiant et mot de passe ne correspondent pas. Réessayez", "Erreur");
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs de connexion", "Erreur");
            }
        }
        //private static List


        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tb_login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // Fonction pour calculer le hachage MD5 d'une chaîne de caractères
        static string CalculateMD5Hash(string input)
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

    }
}