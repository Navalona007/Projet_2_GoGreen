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
        ConnectDB conx;
        AdminClass admin;
        OperateurClass operateur;
        ClientClass client;


        public Authentification()
        {
            InitializeComponent();

        }
        string login="";
        string password="";
        //string defaultLogin = "admin";
        //string defaultPassword = "admin";



        private void bt_connect_Click(object sender, RoutedEventArgs e)
        {            
            login = tb_login.Text.ToString();
            password = CalculateMD5Hash(pwd_auth.Password);
            CompareLoginPassword(); 
        }
        //private static List

        public void CompareLoginPassword()
        {
            admin = new AdminClass();
            operateur = new OperateurClass();
            client = new ClientClass();

            

            

            // Vérification si le login et le mot de passe correspondent aux valeurs par défaut
            if (true)
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


        private string CalculateMD5Hash(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        } //end CalculateMD5Hash




    }
}
