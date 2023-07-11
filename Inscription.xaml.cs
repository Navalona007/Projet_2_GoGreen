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
    /// Interaction logic for Inscription.xaml
    /// </summary>
    public partial class Inscription : Window
    {
        public Inscription()
        {
            InitializeComponent();
        }

        private void bt_inscrire_Copy_Click(object sender, RoutedEventArgs e)
        {
            tb_nom_inscription.Text = "";
            tb_prenom_inscription.Text = "";
            tb_mail_inscription.Text = "";
            tb_mdp_inscription.Password = "";
            tb_confirmer_mdp_inscription.Password = "";
            tb_reference_inscription.Text = "";
            lb_confirmation_mdp.Content = "";
            label_champ_vide.Content = "";
        }

        private void tf__TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void bt_inscrire_inscription_Click(object sender, RoutedEventArgs e)
        {
            //Form1 fenetre = new Form1();

            string nom = tb_nom_inscription.Text;
            string prenom = tb_prenom_inscription.Text;
            string motDePasse = tb_mdp_inscription.Password;

            //if (nom.Length == 0)
            if (string.IsNullOrEmpty(nom))
            {
                label_champ_vide.Content = "Remplir le(s) champ(s).";
                label_champ_vide.Foreground = Brushes.Red;
            }
            else
            {
                Console.WriteLine("Nom: " + nom);
                label_champ_vide.Content = "";
            }

            

            string mail = tb_mail_inscription.Text;
            if (string.IsNullOrEmpty(mail))
            {
                label_champ_vide.Content = "Remplir le(s) champ(s).";
                label_champ_vide.Foreground = Brushes.Red;
            }
            else
            {
                Console.WriteLine("Mail: " + mail);
                label_champ_vide.Content = "";
            }

            string reference = tb_reference_inscription.Text;
            if (string.IsNullOrEmpty(reference))
            {
                label_champ_vide.Content = "Remplir le(s) champ(s).";
                label_champ_vide.Foreground = Brushes.Red;
            }
            else
            {
                Console.WriteLine("Réference: " + reference);
                label_champ_vide.Content = "";
            }

            

            if (string.IsNullOrEmpty(motDePasse))
            {
                label_champ_vide.Content = "Le champ mot de passe ne peut pas être vide.";
                label_champ_vide.Foreground = Brushes.Red;
            }

            else if (motDePasse.Length < 8)
            {
                label_champ_vide.Content = "Le mot de passe doit contenir au moins 8 caractères.";
                label_champ_vide.Foreground = Brushes.Red;
            }
            else
            {

                Console.WriteLine("Mot de passe OK");

                //label_champ_vide.Content = "";
            }


            string confirmerMotDePasse = tb_confirmer_mdp_inscription.Password;

            if (string.IsNullOrEmpty(motDePasse) || string.IsNullOrEmpty(confirmerMotDePasse))
            {
                label_champ_vide.Content = "Les champs mot de passe ne peuvent pas être vides.";
                label_champ_vide.Foreground = Brushes.Red;
            }
            else if (motDePasse != confirmerMotDePasse)
            {
                lb_confirmation_mdp.Content = "Les champs mot de passe ne correspondent pas.";
                lb_confirmation_mdp.Foreground = Brushes.Red;
            }
            else
            {

                Console.WriteLine("Mot de passe OK ");
                Console.WriteLine("Confirmer mot de passe OK ");

                lb_confirmation_mdp.Content = "";
            }

        }


    }
}
