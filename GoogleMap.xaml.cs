using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Windows.Shapes;
using System.Collections.Generic;
using Npgsql;

namespace Projet_2_GoGreen
{
    public partial class GoogleMap : Window
    {
        private GMap.NET.WindowsPresentation.GMapMarker marker;

        public GoogleMap()
        {
            InitializeComponent();
            //id_user = lb_id_user.Content.ToString();
            //lb_nom_client.Content = id_user;
        }
        private void bt_retour_map_Click(object sender, RoutedEventArgs e)
        {
            Acceuil_client acceuil_Client = new Acceuil_client();
            acceuil_Client.Show();
            this.Hide();

        }

        private void mapViewOp_Loaded(object sender, RoutedEventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            mapViewOp.MapProvider = GMapProviders.GoogleHybridMap;
            mapViewOp.MaxZoom = 22;
            // Set Antananarivo coordinates (latitude and longitude)
            double antananarivoLat = -18.8792;
            double antananarivoLng = 47.5079;
            // Set the center of the map to Antananarivo
            mapViewOp.Position = new PointLatLng(antananarivoLat, antananarivoLng);
            // Set the desired zoom level
            int zoomLevel = 12; // Adjust this value as needed
            mapViewOp.Zoom = zoomLevel;
            string imagePath = "icons8-location-96.png"; // Chemin relatif de l'image dans le dossier du projet
            string projectPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); // Chemin du dossier du projet
            string absolutePath = System.IO.Path.Combine(projectPath, imagePath); // Chemin absolu de l'image
            marker = CreateMarker(antananarivoLat, antananarivoLng, absolutePath);
            mapViewOp.Markers.Add(marker);

            treeMarker_creation(absolutePath);

