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
        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=muri789456123;Database=gg_db");
        }
        public Inscription()
        {
            InitializeComponent();
        }

        private void bt_inscrire_inscription_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                String nom = (string)tb_nom_inscription.Text;
                String prenom = (string)tb_prenom_inscription.Text;
                String mail = (string)tb_mail_inscription.Text;
                String mot_de_passe = (string)tb_mdp_inscription.Text;
                String hash_mdp = CalculateMD5Hash(mot_de_passe);
                String confirmation = (string)tb_confirmer_mdp_inscription.Text;
                String reference = tb_reference_inscription.Text;

                if (rb_client_inscription.IsChecked == true && mot_de_passe == confirmation)
                {
                    if (string.IsNullOrWhiteSpace(nom) ||
                       string.IsNullOrWhiteSpace(prenom) ||
                       string.IsNullOrWhiteSpace(mail) ||
                       string.IsNullOrWhiteSpace(mot_de_passe) ||
                       string.IsNullOrWhiteSpace(confirmation) )
                      // string.IsNullOrWhiteSpace(reference))
                    {
                        MessageBox.Show("Veuillez remplir tous les champs pour vous incrire.    ", "Champs incomplets", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    String requete = @"INSERT INTO public.client(reference_entrepriseid, nom_client, mail_client, pass_client, prenom_client) VALUES ('1 ', '" + nom + "','" + mail + "','" + hash_mdp + "','" + prenom + "')";
                    //using (var cmd = new NpgsqlCommand(requete, conn))
                    //{
                    //    cmd.Parameters.AddWithValue("@nom_client", nom);
                    //    cmd.Parameters.AddWithValue("@prenom_client", prenom);
                    //    cmd.Parameters.AddWithValue("@mail_client", mail);
                    //    cmd.Parameters.AddWithValue("@pass_client", hash_mdp);
                    var cmd = new NpgsqlCommand(requete, conn);
                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected == 1)
                            {
                                //Réinitialiser le texte des TextBox après l'insertion
                                tb_nom_inscription.Text = string.Empty;
                                tb_prenom_inscription.Text = string.Empty;
                                tb_mail_inscription.Text = string.Empty;
                                //tb_reference_inscription.Text = string.Empty;
                                tb_mdp_inscription.Text = string.Empty;
                                tb_confirmer_mdp_inscription.Text = string.Empty;
                            }
                            else
                            {
                                MessageBox.Show("Erreur lors de l'insertion.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Erreur lors de l'insertion", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    //}
                }
                else if (rb_administrateur_inscription.IsChecked == true && mot_de_passe == confirmation)
                {
                    if (string.IsNullOrWhiteSpace(nom) ||
                       string.IsNullOrWhiteSpace(prenom) ||
                       string.IsNullOrWhiteSpace(mail) ||
                       string.IsNullOrWhiteSpace(mot_de_passe) ||
                       string.IsNullOrWhiteSpace(confirmation) ||
                       string.IsNullOrWhiteSpace(reference))
                    {
                        MessageBox.Show("Veuillez remplir tous les champs avant d'ajouter les données.", "Champs incomplets", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    String requete = @"INSERT INTO public.administateur(reference_entrepriseid, nom_admin, prenom_admin, mail_admin, pass_admin)	VALUES ('1', '"+nom+"', '"+prenom+"', '"+mail+"', '"+mot_de_passe+"');";
                    //using (var cmd = new NpgsqlCommand(requete, conn))
                    //{
                    //    cmd.Parameters.AddWithValue("@nom_admin", nom);
                    //    cmd.Parameters.AddWithValue("@prenom_admin", prenom);
                    //    cmd.Parameters.AddWithValue("@mail_admin", mail);
                    //    cmd.Parameters.AddWithValue("@pass_admin", hash_mdp);
                    var cmd = new NpgsqlCommand(requete, conn);
                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected == 1)
                            {
                                // Réinitialiser le texte des TextBox après l'insertion
                                tb_nom_inscription.Text = string.Empty;
                                tb_prenom_inscription.Text = string.Empty;
                                tb_mail_inscription.Text = string.Empty;
                                tb_reference_inscription.Text = string.Empty;
                                tb_mdp_inscription.Text = string.Empty;
                                tb_confirmer_mdp_inscription.Text = string.Empty;
                            }
                            else
                            {
                                MessageBox.Show("Erreur lors de l'insertion.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Erreur lors de l'insertion", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    
                }
                conn.Close();
               
            }
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
