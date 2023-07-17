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
            tb_login.GotFocus += tb_login_GotFocus;
            tb_login.Foreground = Brushes.LightGray;
        }

        private void bt_connect_Click(object sender, RoutedEventArgs e)
        {

            string mdp = CalculateMD5Hash(pwd_auth.Password);

            if (!string.IsNullOrEmpty(tb_login.Text) || !string.IsNullOrEmpty(mdp))
            {
                using (NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=gg_db;Username=postgres;Password=root"))
                {
                    conn.Open();

                    // Vérifier si l'utilisateur est un administrateur // remaque sur pwd_auth
                    using (NpgsqlCommand adminCmd = new NpgsqlCommand("SELECT administrateur.id, nom_admin, mail_admin, pass_admin, label_ref FROM administrateur JOIN reference_entreprise ON administrateur.reference_entrepriseid=reference_entreprise.id WHERE mail_admin = '" + tb_login.Text + "' AND pass_admin = '" + mdp + "'", conn))
                    {
                        adminCmd.Parameters.AddWithValue("@mail_admin", tb_login.Text);
                        adminCmd.Parameters.AddWithValue("@pass_admin", mdp);
                        using (var adminReader = adminCmd.ExecuteReader())
                        {
                            if (adminReader.Read())
                            {
                                Administrateur administrateur = new Administrateur();
                                administrateur.lb_badge_admin.Content = adminReader.GetString(adminReader.GetOrdinal("nom_admin")) + " de " + adminReader.GetString(adminReader.GetOrdinal("label_ref"));
                                administrateur.Show();
                                this.Hide();

                                tb_login.Text = "";
                                pwd_auth.Password = "";
                                //return;
                            }
                        }
                    }

                    // Vérifier si l'utilisateur est un client
                    using (NpgsqlCommand ClientCmd = new NpgsqlCommand("SELECT client.id, nom_client, prenom_client, mail_client, pass_client, status_client FROM client WHERE mail_client = '" + tb_login.Text + "' AND pass_client = '" + mdp + "'", conn))
                    {
                        ClientCmd.Parameters.AddWithValue("@mail_client", tb_login.Text);
                        ClientCmd.Parameters.AddWithValue("@pass_client", mdp);

                        using (var clientReader = ClientCmd.ExecuteReader())
                        {
                            while (clientReader.Read())
                            {


                                if (clientReader.Read() && clientReader.GetString(clientReader.GetOrdinal("status_client")) == "actif")
                                {
                                    Acceuil_client acc = new Acceuil_client();
                                    acc.nombre_arbre(clientReader.GetInt32(clientReader.GetOrdinal("id")));
                                    acc.lb_nom_client.Content = clientReader.GetString(clientReader.GetOrdinal("nom_client")) + " " + clientReader.GetString(clientReader.GetOrdinal("prenom_client"));
                                    acc.Show();
                                    this.Hide();
                                    //MessageBox.Show("Bienvenu sur votre plateforme", "Reussi", MessageBoxButton.OK, MessageBoxImage.Information);
                                    tb_login.Text = "";
                                    pwd_auth.Password = "";
                                    return;
                                }
                                else //if (clientReader.GetString(clientReader.GetOrdinal("status_client")) == "suspendu")
                                {
                                    MessageBox.Show("Echec de connexion! \n Merci de contacter votre prestataire", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }
                    }

                    // Vérifier si l'utilisateur est un opérateur de saisi // remaque sur pwd_auth
                    using (NpgsqlCommand OperCmd = new NpgsqlCommand("SELECT nom_oper, prenom_oper, mail_oper, pass_oper, status_oper FROM opérateur_de_saisi WHERE mail_oper = '" + tb_login.Text + "' AND pass_oper = '" + mdp + "'", conn))
                    {
                        OperCmd.Parameters.AddWithValue("@mail_oper", tb_login.Text);
                        OperCmd.Parameters.AddWithValue("@pass_oper", mdp);

                        using (var operReader = OperCmd.ExecuteReader())
                        {
                            while (operReader.Read())
                            {


                                if (operReader.Read() && operReader.GetString(operReader.GetOrdinal("status_oper")) == "actif")
                                {
                                    Test_op test_op = new Test_op();
                                    test_op.lb_nom_operateur.Content = operReader.GetString(operReader.GetOrdinal("nom_"));
                                    test_op.Show();
                                    this.Hide();
                                    //MessageBox.Show("Bienvenu sur votre plateforme", "Reussi", MessageBoxButton.OK, MessageBoxImage.Information );
                                    tb_login.Text = "";
                                    pwd_auth.Password = "";
                                    //return;
                                }
                                else //if (operReader.GetString(operReader.GetOrdinal("status_oper")) == "suspendu")
                                {
                                    MessageBox.Show("Echec de connexion! \n Merci de contacter votre administrateur", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }
                    }

                    // Si aucun utilisateur correspondant n'est trouvé
                    MessageBox.Show("Votre identifiant et mot de passe ne correspondent pas. Réessayez", "Erreur");
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs de connexion", "Erreur");
            }
        }

        //passage a la fenetre d'inscription

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Inscription inscription = new Inscription();
            inscription.Show();
            this.Hide();

            //Administrateur operateur=new Administrateur();
            //operateur.LoadDataOperateur();
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
      
        private void tb_login_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= tb_login_GotFocus;
            tb.Foreground = Brushes.Black;
        }

        private void tb_login_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "Email";
                tb.GotFocus += tb_login_GotFocus;
                tb.Foreground = Brushes.LightGray;
            }
            else
            {
                tb.Foreground = Brushes.Black;
            }
        }
       
    }
}