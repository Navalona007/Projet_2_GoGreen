﻿using Npgsql;
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
    /// Interaction logic for Test_op.xaml
    /// </summary>
    public partial class Test_op : Window
    {

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
        }

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

        Dictionary<string, int> liste_espece;
        public void list_espece ()
        {
            liste_espece = null;
            liste_espece = new Dictionary<string, int>();
            var conn = GetConnection();
            conn.Open();

            String query = "SELECT id, name FROM espece";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste_espece.Add((string)reader["name"], (int)reader["id"]);
            }
            conn.Close();
        }

        public void insert_espece(int id, string valueInsert)
        {
            var conn = GetConnection();
            conn.Open();

            String query = "INSERT INTO public.espece (typeid, name)    VALUES ('"+id+"','" + valueInsert + "');";
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
            liste_type = new Dictionary<string, int> ();            
            var conn = GetConnection();
            conn.Open();

            String query = "SELECT * FROM type";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste_id_type.Add(reader["id"].ToString(), (string)reader["name"]);
                liste_type.Add((string)reader["name"], (int)reader["id"]);
            }
            conn.Close();
        }
        public void insert_type(string valueInsert) // inserer lieu
        {
            var conn = GetConnection();
            conn.Open();

            String query = "INSERT INTO public.type (name) VALUES ('" + valueInsert + "'); ";
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


        private void bt_inserer_Click(object sender, RoutedEventArgs e)
        {
            list_type();
            list_espece();
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

                if (string.IsNullOrWhiteSpace(date_plantation) ||
                    string.IsNullOrWhiteSpace(diametre) ||
                    string.IsNullOrWhiteSpace(hauteur) ||
                    string.IsNullOrWhiteSpace(espece) ||
                    string.IsNullOrWhiteSpace(type) ||
                    string.IsNullOrWhiteSpace(zone) ||
                    string.IsNullOrWhiteSpace(latitude) ||
                    string.IsNullOrWhiteSpace(longitude))
                {
                    lb_message.Content = "Veuillez remplir tous les champs!";                   
                } 
                else if (liste_type.ContainsKey(type) && !liste_espece.ContainsKey(espece))
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

                else if (!liste_type.ContainsKey(type) && liste_espece.ContainsKey(espece))
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

                    }

                }

            } 

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
    }
}
