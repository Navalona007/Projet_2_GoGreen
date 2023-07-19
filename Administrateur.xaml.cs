using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Projet_2_GoGreen
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Administrateur : Window
    {
        public static string id_admin_connected { get; set; }

        public string id_selected { get; set; }

        //ConnectDB conx = new ConnectDB();// used by samira
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


        Authentification auth = new Authentification();
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

            conn.Close();

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
            //using (var conn = GetConnection())

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

                        string requete = "INSERT INTO opérateur_de_saisi( lieu_travailid, nom_oper, prenom_oper, mail_oper, pass_oper, mobile_oper, status_oper)"
                                          + " VALUES( '" + key + "', '" + nom + "', '" + prenom + "', '" + mail + "', '" + hash_mdp + "', '" + mobile + "', 'actif'); ";//operateur indcri doit etre toujours actif
                        var cmd = new NpgsqlCommand(requete, conx);
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

                                MessageBox.Show("Inscription de l'opérateur de saisie réussie", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
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

                    string requete = "INSERT INTO opérateur_de_saisi( lieu_travailid, nom_oper, prenom_oper, mail_oper, pass_oper, mobile_oper, status_oper)"
                                      + " VALUES( '" + key + "', '" + nom + "', '" + prenom + "', '" + mail + "', '" + hash_mdp + "', '" + mobile + "', 'actif'); ";//operateur indcri doit etre toujours actif
                    var cmd = new NpgsqlCommand(requete, conx);
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

                            MessageBox.Show("Inscription de l'opérateur de saisie réussie", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
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

                    conx.Close();

                }
                lecture_ecriture();
            }
        }

        private void tb_search_oper_TextChanged(object sender, TextChangedEventArgs e)
        {
            lecture_ecriture();
            string text_to_find = tb_search_oper.Text;
            searchOperateur(listeOperateurs, text_to_find);
        }
        //recherche operateur
        private void searchOperateur(ObservableCollection<OperateurClass> listInit, string text)
        {
            if (text.Contains(" "))
            {
                string[] decompos = text.Split(' ');

                for (int i = 0; i < decompos.Length; i++)
                {
                    listInit = triageListeOperateur(listInit, decompos[i]);
                }
            }
            else
            {
                listInit = triageListeOperateur(listInit, text);
            }
            grid_oper.ItemsSource = listInit;
        }

        private ObservableCollection<OperateurClass> triageListeOperateur(ObservableCollection<OperateurClass> list, string text)
        {
            ObservableCollection<OperateurClass> searchList = new ObservableCollection<OperateurClass>();
            foreach (OperateurClass oper in list)
            {
                text.ToLower(); //same conversion

                if (oper.toString().Contains(text))
                {
                    searchList.Add(oper);
                }
            }
            return searchList;
        }

        private void bt_suppr_oper_Click(object sender, RoutedEventArgs e)
        {
            var oper_select = grid_oper.SelectedItem as OperateurClass;
            string id_select = oper_select.id;

            if (id_select != null)
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer cet élément ?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Question);
                // grid_oper.SelectedItems.Clear();

                // Handle the button click based on the result
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        delete_oper_DB(int.Parse(id_select));
                        lecture_ecriture();
                        tb_nom_oper.Text = string.Empty;
                        tb_prenom_oper.Text = string.Empty;
                        tb_email_oper.Text = string.Empty;
                        tb_lieu.Text = string.Empty;
                        tb_mobile_oper.Text = string.Empty;
                        pwd_oper.Password = "";
                        pwd_oper_confirm.Password = "";
                        break;
                    case MessageBoxResult.No:
                        tb_nom_oper.Text = string.Empty;
                        tb_prenom_oper.Text = string.Empty;
                        tb_email_oper.Text = string.Empty;
                        tb_lieu.Text = string.Empty;
                        tb_mobile_oper.Text = string.Empty;
                        pwd_oper.Password = "";
                        pwd_oper_confirm.Password = "";
                        break;
                }
            }
        }

        private void delete_oper_DB(int id)
        {
            ConnectDB con = new ConnectDB();
            try
            {
                con.executeRequest("DELETE FROM public.opérateur_de_saisi WHERE id='" + id + "';");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur de suppression de l'opérateur de saisi", MessageBoxButton.OK);
            }

        }

        private void bt_modifier_oper_Click(object sender, RoutedEventArgs e)
        {
            //recuperer id_lieu + tester lieu
            string lieu_travail = tb_lieu.Text;
            list_lieu();
            int key;
            if (liste_name_lieu.ContainsKey(lieu_travail))
            {
                key = hasKey(lieu_travail);
            }
            else
            {
                insertLieu(lieu_travail);
                list_lieu();
                key = hasKey(lieu_travail);
            }//fin test lieu

            string connString = @"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;";

            string query = "UPDATE opérateur_de_saisi SET nom_oper ='" + tb_nom_oper.Text + "', prenom_oper = '" + tb_prenom_oper.Text + "', mail_oper = '" + tb_email_oper.Text + "', lieu_travailid = '" + key + "' WHERE id=" + id_selected + " ;";//ajouter id_lieu modifié

            NpgsqlConnection conn = new NpgsqlConnection(connString);
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

            try
            {
                conn.Open();
                MessageBox.Show("Voulez-vous vraiment modifier cet élément?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
            LoadClientData();
            string text_to_find = tb_search_client.Text;
            searchClient(clients, text_to_find);

        }
        //recherche client
        private void searchClient(List<ClientClass> listInit, string text_to_find)
        {
            if (text_to_find.Contains(" "))
            {
                string[] decompos = text_to_find.Split(' ');

                for (int i = 0; i < decompos.Length; i++)
                {
                    listInit = triageListeClient(listInit, decompos[i]);
                }
            }
            else
            {
                listInit = triageListeClient(listInit, text_to_find);
            }
            grid_client.ItemsSource = listInit;
        }

        private List<ClientClass> triageListeClient(List<ClientClass> list, string text)
        {
            List<ClientClass> searchList = new List<ClientClass>();
            foreach (ClientClass kila in list)
            {
                text.ToLower(); //same conversion

                if (kila.toString().Contains(text))
                {
                    searchList.Add(kila);
                }
            }
            return searchList;
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
            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, "Voulez-vous vraiment vous déconnecter?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                Authentification authentification = new Authentification();
                authentification.Show();
                this.Hide();
            }
        }

        ObservableCollection<OperateurClass> listeOperateurs = new ObservableCollection<OperateurClass>();
        private void lecture_ecriture()
        {
            listeOperateurs.Clear();
            NpgsqlConnection GetConnection()
            {
                return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
            }
            try
            {
                var conn = GetConnection();
                conn.Open();
                String query = "SELECT opérateur_de_saisi.id, nom_oper, prenom_oper, mail_oper, mobile_oper, name_lieu, status_oper " +
                                "FROM opérateur_de_saisi " +
                                "INNER JOIN lieu_travail ON opérateur_de_saisi.lieu_travailid = lieu_travail.id "; //+
                                                                                                                   // "INNER JOIN statut_opérateur ON opérateur_de_saisi.statut_opérateurid = statut_opérateur.id";
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    OperateurClass operateur = new OperateurClass();
                    operateur.id = reader["id"].ToString();
                    operateur.name = reader["nom_oper"].ToString();
                    operateur.lastname = reader["prenom_oper"].ToString();
                    operateur.email = reader["mail_oper"].ToString();
                    operateur.mobile = reader["mobile_oper"].ToString();
                    operateur.workplace = reader["name_lieu"].ToString();
                    operateur.statut = reader["status_oper"].ToString();
                    listeOperateurs.Add(operateur);
                }
                grid_oper.ItemsSource = listeOperateurs;

                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "erreur request", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ButtonStatus_oper_Click(object sender, RoutedEventArgs e)
        {
            var selectedOperateur = grid_oper.SelectedItem as OperateurClass;
            string idO = selectedOperateur.id;
            string status_oper = null; ;
            string connString = @"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;";

            if (selectedOperateur.statut == "actif")
            {
                status_oper = "suspendu";
            }

            else if (selectedOperateur.statut == "suspendu")
            {
                status_oper = "actif";
            }

            string query = "UPDATE opérateur_de_saisi SET status_oper = '" + status_oper + "' WHERE id=" + idO + " ;";
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

            try
            {
                conn.Open();
                Console.WriteLine("=================opened====================");
                cmd.ExecuteNonQuery();
                Console.WriteLine("=================write====================");
                conn.Close();
                Console.WriteLine("=================closed====================");
                lecture_ecriture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //----------------------------------------------------------ONGLET CLIENT----------------------------------------------------------------------
        //Navalona
        //Début . . .
        //*****Méthodes********:*********Afficher la liste client dans le tableau de grid_client*******************************************************
        //*******************************Sélectionner client dans le tableau de grid_client************************************************************
        //*******************************Modifier le statut du client dans le tableau de grid_client***************************************************
        //*******************************Mettre à jour la table client dans la BdD*********************************************************************


        public string connectionString = @"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;";

        List<ClientClass> clients = new List<ClientClass>();
        private void LoadClientData()
        {
            clients.Clear();
            string query = "SELECT client.id, nom_client, prenom_client, mail_client, adresse_client, date_inscrip, mobile_client, status_client FROM public.client JOIN reference_entreprise ON client.reference_entrepriseid=reference_entreprise.id WHERE reference_entreprise.id='2';";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        //List<ClientClass> clients = new List<ClientClass>();


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
                        reader.Close();
                        grid_client.ItemsSource = clients;

                    }
                }

                connection.Close();
            }
        }

        public void refresh_client()
        {
            grid_client.ItemsSource = null;
            LoadClientData();
        }

        private void ButtonStatusClient_Click(object sender, RoutedEventArgs e)
        {
            string connString = @"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;";
            var selectedClient = grid_client.SelectedItem as ClientClass;
            string idC = selectedClient.id;
            string status_client = null;

            if (selectedClient.status == "actif")
            {
                status_client = "suspendu";

            }
            else if (selectedClient.status == "suspendu")
            {
                status_client = "actif";
            }

            string query = "UPDATE client SET status_client = '" + status_client + "' WHERE id=" + idC + " ;";
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

            try
            {
                conn.Open();
                Console.WriteLine("=================opened====================");
                cmd.ExecuteNonQuery();
                Console.WriteLine("=================write====================");
                conn.Close();
                Console.WriteLine("=================closed====================");
                refresh_client();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //. . . Fin
        //---------------------------------------------------------------------------------------------------------------------------------------------

        private void grid_oper_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            if (grid_oper.SelectedItem != null)
            {
                var selectedOperateur = grid_oper.SelectedItem as OperateurClass; // grid_oper.SelectedItem se comporte comme une instance de la Classe  OperateurClass

                id_selected = selectedOperateur.id;

                tb_nom_oper.Text = selectedOperateur.name;
                tb_prenom_oper.Text = selectedOperateur.lastname;
                tb_lieu.Text = selectedOperateur.workplace;
                tb_mobile_oper.Text = selectedOperateur.mobile;
                tb_email_oper.Text = selectedOperateur.email;

            }
            else
            {
                tb_nom_oper.Text = "";
                tb_prenom_oper.Text = "";
                tb_lieu.Text = "";
                tb_mobile_oper.Text = "";
                tb_email_oper.Text = "";
            }

        }

        private void selected_cells(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }

}
