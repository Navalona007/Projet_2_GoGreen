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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Administrateur : Window
    {
        public Administrateur()
        {
            InitializeComponent();
        }
        private void bt_cancel_oper_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bt_add_oper_Click(object sender, RoutedEventArgs e)
        {

        }
        private void connectDB()
        {
            NpgsqlConnection conx = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=admin13;Database=gg_db;");
            conx.Open();
            if (conx.State == System.Data.ConnectionState.Open)
            {
                tb_nom_oper.Text = "connecter";
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
    }
}
