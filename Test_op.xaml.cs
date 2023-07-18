using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Test_op.xaml
    /// </summary>
    public partial class Test_op : Window
    {

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
        }

        public Test_op()
        {
            InitializeComponent();
            lecture_ecriture_arbre();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void bt_se_déconnecter_Click(object sender, RoutedEventArgs e)
        {
            Authentification authentification = new Authentification();
            authentification.Show();
            this.Hide();
        }

        Dictionary<string, int> liste_espece;
        public void list_espece ()
        {
            liste_espece = null;
            liste_espece = new Dictionary<string, int>();
            var conn = GetConnection();
            conn.Open();

            String query = "SELECT id, name_espece FROM espece";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste_espece.Add((string)reader["name_espece"], (int)reader["id"]);
            }
            conn.Close();
        }

        public void insert_espece(int id, string valueInsert)
        {
            var conn = GetConnection();
            conn.Open();

            String query = "INSERT INTO public.espece (typeid, name_espece)    VALUES ('"+id+"','" + valueInsert + "');";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.ExecuteNonQuery();
        }

        public int hasKey_espece(string valuetofind) //verifier si clé existe
        {
            int key = 0;

            foreach (var pair in liste_espece)
            {
                if (pair.Key == valuetofind)
                {
                    key = pair.Value;

                    break;

                }

            }
            return key;
        }

        Dictionary<string, string> liste_id_type;
        Dictionary<string, int> liste_type;
        public void list_type()
        {
            liste_id_type = null;
            liste_type = null;
            liste_id_type = new Dictionary<string, string>();
            liste_type = new Dictionary<string, int> ();            
            var conn = GetConnection();
            conn.Open();

            String query = "SELECT * FROM type";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste_id_type.Add(reader["id"].ToString(), (string)reader["name_type"]);
                liste_type.Add((string)reader["name_type"], (int)reader["id"]);
            }
            conn.Close();
        }
        public void insert_type(string valueInsert) // inserer lieu
        {
            var conn = GetConnection();
            conn.Open();

            String query = "INSERT INTO public.type (name_type) VALUES ('" + valueInsert + "'); ";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.ExecuteNonQuery();
        }

        public int hasKey_type(string valuetofind) //verifier si clé existe
        {
            int key = 0;

            foreach (var pair in liste_type)
            {
                if (pair.Key == valuetofind)
                {
                    key = pair.Value;

                    break;

                }

            }
            return key;
        }


        private void bt_inserer_Click(object sender, RoutedEventArgs e)
        {
            list_type();
            list_espece();
            NpgsqlConnection conx = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;");
            conx.Open();
            if (conx.State == System.Data.ConnectionState.Open)
            {
                string date_plantation = dp_plantation.Text;
                string diametre = tb_diametre.Text;
                string hauteur = tb_hauteur.Text;
                string espece = cb_espece.Text;
                string type = cb_type.Text;
                string zone = tb_zone.Text;
                string latitude = tb_latitude.Text;
                string longitude = tb_longitude.Text;

                if (string.IsNullOrWhiteSpace(date_plantation) ||
                    string.IsNullOrWhiteSpace(diametre) ||
                    string.IsNullOrWhiteSpace(hauteur) ||
                    string.IsNullOrWhiteSpace(espece) ||
                    string.IsNullOrWhiteSpace(type) ||
                    string.IsNullOrWhiteSpace(zone) ||
                    string.IsNullOrWhiteSpace(latitude) ||
                    string.IsNullOrWhiteSpace(longitude))
                {
                    lb_message.Content = "Veuillez remplir tous les champs!";                   
                } 
                else if (liste_type.ContainsKey(type) && !liste_espece.ContainsKey(espece))
                {
                   
                    int key_type = hasKey_type(type);
                    insert_espece(key_type, espece);
                    list_espece();                    
                    int key_espece = hasKey_espece(espece);

                    string requete = "INSERT INTO arbre ( especeid, date_plantation)"
                                          + " VALUES( '" + key_espece + "', '" + date_plantation + "'); ";
                    var cmd = new NpgsqlCommand(requete, conx);
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 1)
                        {
                            MessageBox.Show("Insertion avec succès!", "Confirmation", MessageBoxButton.OK);
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de l'insertion", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK);

                    }

                }

                else if (!liste_type.ContainsKey(type) && liste_espece.ContainsKey(espece))
                {
                    insert_type(type);
                    list_type();
                    int key_type = hasKey_type(type);
                    insert_espece(key_type, espece);
                    list_espece();
                    int key_espece = hasKey_espece(espece);

                    string requete = "INSERT INTO arbre ( especeid, date_plantation)"
                                          + " VALUES( '" + key_espece + "', '" + date_plantation + "'); ";
                    var cmd = new NpgsqlCommand(requete, conx);
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 1)
                        {
                            MessageBox.Show("Insertion avec succès!", "Confirmation", MessageBoxButton.OK);
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de l'insertion", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK);

                    }

                }

            } 

            }

        List<string> valeurs = new List<string>();

        private void cb_espece_TextInput(object sender, TextCompositionEventArgs e)
        {
            string nouvelleValeur = cb_espece.Text;
            if (!string.IsNullOrWhiteSpace(nouvelleValeur))
            {
                valeurs.Add(nouvelleValeur);
            }
         }
        private void lecture_ecriture_arbre()
        {
            ObservableCollection<Arbre> listeArbres = new ObservableCollection<Arbre>();
            NpgsqlConnection GetConnection()
            {
                return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
            }
            try
            {
                var conn = GetConnection();
                conn.Open();

                String query = "SELECT arbre.id, arbre.status_arbre, espece.name_espece, client.nom_client, arbre.date_plantation " +
                                "FROM public.arbre " +
                                "JOIN public.espece ON public.espece.id = public.arbre.especeid " +
                                "JOIN public.client ON public.client.id = public.arbre.clientid";// WHERE arbre.opérateur_de_saisiid = '+lb_id_operateur.Content+'";//id_operatezur_de saisi ne cours

                NpgsqlCommand command = new NpgsqlCommand(query, conn);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Arbre arbreGG = new Arbre();

                    arbreGG.id_arbre = reader["id"].ToString();
                    arbreGG.statut = reader["status_arbre"].ToString();
                    arbreGG.espece = reader.GetString(reader.GetOrdinal("name_espece"));
                    arbreGG.nom_client = reader.GetString(reader.GetOrdinal("nom_client"));
                    arbreGG.date_plantation = reader.GetString(reader.GetOrdinal("date_plantation"));

                    listeArbres.Add(arbreGG);
                }
                grid_liste.ItemsSource = listeArbres;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "erreur request", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void lecture_etat_arbre()
        {
            ObservableCollection<Etat_arbre> liste_Etat = new ObservableCollection<Etat_arbre>();
            var selection = grid_liste.SelectedItem as Arbre;
            string id_arbre = selection.id_arbre;
            NpgsqlConnection GetConnection()
            {
                return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
            }
            try
            {
                var conn = GetConnection();
                conn.Open();

                String requete = "SELECT public.zone.name_zone, public.etat.date_create_etat, public.etat_feuillage.description, public.etat.height, public.etat.trunk_diameter, type.name_type " +
                                "FROM public.arbre " +
                                "join espece on espece.id=arbre.especeid "+
                                "join type on type.id=espece.typeid "+
                                "join public.etat on public.arbre.id= public.etat.arbreid " +
                                "join public.etat_feuillage on etat_feuillage.id=etat.etat_feuillageid2 " +
                                "join public.position on public.arbre.id=public.position.arbreid " +
                                "join public.zone on public.zone.id=public.position.zoneid WHERE arbre.id='"+id_arbre+"'";


                NpgsqlCommand command = new NpgsqlCommand(requete, conn);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Etat_arbre etatGG = new Etat_arbre();

                    etatGG.zone=reader["name_zone"].ToString();
                    MessageBox.Show(etatGG.zone);
                    etatGG.type=reader["name_type"].ToString();
                    etatGG.hauteur = reader.GetFloat(reader.GetOrdinal("height"));
                    etatGG.diametre_tronc = reader.GetFloat(reader.GetOrdinal("trunk_diameter"));
                    etatGG.date_mis_a_jour = reader.GetString(reader.GetOrdinal("date_create_etat"));
                    etatGG.etat_feuillage = reader.GetString(reader.GetOrdinal("description"));

                    liste_Etat.Add(etatGG);
                }
                grid_etat.ItemsSource = liste_Etat;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "erreur request", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void bt_demander_modif_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void bt_demander_suppr_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bt_inserer_Copy_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void selected_cells(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void grid_etat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void grid_liste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
