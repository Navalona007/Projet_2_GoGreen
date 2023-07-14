using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Data;


namespace Projet_2_GoGreen
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Administrateur : Window
    {
        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
        }
        public Administrateur()
        {
            InitializeComponent();
            lecture_ecriture();
            LoadClientData();
        }
        
        private void bt_cancel_oper_Click(object sender, RoutedEventArgs e)
        {
            tb_nom_oper.Text = "";
            tb_prenom_oper.Text = "";
            tb_email_oper.Text = "";
            tb_lieu.Text = "";
            tb_mobile_oper.Text = "";
            pwd_oper.Password = "";
            pwd_oper.Password = "";


            Authentification authentification = new Authentification();
            authentification.Show();
            this.Hide();
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

        

        //Dictionary<string, string> liste_id_lieu;
        Dictionary<string, int> liste_name_lieu;
        public void list_lieu()
        {
            //liste_id_lieu = null;
            liste_name_lieu = null;
            //liste_id_lieu = new Dictionary<string, string>();
            liste_name_lieu = new Dictionary<string, int>();
            var conn = GetConnection();
            conn.Open();

            String query = "SELECT * FROM lieu_travail";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //liste_id_lieu.Add(reader["id"].ToString(), (string)reader["name_lieu"]);//cast error
                liste_name_lieu.Add((string)reader["name_lieu"], (int)reader["id"]);
            }
            conn.Close();
            //return liste_reference;
        }

        public void insertLieu(string valueInsert) // inserer lieu
        {
            var conn = GetConnection();
            conn.Open();

            String query = "INSERT INTO public.lieu_travail (name_lieu)	VALUES ('" + valueInsert + "');";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.ExecuteNonQuery();

        }

        public int hasKey(string valuetofind) //verifier si clé existe
        {
            int key = 0;

            foreach (var pair in liste_name_lieu)
            {
                if (pair.Key == valuetofind)
                {
                    key = pair.Value;
                    
                    break;
                    
                }
                
            }
            return key;
        }


        private void bt_cancel_oper_Click(object sender, RoutedEventArgs e)
        {
            tb_nom_oper.Text = string.Empty;
            tb_prenom_oper.Text = string.Empty;
            tb_email_oper.Text = string.Empty;
            tb_lieu.Text = string.Empty;
            tb_mobile_oper.Text = string.Empty;
            pwd_oper.Password = "";
            pwd_oper_confirm.Password = "";
        }

        private void bt_add_oper_Click(object sender, RoutedEventArgs e)
        {

            list_lieu();
            using (var conn = GetConnection())

            NpgsqlConnection conx = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;");
            conx.Open();
            if (conx.State == System.Data.ConnectionState.Open)
            {
                
                string nom = tb_nom_oper.Text;
                string prenom = tb_prenom_oper.Text;
                string mail = tb_email_oper.Text;
                string lieu_travail = tb_lieu.Text;
                string mobile = tb_mobile_oper.Text;
                string mot_de_passe_oper = pwd_oper.Password;
                String hash_mdp = CalculateMD5Hash(mot_de_passe_oper);
                string confirm_mdp = pwd_oper_confirm.Password;


                if (string.IsNullOrWhiteSpace(nom) ||
                    string.IsNullOrWhiteSpace(prenom) ||
                    string.IsNullOrWhiteSpace(mail) ||
                    string.IsNullOrWhiteSpace(lieu_travail) ||
                    string.IsNullOrWhiteSpace(mobile) ||
                    string.IsNullOrWhiteSpace(mot_de_passe_oper) ||
                    string.IsNullOrWhiteSpace(confirm_mdp))
                {
                    lb_message_oper.Content = "Veuillez remplir tous les champs!";
                    lb_message_oper.Foreground = Brushes.Red;
                }

                else if (!string.IsNullOrWhiteSpace(lieu_travail) && mot_de_passe_oper == confirm_mdp && liste_name_lieu.ContainsKey(lieu_travail))
                {                    
                    {
                        int key = hasKey(lieu_travail);
                        conn.Open();
                        string requete = "INSERT INTO opérateur_de_saisi( lieu_travailid, nom_oper, prenom_oper, mail_oper, pass_oper, mobile_oper)"
                                          + " VALUES( '" + key + "', '" + nom + "', '" + prenom + "', '" + mail + "', '" + hash_mdp + "', '" + mobile + "'); ";
                        var cmd = new NpgsqlCommand(requete, conn);
                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected == 1)
                            {
                                // Réinitialiser le texte des TextBox après l'insertion 
                                tb_nom_oper.Text = string.Empty;
                                tb_prenom_oper.Text = string.Empty;
                                tb_email_oper.Text = string.Empty;
                                tb_lieu.Text = string.Empty;
                                tb_mobile_oper.Text = string.Empty;
                                pwd_oper.Password = "";
                                pwd_oper_confirm.Password = "";

                                MessageBox.Show("Inscription de l'opérateur de saisie réussi", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
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
                }
                else if (!string.IsNullOrWhiteSpace(lieu_travail) && mot_de_passe_oper == confirm_mdp && !liste_name_lieu.ContainsKey(lieu_travail))
                {                    
                        insertLieu(lieu_travail);
                        list_lieu();
                        int key = hasKey(lieu_travail);
                        conn.Open();
                        string requete = "INSERT INTO opérateur_de_saisi( lieu_travailid, nom_oper, prenom_oper, mail_oper, pass_oper, mobile_oper)"
                                          + " VALUES( '" + key + "', '" + nom + "', '" + prenom + "', '" + mail + "', '" + hash_mdp + "', '" + mobile + "'); ";
                        var cmd = new NpgsqlCommand(requete, conn);
                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected == 1)
                            {
                                // Réinitialiser le texte des TextBox après l'insertion 
                                tb_nom_oper.Text = string.Empty;
                                tb_prenom_oper.Text = string.Empty;
                                tb_email_oper.Text = string.Empty;
                                tb_lieu.Text = string.Empty;
                                tb_mobile_oper.Text = string.Empty;
                                pwd_oper.Password = "";
                                pwd_oper_confirm.Password = "";

                                MessageBox.Show("Inscription de l'opérateur de saisie réussi", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
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
            }
        }

        private void tb_search_oper_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void bt_suppr_oper_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                GetConnection();


            }
            catch (Exception ex)
            {

            }
        }

        private void bt_modifier_oper_Click(object sender, RoutedEventArgs e)
        {
            string connString = @"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;";

            //string query = "UPDATE opérateur_de_saisi SET colonne1 = @valeur1, colonne2 = @valeur2 WHERE id = @valeur_id;";

            //string query = "UPDATE opérateur_de_saisi SET nom_oper = tb_nom_oper.Text, prenom_oper = tb_prenom_oper.Text, mail_oper =tb_email_oper.Text WHERE id=@id;";
            
            string query = "UPDATE opérateur_de_saisi SET nom_oper ='"+tb_nom_oper.Text+ "', prenom_oper = '"+tb_prenom_oper.Text+"', mail_oper = '"+tb_email_oper.Text+"' WHERE id=id;";

            NpgsqlConnection conn = new NpgsqlConnection(connString);
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nom_oper", tb_nom_oper.Text);
            cmd.Parameters.AddWithValue("@prenom_oper", tb_prenom_oper.Text);
            cmd.Parameters.AddWithValue("@mail_oper", tb_email_oper.Text);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                lecture_ecriture();
                conn.Close();
            }

        }

        private void tb_search_client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Click_bt_demande_suppr_valide(object sender, RoutedEventArgs e)
        {

        }

        private void Click_bt_demande_suppr_decline(object sender, RoutedEventArgs e)
        {

        }

        private void Click_bt_demande_modif_valide(object sender, RoutedEventArgs e)
        {

        }

        private void Click_bt_demande_modif_decline(object sender, RoutedEventArgs e)
        {

        }

        private void bt_se_déconnecter_Click(object sender, RoutedEventArgs e)
        {
            Authentification authentification = new Authentification();
            authentification.Show();
            this.Hide();
        }


        private void grid_oper_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void tb_mail_oper_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= tb_mail_oper_GotFocus;
        }

        private void lecture_ecriture()
        {
            ObservableCollection<OperateurClass> listeOperateurs = new ObservableCollection<OperateurClass>();
            NpgsqlConnection GetConnection()
            {
                return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
            }
            try
            {
                var conn = GetConnection();
                conn.Open();

                String query = "SELECT opérateur_de_saisi.id, nom_oper, prenom_oper, mail_oper, mobile_oper, name_lieu, status " +
                                "FROM opérateur_de_saisi " +
                                "INNER JOIN lieu_travail ON opérateur_de_saisi.lieu_travailid = lieu_travail.id " +
                                "INNER JOIN statut_opérateur ON opérateur_de_saisi.statut_opérateurid = statut_opérateur.id";
                NpgsqlCommand command = new NpgsqlCommand(query, conn);

                 BindingList<OperateurClass> bindingList = new BindingList<OperateurClass>(listeOperateurs);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    OperateurClass operateur = new OperateurClass();

                     grid_oper.ItemsSource = bindingList;

                    operateur.setId(reader["id"].ToString());
                    operateur.setName(reader["nom_oper"].ToString());
                    operateur.setLastname(reader.GetString(reader.GetOrdinal("prenom_oper")));
                    operateur.setEmail(reader.GetString(reader.GetOrdinal("mail_oper")));
                    operateur.setMobile(reader.GetString(reader.GetOrdinal("mobile_oper")));
                    operateur.setWorkplace(reader.GetString(reader.GetOrdinal("name_lieu")));
                    operateur.setStatut(reader.GetString(reader.GetOrdinal("status")));

                    listeOperateurs.Add(operateur);

                }
                grid_oper.ItemsSource = listeOperateurs;
                
                conn.Close();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "erreur request", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
       
       public string connectionString = @"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;";

        private void LoadClientData()
        {
            string query = "SELECT id, nom_client, prenom_client, mail_client, adresse_client, date_inscrip, mobile_client, status_client FROM public.client;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        List<ClientClass> clients = new List<ClientClass>();

                        while (reader.Read())
                        {
                            ClientClass client = new ClientClass();
                            client.id = reader["id"].ToString();
                            client.name = reader["nom_client"].ToString();
                            client.lastname = reader["prenom_client"].ToString();
                            client.email = reader["mail_client"].ToString();
                            client.mobile = reader["mobile_client"].ToString();
                            client.adress = reader["adresse_client"].ToString();
                            //client.dateInscription = (DateTime)reader["date_inscrip"];
                            client.status = reader["status_client"].ToString();

                            clients.Add(client);
                        }

                        grid_client.ItemsSource = clients;
                    }
                }

                connection.Close();
            }
        }

        private void ButtonStatus_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string id = button.Tag.ToString();

           // Find the corresponding client object in the DataGrid's underlying data source
            ClientClass client = grid_client.Items.OfType<ClientClass>().FirstOrDefault(c => c.id == id);

            //Update the status value and button content based on the current status
            if (client.status == "actif")
            {
                client.status = "suspendu";
                button.Content = "Réactiver";
            }
            else if (client.status == "suspendu")
            {
                client.status = "actif";
                button.Content = "Suspendre";
            }
        }
    }

}
