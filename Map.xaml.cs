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
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Web.WebView2.Core;

namespace Projet_2_GoGreen
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        public Map()
        {
            InitializeComponent();
            webView.Source = new Uri("https://maps.google.com/maps?key=ATAO_ETO_LE_KEY");
        }

        private void bt_retour_map_Click(object sender, RoutedEventArgs e)
        {
            Acceuil_client acceuil_Client = new Acceuil_client();
            acceuil_Client.Show();
            this.Hide();

        }

        private void bt_se_deconnecter_map_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}
