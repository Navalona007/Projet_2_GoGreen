using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        NpgsqlConnection conx;
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

        private void bt_add_oper_Click(object sender, RoutedEventArgs e)
        {
            string nom = tb_nom_oper.Text;
            string prenom = tb_prenom_oper.Text;
            string mail = tb_email_oper.Text;
            string mobile = tb_mobile_oper.Text;
            if (string.IsNullOrEmpty(nom))
            {
                lb_message_oper.Content = "remplir les champs obligatoires!";
            }
            else { 
            try
            {
                connectDB();

                    string requ = "INSERT INTO opérateur_de_saisi(statut_opérateurid, lieu_travailid, nom_oper, prenom_oper, mail_oper, pass_oper, mobile_oper)"
                                    +" VALUES( , ?, ?, ?, ?, ?, ?); ";
                conx.CreateCommand();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }
        private void connectDB()
        {
            NpgsqlConnection conx = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;");
            conx.Open();
            if (conx.State == System.Data.ConnectionState.Open)
            {
                lb_message_oper.Content = "connecter";
            }
        }

        private void tb_search_oper_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void bt_suppr_oper_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                connectDB();


            }
            catch (Exception ex)
            {

            }
        }

        private void bt_modifier_oper_Click(object sender, RoutedEventArgs e)
        {
           
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
