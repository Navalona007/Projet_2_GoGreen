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

                //List<OperateurClass> listeOperateurs = new List<OperateurClass>();
                

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
        
        //public string lecture_lieu()
        //{
        //    NpgsqlConnection GetConnection()
        //    {
        //        return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
        //    }
        //    var conn = GetConnection();
        //    conn.Open();

        //    int id = 1; 
        //    //string nomlieu = "nom"; // Le nom de la colonne que vous souhaitez récupérer

        //    string query = "SELECT nom_lieu FROM lieu_travail WHERE ID = id";
        //    using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
        //    {
        //        command.Parameters.AddWithValue("id", id);

        //        using (NpgsqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {

        //                string columnValue = reader.GetString(0); // Récupère la valeur de la colonne

 
  
        //            }
        //        }
        //        //String query = "SELECT name_lieu FROM lieu_travail WHERE ID = id";
        //        return null;
        //}
    }
}
