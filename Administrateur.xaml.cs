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

       

        private void lecture_ecriture()
        {

            NpgsqlConnection GetConnection()
            {
                return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
            }
            var conn = GetConnection();
            conn.Open();

            String query = "SELECT * FROM opérateur_de_saisi";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            
            ObservableCollection<OperateurClass> listeOperateurs = new ObservableCollection<OperateurClass>();

            BindingList<OperateurClass> bindingList = new BindingList<OperateurClass>(listeOperateurs);

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                OperateurClass operateur = new OperateurClass();

                grid_oper.ItemsSource = bindingList;

                operateur.setId_op(reader.GetInt32(reader.GetOrdinal("id")));

                operateur.setName(reader.GetString(reader.GetOrdinal("nom_oper")));
                operateur.setLastname(reader.GetString(reader.GetOrdinal("prenom_oper")));
                operateur.setEmail(reader.GetString(reader.GetOrdinal("mail_oper")));
                operateur.setMobile(reader.GetString(reader.GetOrdinal("mobile_oper")));

                listeOperateurs.Add(operateur);

            }
            grid_oper.ItemsSource = listeOperateurs;
            reader.Close();
        }

        private void grid_oper_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (grid_oper.SelectedItem != null)
            {              
                OperateurClass selectedOperateur = (OperateurClass)grid_oper.SelectedItem;

                tb_nom_oper.Text = selectedOperateur.name;
                tb_prenom_oper.Text = selectedOperateur.lastname;
                tb_lieu.Text = selectedOperateur.workplace;
                tb_mobile_oper.Text = selectedOperateur.mobile;
                tb_email_oper.Text = selectedOperateur.email;

            }
        }
    }

}
