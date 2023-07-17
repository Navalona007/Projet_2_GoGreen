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
        public Test_op()
        {
            InitializeComponent();
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

 

                //String query = "SELECT id, date_plantation, date_creation, status FROM public.arbre;";
                //String query = "SELECT id, status FROM public.arbre;";

                String query = "SELECT arbre.id, arbre.status, type.name, espece.name, etat.height, etat.trunk.diameter ," +
                                ".etat_feuillage.description, position.latitude , position.longitude, zone.name  FROM public.position" +
                                "INNER JOIN public.arbre ON public.position.arbreid = public.arbre.id" +
                                "INNER JOIN public.client ON arbre.clientid = client.id " +
                                "INNER JOIN public.espece ON arbre.especeid = espece.id INNER JOIN public.type ON espece.typeid = type.id";


                NpgsqlCommand command = new NpgsqlCommand(query, conn);

                

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Arbre arbreGG = new Arbre();

                    arbreGG.setId_arbre(reader["id"].ToString());
                    arbreGG.setStatut(reader["status"].ToString());
                    //arbreGG.setStatut(reader.GetString(reader.GetOrdinal("status")));



                    //if (DateTime.TryParse(reader["date_plantation"].ToString(), out DateTime datePlantation))
                    //{
                    //    arbreGG.setDate_plantation(datePlantation);
                    //}

                    //if (DateTime.TryParse(reader["date_création"].ToString(), out DateTime dateCreation))
                    //{
                    //    arbreGG.setDate_creation(datePlantation);
                    //}



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

        private void bt_demander_modif_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void bt_inserer_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
