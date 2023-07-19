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
    /// Interaction logic for Acceuil_client.xaml
    /// </summary>
    public partial class Acceuil_client : Window
    {
        string id_client = null;
        public Acceuil_client()
        {
            InitializeComponent();
            nombre_arbre();
        }

        public void nombre_arbre()
        {
            ConnectDB conx = new ConnectDB();
            conx.launchReader("SELECT count (*) nombre FROM arbre JOIN client ON arbre.clientid=client.id WHERE client.id ='"+id_client+"'");
            string nb = null;
            if (conx.read.Read()==true)
            {
                nb = conx.read.GetInt32(0).ToString();
            }
            conx.close();
            lb_nbr_arbre.Content = nb;
        }

        private void bt_localiser_arbre_client_Click(object sender, RoutedEventArgs e)
        {
            Map map = new Map();
            map.lb_nom_client.Content = lb_nom_client.Content;
            map.lb_id_user.Content = lb_id_client.Content;
            map.Show();
            this.Hide();

        }

        private void bt_se_deconnecter_client_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment vous déconnecter ?", "Deconnexion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) 
            { 
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
            }
        }

        private void get_id_value_Activation(object sender, EventArgs e)
        {
            id_client = lb_id_client.Content.ToString();
        }
    }
}
