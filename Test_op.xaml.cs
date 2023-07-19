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

        string id_operateur = null;

        public string id_selected { get; set; }

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
            //option.Visibility = Visibility.Visible;
        }

        Dictionary<string, int> liste_espece;
        public void list_espece()
        {
            liste_espece = null;
            liste_espece = new Dictionary<string, int>();
            //list_type();
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

            String query = "INSERT INTO public.espece (typeid, name_espece)    VALUES ('" + id + "','" + valueInsert + "');";
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
            liste_type = new Dictionary<string, int>();
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
        public void insert_type(string valueInsert)
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

        Dictionary<string, int> liste_etat_feuillage;
        public void list_etat_feuillage()
        {
            liste_etat_feuillage = null;
            liste_etat_feuillage = new Dictionary<string, int>();
            //list_type();
            var conn = GetConnection();
            conn.Open();

            String query = "SELECT * FROM etat_feuillage";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste_espece.Add((string)reader["description"], (int)reader["id"]);
            }
            conn.Close();
        }

        public void insert_etat_feuillage(string valueInsert)
        {
            var conn = GetConnection();
            conn.Open();

            String query = "INSERT INTO public.etat_feuillage (description) VALUES ('" + valueInsert + "'); ";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.ExecuteNonQuery();
        }

        public int hasKey_etat_feuillage(string valuetofind) //verifier si clé existe
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

        Dictionary<string, int> liste_zone;
        public void list_zone()
        {
            liste_zone = null;
            liste_zone = new Dictionary<string, int>();
            //list_type();
            var conn = GetConnection();
            conn.Open();

            String query = "SELECT * FROM zone";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste_espece.Add((string)reader["name_zone"], (int)reader["id"]);
            }
            conn.Close();
        }

        public void insert_zone(string valueInsert)
        {
            var conn = GetConnection();
            conn.Open();

            String query = "INSERT INTO public.zone (name_zone) VALUES ('" + valueInsert + "'); ";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.ExecuteNonQuery();
        }
        public int hasKey_zone(string valuetofind) //verifier si clé existe
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
            list_zone();
            list_etat_feuillage();

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
                //rb_vert.IsChecked = (false);
                //rb_jaune.IsChecked = (false);
                //rb_marron.IsChecked = (false);
                //rb_sans_feuille.IsChecked = (false);

                if (string.IsNullOrWhiteSpace(date_plantation) ||
                    string.IsNullOrWhiteSpace(diametre) ||
                    string.IsNullOrWhiteSpace(hauteur) ||
                    string.IsNullOrWhiteSpace(espece) ||
                    string.IsNullOrWhiteSpace(type) ||
                    string.IsNullOrWhiteSpace(zone) ||
                    string.IsNullOrWhiteSpace(latitude) ||
                    string.IsNullOrWhiteSpace(longitude)) //||
                                                          //!(rb_vert.IsChecked.Value) ||
                                                          //!(rb_jaune.IsChecked.Value) ||
                                                          //!(rb_marron.IsChecked.Value) ||
                                                          //!(rb_sans_feuille.IsChecked.Value))
                {
                    lb_message.Content = "Veuillez remplir tous les champs!";
                }              

                else if (/*liste_zone.ContainsKey(zone) &&*/ !liste_espece.ContainsKey(espece)) //type existe et espece n'existe pas
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

                else if (/*!liste_type.ContainsKey(type) && */liste_espece.ContainsKey(espece)) //type n'existe pas et espece existe  
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

                    } //end catch
                } // end else if

                //else if (!liste_type.ContainsKey(type) && !liste_espece.ContainsKey(espece))//type et espece n'existent pas 
                //{
                //    insert_type(type);
                //    list_type();
                //    int key_type = hasKey_type(type);
                //    insert_espece(key_type, espece);
                //    list_espece();
                //    int key_espece = hasKey_espece(espece);

                //    string requete = "INSERT INTO arbre ( especeid, date_plantation)"
                //                          + " VALUES( '" + key_espece + "', '" + date_plantation + "'); ";
                //    var cmd = new NpgsqlCommand(requete, conx);
                //    try
                //    {
                //        int rowsAffected = cmd.ExecuteNonQuery();

                //        if (rowsAffected == 1)
                //        {
                //            MessageBox.Show("Insertion avec succès!", "Confirmation", MessageBoxButton.OK);
                //        }
                //        else
                //        {
                //            MessageBox.Show("Erreur lors de l'insertion", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Error);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK);
                //    } //end catch
                //} // end else if
                //else if (liste_type.ContainsKey(type) && liste_espece.ContainsKey(espece)) //type et espece existent
                //{
                //    int key_type = hasKey_type(type);
                //   // insert_espece(key_type, espece);
                //    int key_espece = hasKey_espece(espece);
                //    Console.WriteLine("l'arbre de ", type + " " + espece, "");

                //    string requete = "INSERT INTO arbre ( especeid, date_plantation)"
                //                          + " VALUES( '" + key_espece + "', '" + date_plantation + "'); ";
                //    var cmd = new NpgsqlCommand(requete, conx);
                //    try
                //    {
                //        int rowsAffected = cmd.ExecuteNonQuery();

                //        if (rowsAffected == 1)
                //        {
                //            MessageBox.Show("Insertion avec succès!", "Confirmation", MessageBoxButton.OK);
                //        }
                //        else
                //        {
                //            MessageBox.Show("Erreur lors de l'insertion", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Error);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK);

                //    } //end catch
                //} // end else if

            }
            raz();


        }

        public void raz()
        {
            dp_plantation.Text = "";
            tb_diametre.Text = "";
            tb_hauteur.Text = "";
            cb_espece.Text = "";
            cb_type.Text = "";
            tb_zone.Text = "";
            tb_latitude.Text = "";
            tb_longitude.Text = "";
            rb_vert.IsChecked = (false);
            rb_jaune.IsChecked = (false);
            rb_marron.IsChecked = (false);
            rb_sans_feuille.IsChecked = (false);
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

            string date_plantation = dp_plantation.Text;
            string diametre = tb_diametre.Text;
            string hauteur = tb_hauteur.Text;
            string espece = cb_espece.Text;
            string type = cb_type.Text;
            string zone = tb_zone.Text;
            string latitude = tb_latitude.Text;
            string longitude = tb_longitude.Text;

            //recuperer id_lieu + tester lieu            
            list_type();
            list_espece();
            int key_type;
            if (liste_type.ContainsKey(type))
            {
                key_type = hasKey_type(type);
            }
            else
            {
                insert_type(type);
                list_type();
                key_type = hasKey_type(type);
            }//fin test type

            int key_espece;
            if (liste_espece.ContainsKey(espece))
            {
                key_espece = hasKey_espece(espece);

            }
            else
            {
                insert_espece(key_type, espece);
                list_espece();
                key_espece = hasKey_espece(espece);
            }//fin test type

            string connString = @"Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db;";

            string query = "UPDATE arbre SET especeid ='" + key_espece + "' WHERE id=" + id_selected + " ;";

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
                // lecture_ecriture();
                conn.Close();
            }

        }


        private void bt_demander_suppr_Click(object sender, RoutedEventArgs e)
        {
            var arbre_select = grid_liste.SelectedItem as Arbre;
            string id_arbre_select = arbre_select.id_arbre;

            if (id_arbre_select != null)
            {
                MessageBox.Show("Voulez-vous vraiment supprimer cet élément ?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Question);
                delete_arbre_DB(int.Parse(id_arbre_select));
                // lecture_ecriture();
            }
        }

        private void delete_arbre_DB(int id)
        {
            ConnectDB con = new ConnectDB();
            try
            {
                con.executeRequest("DELETE FROM public.arbre WHERE id='" + id + "';");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur de suppression de l'arbre", MessageBoxButton.OK);
            }
        }

        private void bt_annuler_Click(object sender, RoutedEventArgs e)
        {
            raz();
        }


        private void trouverId_client(string nom_client, string mail_client)
        {
            ConnectDB conx = new ConnectDB();
            Dictionary<string, ClientClass> list_nom_client = new Dictionary<string, ClientClass>();
            Dictionary<string, ClientClass> list_mail_client = new Dictionary<string, ClientClass>();
            conx.launchReader("SELECT id, nom_client, prenom_client, mail_client FROM client WHERE opérateur_de_saisiid = '"+lb_id_operateur+"'");
            while (conx.read.Read())
            {
                ClientClass client = new ClientClass();
                
                client.name = conx.read.GetString(conx.read.GetOrdinal("nom_client"));
                client.lastname = conx.read.GetString(conx.read.GetOrdinal("prenom_client"));
                client.email = conx.read.GetString(conx.read.GetOrdinal("mail_client"));
                
                list_nom_client.Add(client.name, client);
                list_mail_client.Add(client.email, client);
            }
            conx.close();

            //if()
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

        private void get_Value_init(object sender, EventArgs e)
        {
            id_operateur = lb_id_operateur.Content.ToString();
        }

    }
}