            // Add the events to the marker's shape
            marker.Shape.MouseEnter += Marker_MouseEnter;
            marker.Shape.MouseLeave += Marker_MouseLeave;
            mapViewOp.DragButton = MouseButton.Left;
            marker.Shape.MouseLeftButtonDown += Marker_MouseLeftButtonDown;
            mapViewOp.MouseDoubleClick += mapView_MouseDoubleClick;
        }
        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=root;Database=gg_db");
        }
        string id_user = null;
        List<Arbre> arbre_client = new List<Arbre>();
        private void treeMarker_creation(string pin_path)
        {

            //string imagePath = "icons8-location-96.png";
            //id_user = lb_id_user.Content.ToString();
            var conn = GetConnection();
            conn.Open();
            //ConnectDB con = new ConnectDB();
            // Utilisation de paramètres pour éviter les problèmes de sécurité liés aux injections SQL
            string query = "SELECT arbre.id, espece.name_espece, position.latitude, position.longitude " +
                           "FROM public.arbre " +
                           "JOIN public.espece ON public.espece.id = public.arbre.especeid " +
                           "JOIN public.position ON public.arbre.id = public.position.arbreid WHERE arbre.clientid='" + id_user + "'";

            //con.launchReader(query);
            NpgsqlCommand command = new NpgsqlCommand(query, conn);

            // Utilisation de paramètres pour éviter les problèmes de sécurité liés aux injections SQL
            //command.Parameters.AddWithValue("@clientid", int.Parse(id_personne));
            try
            {


                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Arbre arbre = new Arbre();

                    arbre.id_arbre = reader["id"].ToString();
                    arbre.espece = reader["name_espece"].ToString();
                    arbre.latitude = reader.GetDouble(reader.GetOrdinal("latitude"));
                    arbre.longitude = reader.GetDouble(reader.GetOrdinal("longitude"));

                    arbre_client.Add(arbre);
                    marker = CreateMarker(arbre.latitude, arbre.longitude, pin_path);
                    mapViewOp.Markers.Add(marker);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "erreur query");
            }

        }

        private GMap.NET.WindowsPresentation.GMapMarker CreateMarker(double latitude, double longitude, string imagePath)
        {
            var imageMarker = new GMap.NET.WindowsPresentation.GMapMarker(new GMap.NET.PointLatLng(latitude, longitude));

            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            System.Windows.Controls.Image image = new System.Windows.Controls.Image
            {
                Source = bitmapImage,
                Width = 30,
                Height = 30,
                Opacity = 1,
                Cursor = Cursors.Hand // Modifier le curseur de la souris pour le marqueur
            };

            // Déplacer l'image vers le haut et la gauche pour aligner la tête du pushpin avec la pointe du curseur
            double offsetX = -image.Width / 2;
            double offsetY = -image.Height;
            imageMarker.Offset = new System.Windows.Point(offsetX, offsetY);

            imageMarker.Shape = image;

            return imageMarker;
        }



        private void mapView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PointLatLng mousePosition = mapViewOp.FromLocalToLatLng((int)e.GetPosition(mapViewOp).X, (int)e.GetPosition(mapViewOp).Y);

            double latitude = mousePosition.Lat;
            double longitude = mousePosition.Lng;

            string imagePath = "icons8-location-96.png"; // Chemin relatif de l'image dans le dossier du projet
            string projectPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); // Chemin du dossier du projet
            string absolutePath = System.IO.Path.Combine(projectPath, imagePath); // Chemin absolu de l'image
            GMap.NET.WindowsPresentation.GMapMarker newMarker = CreateMarker(latitude, longitude, absolutePath);
            MessageBoxResult result = MessageBox.Show($"Un arbre a-t-il été planté à cette position ? \n Latitude: {latitude}\nLongitude: {longitude}", "Planter un arbre", MessageBoxButton.YesNo, MessageBoxImage.Question);


            switch (result)
            {
                case MessageBoxResult.Yes:
                    mapViewOp.Markers.Add(newMarker);

                    // Add the events to the new marker's shape
                    newMarker.Shape.MouseEnter += Marker_MouseEnter;
                    newMarker.Shape.MouseLeave += Marker_MouseLeave;
                    newMarker.Shape.MouseLeftButtonDown += Marker_MouseLeftButtonDown;
                    //inserer_arbre_position(latitude, longitude);
                    Test_op oper = new Test_op();
                    oper.tb_latitude.Text = latitude.ToString();
                    oper.tb_longitude.Text = longitude.ToString();
                    oper.Show();


                    break;
                case MessageBoxResult.No:

                    break;

            }

        }

        private void Marker_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Shape shape)
            {
                shape.Cursor = Cursors.Hand; // Modifier le curseur de la souris pour le marqueur
            }
        }

        private void Marker_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Shape shape)
            {
                shape.Cursor = Cursors.Arrow; // Restaurer le curseur par défaut de la souris
            }
        }

        private void Marker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Show a message box with text, image, and buttons
            MessageBoxResult result = MessageBox.Show("This is a message box with text, image, and buttons.",
                                                      "Marker Clicked",
                                                      MessageBoxButton.YesNoCancel,
                                                      MessageBoxImage.Information);

            // Handle the button click based on the result
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User clicked 'Yes' button
                    // Perform action
                    break;
                case MessageBoxResult.No:
                    // User clicked 'No' button
                    // Perform action
                    break;
                case MessageBoxResult.Cancel:
                    // User clicked 'Cancel' button
                    // Perform action
                    break;
            }

        }

        private void Mouse_sur_retour(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand; // Définit le curseur sur "Main" lors du survol
        }

        private void Mouse_leave_retour(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow; // Rétablit le curseur par défaut lorsqu'il quitte le bouton
        }
        public void position_list()
        {

        }

        private void inserer_arbre_position(double latitude, double longitude)
        {
            ConnectDB con = new ConnectDB();
            con.executeRequest("INSERT INTO position (zoneid, arbreid, latitude, longitude) VALUES('', '', '" + latitude + "', '" + longitude + "'); ");
            con.close();

        }


        private void get_value_label(object sender, EventArgs e)
        {
            id_user = lb_id_user.Content.ToString();
        }
    }
}


