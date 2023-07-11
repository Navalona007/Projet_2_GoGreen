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
    /// Interaction logic for Authentification.xaml
    /// </summary>
    public partial class Authentification : Window
    {
        NpgsqlConnection conx;

        public Authentification()
        {
            InitializeComponent();
            conx = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=gg_db;");

        }
        string login;
        string password;
        string defaultLogin = "admin";
        string defaultPassword = "admin";



        private void bt_connect_Click(object sender, RoutedEventArgs e)
        {            
            login = tb_login.Text.ToString();
            password = pwd_auth.Password;
            CompareLoginPassword(); 
        }


        public void CompareLoginPassword()
        {
            // Vérification si le login et le mot de passe correspondent aux valeurs par défaut
            if (login == defaultLogin && password == defaultPassword)
            {
                lb_message.Content = "correct";
                lb_message.Foreground = Brushes.Green;
                //basculer vers une nouvelle fenetre
            }
            else
            {
                //affichage message sur lb_message au cas ou identifiant et mot de passe contient erreur
                lb_message.Content = "login et mdp érroné";
                lb_message.Foreground = Brushes.Red;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void tb_login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
            
            

    }
}
