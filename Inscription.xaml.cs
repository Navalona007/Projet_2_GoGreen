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
            return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
        }

      
        public Inscription()
        {
            InitializeComponent();
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

        Dictionary<string, string> liste_id_ref;
        Dictionary<string, int> liste_label_ref;
        public void list_reference()
        {
            liste_id_ref = null;
            liste_label_ref = null;
            liste_id_ref = new Dictionary<string, string>();
            liste_label_ref = new Dictionary<string, int>();
            var conn = GetConnection();
            conn.Open();
            
            String query = "SELECT * FROM reference_entreprise";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste_id_ref.Add(reader["id"].ToString(), (string)reader["label_ref"]) ;//cast error
                liste_label_ref.Add((string)reader["label_ref"], (int)reader["id"]);
            }

        }
        public void insertReference(string valueInsert)
        {
            var conn = GetConnection();
            conn.Open(); 

            String query = "INSERT INTO public.reference_entreprise(label_ref)	VALUES ('"+valueInsert+"');";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.ExecuteNonQuery();

        }

        public int hasKey(string valuetofind)
        {
            int key=0;

            foreach (var pair in liste_label_ref)
            {
                if( pair.Key == valuetofind)
                {
                    key = pair.Value;
                    break;
                }
            }
            return key;
        }

        public bool isExist(string valueTofind)
        {
            if(liste_id_ref.ContainsValue(valueTofind))
            {
                MessageBox.Show("Cette référence existe déjà. \nMerci de saisir votre référence d'entreprise.", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int conversion_reference_key(string value)
        {
            int id = 0;
            if (int.TryParse(value, out id)) { 
                id = int.Parse(value);
                return id;
            }
            else
            {
                MessageBox.Show("Cette référence n'existe pas. \nMerci de contacter votre prestataire.", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Warning);
                tb_reference_inscription.Text = "";
                return 0;
             }
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
                DateTime date_inscription = DateTime.Now;

                list_reference();

                if (string.IsNullOrWhiteSpace(nom) ||
                       string.IsNullOrWhiteSpace(prenom) ||
                       string.IsNullOrWhiteSpace(mail) ||
                       string.IsNullOrWhiteSpace(mot_de_passe) ||
                       string.IsNullOrWhiteSpace(confirmation) ||
                       string.IsNullOrWhiteSpace(reference))
                {
                    MessageBox.Show("Veuillez remplir tous les champs pour vous incrire.", "Champs obligatoires", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                else if (rb_client_inscription.IsChecked == true && mot_de_passe == confirmation && liste_id_ref.ContainsKey(reference))
                {

                    
                    String requete = @"INSERT INTO public.client(reference_entrepriseid, nom_client, mail_client, pass_client, date_inscrip, prenom_client, status_client) "+
                        "VALUES ('"+int.Parse(reference)+"', '" + nom + "','" + mail + "','" + hash_mdp + "', '"+date_inscription+"','" + prenom + "', 'actif')";//client inscrit doit etre toujours actif

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
                                tb_reference_inscription.Text = string.Empty;
                                tb_mdp_inscription.Text = string.Empty;
                                tb_confirmer_mdp_inscription.Text = string.Empty;
                                MessageBox.Show("Inscription avec succès!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                Authentification authentification = new Authentification();
                                authentification.Show();
                                this.Hide();
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

                else if (rb_administrateur_inscription.IsChecked == true && mot_de_passe == confirmation && !liste_label_ref.ContainsKey(reference))
                {
                    insertReference(reference);
                    list_reference();
                    int key = hasKey(reference);


                    String requete = @"INSERT INTO public.administrateur(reference_entrepriseid, nom_admin, prenom_admin, mail_admin, pass_admin, date_inscript)	VALUES ('"+ key + "', '"+nom+"', '"+prenom+"', '"+mail+"', '"+hash_mdp+"', '"+date_inscription+"');";
                    
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
                                MessageBox.Show("Inscription réussie! \n Votre clé de référence à partager avec vos clients est : "+key , "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                Authentification authentification = new Authentification();
                                authentification.Show();
                                this.Hide();
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
                else if (!liste_id_ref.ContainsKey(reference) || liste_label_ref.ContainsKey(reference))
                {
                    MessageBox.Show("Cette référence est invalide. \nVeuillez entrer une référe nce valide", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
                    tb_reference_inscription.Text = "";
                }
                conn.Close();
               
            }
        }


        private void bt_annuler_inscription_Click(object sender, RoutedEventArgs e)
        {
            tb_nom_inscription.Text = "";
            tb_prenom_inscription.Text = "";
            tb_mail_inscription.Text = "";
            tb_mdp_inscription.Text = "";
            tb_confirmer_mdp_inscription.Text = "";
            tb_reference_inscription.Text = "";

            Authentification authentification = new Authentification();
            authentification.Show();

            this.Hide();

        }
    }
}
