//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using Microsoft.Web.WebView2.Wpf;
//using Microsoft.Web.WebView2.Core;
//using System.IO;
//using GMap.NET;
//using GMap.NET.WindowsPresentation;

//namespace Projet_2_GoGreen
//{
//    /// <summary>
//    /// Interaction logic for Map.xaml
//    /// </summary>
//    public partial class Map : Window
//    {
//        public Map()
//        {
//            InitializeComponent();
//            // webView.Source = new Uri("https://maps.google.com/maps?key=ATAO_ETO_LE_KEY");
//        }

//        private void bt_retour_map_Click(object sender, RoutedEventArgs e)
//        {
//            Acceuil_client acceuil_Client = new Acceuil_client();
//            acceuil_Client.Show();
//            this.Hide();

//        }

//        private void bt_se_deconnecter_map_Click(object sender, RoutedEventArgs e)
//        {
//            MainWindow mainWindow = new MainWindow();
//            mainWindow.Show();
//            this.Hide();
//        }
//        ////==============================================Initialisation_map======================================================
//        //private void mapView_Loaded(object sender, RoutedEventArgs e)
//        //{
//        //    GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
//        //    // choose your provider here
//        //    mapView.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
//        //    mapView.MinZoom = 2;
//        //    mapView.MaxZoom = 17;
//        //    // whole world zoom
//        //    mapView.Zoom = 2;
//        //    // lets the map use the mousewheel to zoom
//        //    mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
//        //    // lets the user drag the map
//        //    mapView.CanDragMap = true;
//        //    // lets the user drag the map with the left mouse button
//        //    mapView.DragButton = MouseButton.Left;
//        //}
//        ////====================================================FIN================================================================

//        //==============================================Map_centered_on_Antananarivo======================================================
//        private void mapView_Loaded(object sender, RoutedEventArgs e)
//        {
//            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
//            // choose your provider here
//            mapView.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
//            mapView.MinZoom = 2;
//            mapView.MaxZoom = 17;



//            // Set Antananarivo coordinates (latitude and longitude)
//            double antananarivoLat = -18.8792;
//            double antananarivoLng = 47.5079;

//            //___________________________________TEST________________________________________
//            // Set the coordinates for the markers
//            double marker1Lat = 41.881832;
//            double marker1Lng = -87.623177;

//            double marker2Lat = 40.712776;
//            double marker2Lng = -74.005974;

//            double marker3Lat = 51.5074;
//            double marker3Lng = -0.1278;

//            double marker4Lat = 48.8566;
//            double marker4Lng = 2.3522;

//            double marker5Lat = 34.052235;
//            double marker5Lng = -118.243683;

//            // Create the markers
//            GMap.NET.WindowsPresentation.GMapMarker marker1 = CreateMarker(marker1Lat, marker1Lng, "icons8-location-96.png");
//            GMap.NET.WindowsPresentation.GMapMarker marker2 = CreateMarker(marker2Lat, marker2Lng, "icons8-location-96.png");
//            GMap.NET.WindowsPresentation.GMapMarker marker3 = CreateMarker(marker3Lat, marker3Lng, "icons8-location-96.png");
//            GMap.NET.WindowsPresentation.GMapMarker marker4 = CreateMarker(marker4Lat, marker4Lng, "icons8-location-96.png");
//            GMap.NET.WindowsPresentation.GMapMarker marker5 = CreateMarker(marker5Lat, marker5Lng,"icons8-location-96.png");

//            // Add the markers to the map
//            mapView.Markers.Add(marker1);
//            mapView.Markers.Add(marker2);
//            mapView.Markers.Add(marker3);
//            mapView.Markers.Add(marker4);
//            mapView.Markers.Add(marker5);

//            //________________________________FIN_TEST_______________________________________


//            // Set the center of the map to Antananarivo
//            mapView.Position = new GMap.NET.PointLatLng(antananarivoLat, antananarivoLng);
//            //marker = new GMap.NET.WindowsPresentation.GMapMarker(new GMap.NET.PointLatLng(latitude, longitude));
//            GMap.NET.WindowsPresentation.GMapMarker marker = CreateMarker(antananarivoLat, antananarivoLng, "icons8-location-96.png");
//            // Create the markers

//            //marker.Shape = new Ellipse
//            //{
//            //    Width = 10,
//            //    Height = 10,
//            //    Stroke = Brushes.Red,
//            //    Fill = Brushes.Red,
//            //    Opacity = 0.6
//            //};


//            // Add the marker to the map
//            mapView.Markers.Add(marker);

//            // lets the map use the mousewheel to zoom
//            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;

//            // Set the desired zoom level
//            int zoomLevel = 7; // Adjust this value as needed
//            mapView.Zoom = zoomLevel;

//            // lets the map use the mousewheel to zoom
//            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
//            // lets the user drag the map
//            mapView.CanDragMap = true;
//            // lets the user drag the map with the left mouse button
//            mapView.DragButton = MouseButton.Left;
//            mapView.MouseDoubleClick += mapView_MouseDoubleClick;
//            marker.Shape.MouseLeftButtonDown += Marker_MouseLeftButtonDown;
//            marker.Shape.MouseEnter += Marker_MouseEnter;
//            marker.Shape.MouseLeave += Marker_MouseLeave;

//        }
//        //==============================================FIN======================================================

//        //private GMap.NET.WindowsPresentation.GMapMarker CreateMarker(double latitude, double longitude)
//        //{
//        //    GMap.NET.WindowsPresentation.GMapMarker marker = new GMap.NET.WindowsPresentation.GMapMarker(new GMap.NET.PointLatLng(latitude, longitude));
//        //    marker.Shape = CreateMarkerShape();
//        //    marker.Offset = new System.Windows.Point(-5, -5);
//        //    return marker;
//        //}

//        //private Shape CreateMarkerShape()
//        //{
//        //    var ellipse = new Ellipse
//        //    {
//        //        Width = 10,
//        //        Height = 10,
//        //        Stroke = Brushes.Red,
//        //        Fill = Brushes.Red,
//        //        Opacity = 0.6
//        //    };
//        //    return ellipse;
//        //}

//        private GMap.NET.WindowsPresentation.GMapMarker CreateMarker(double latitude, double longitude, string imagePath)
//        {
//            var imageMarker = new GMap.NET.WindowsPresentation.GMapMarker(new GMap.NET.PointLatLng(latitude, longitude));

//            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
//            imageMarker.Shape = new System.Windows.Controls.Image
//            {
//                Source = bitmapImage,
//                Width = 20,
//                Height = 20,
//                Opacity = 0.6
//            };

//            return imageMarker;
//        }

//        //private void mapView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
//        //{
//        //    // Récupérer les coordonnées du double-clic
//        //    GMap.NET.PointLatLng point = mapView.FromLocalToLatLng((int)e.GetPosition(mapView).X, (int)e.GetPosition(mapView).Y);

//        //    // Utilisez les valeurs de point.Lat et point.Lng pour les coordonnées de latitude et de longitude
//        //    double latitude = point.Lat;
//        //    double longitude = point.Lng;

//        //    // Faites quelque chose avec les coordonnées récupérées
//        //    // Par exemple, afficher les coordonnées dans une boîte de dialogue
//        //    MessageBox.Show($"Latitude: {latitude}\nLongitude: {longitude}");
//        //}

//        private void mapView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
//        {
//            GMap.NET.PointLatLng mousePosition = mapView.FromLocalToLatLng((int)e.GetPosition(mapView).X, (int)e.GetPosition(mapView).Y);

//            double latitude = mousePosition.Lat;
//            double longitude = mousePosition.Lng;

//            GMap.NET.WindowsPresentation.GMapMarker marker = CreateMarker(latitude, longitude, "icons8-location-96.png");

//            mapView.Markers.Add(marker);
//        }


//private void Marker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//{
//    // Show a message box with text, image, and buttons
//    MessageBoxResult result = MessageBox.Show("This is a message box with text, image, and buttons.",
//                                              "Marker Clicked",
//                                              MessageBoxButton.YesNoCancel,
//                                              MessageBoxImage.Information);

//    // Handle the button click based on the result
//    switch (result)
//    {
//        case MessageBoxResult.Yes:
//            // User clicked 'Yes' button
//            // Perform action
//            break;
//        case MessageBoxResult.No:
//            // User clicked 'No' button
//            // Perform action
//            break;
//        case MessageBoxResult.Cancel:
//            // User clicked 'Cancel' button
//            // Perform action
//            break;
//    }

//}
//        private void Marker_MouseEnter(object sender, MouseEventArgs e)
//        {
//            if (sender is Shape shape)
//            {
//               // shape.Fill = Brushes.Green; // Change the marker's fill color to indicate it's clickable
//                shape.Cursor = Cursors.Hand; // Set the cursor to Hand
//            }
//        }

//        private void Marker_MouseLeave(object sender, MouseEventArgs e)
//        {
//            if (sender is Shape shape)
//            {
//               // shape.Fill = Brushes.Red; // Change the marker's fill color back to the original color
//                shape.Cursor = Cursors.Arrow; // Restore the default cursor
//            }
//        }

//    }
//}


////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Windows;
////using System.Windows.Controls;
////using System.Windows.Data;
////using System.Windows.Documents;
////using System.Windows.Input;
////using System.Windows.Media;
////using System.Windows.Media.Imaging;
////using System.Windows.Shapes;
////using Microsoft.Web.WebView2.Wpf;
////using Microsoft.Web.WebView2.Core;
////using GMap.NET.WindowsPresentation;
////using GMap.NET;
////using System.IO;



////        private void mapView_Loaded(object sender, RoutedEventArgs e)
////        {


////            // Generate 5 random markers
////            for (int i = 0; i < 5; i++)
////            {
////                double randomLat = GetRandomLatitude();
////                double randomLng = GetRandomLongitude();

////                GMapMarker randomMarker = CreateMarker(randomLat, randomLng);
////                mapView.Markers.Add(randomMarker);
////            }

////            // Add the same events and shapes to all markers
////            foreach (var existingMarker in mapView.Markers)
////            {
////                var shape = (Ellipse)existingMarker.Shape;
////                shape.MouseLeftButtonDown += Marker_MouseLeftButtonDown;
////                shape.MouseEnter += Marker_MouseEnter;
////                shape.MouseLeave += Marker_MouseLeave;
////            }

////            mapView.DragButton = MouseButton.Left;
////            mapView.MouseDoubleClick += mapView_MouseDoubleClick;
////            marker.Shape.MouseLeftButtonDown += Marker_MouseLeftButtonDown;
////            marker.Shape.MouseEnter += Marker_MouseEnter;
////            marker.Shape.MouseLeave += Marker_MouseLeave;
////        }




////        private GMapMarker CreateMarker(double latitude, double longitude)
////        {
////            GMapMarker marker = new GMapMarker(new PointLatLng(latitude, longitude));
////            marker.Shape = CreateMarkerShape();
////            marker.Offset = new System.Windows.Point(-5, -5);
////            return marker;
////        }

////        private Ellipse CreateMarkerShape()
////        {
////            var ellipse = new Ellipse
////            {
////                Width = 10,
////                Height = 10,
////                Stroke = Brushes.Red,
////                Fill = Brushes.Red,
////                Opacity = 0.6
////            };

////            return ellipse;
////        }


////        private double GetRandomLatitude()
////        {
////            Random random = new Random();
////            double minLat = -90.0;
////            double maxLat = 90.0;
////            return random.NextDouble() * (maxLat - minLat) + minLat;
////        }

////        private double GetRandomLongitude()
////        {
////            Random random = new Random();
////            double minLng = -180.0;
////            double maxLng = 180.0;
////            return random.NextDouble() * (maxLng - minLng) + minLng;
////        }

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
    public partial class Map : Window
    {
        private GMap.NET.WindowsPresentation.GMapMarker marker;

        public Map()
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

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            mapView.MapProvider = GMapProviders.GoogleHybridMap;
            mapView.MaxZoom = 22;
            // Set Antananarivo coordinates (latitude and longitude)
            double antananarivoLat = -18.8792;
            double antananarivoLng = 47.5079;
            // Set the center of the map to Antananarivo
            mapView.Position = new PointLatLng(antananarivoLat, antananarivoLng);
            // Set the desired zoom level
            int zoomLevel = 12; // Adjust this value as needed
            mapView.Zoom = zoomLevel;
            string imagePath = "icons8-location-96.png"; // Chemin relatif de l'image dans le dossier du projet
            string projectPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); // Chemin du dossier du projet
            string absolutePath = System.IO.Path.Combine(projectPath, imagePath); // Chemin absolu de l'image
            marker = CreateMarker(antananarivoLat, antananarivoLng, absolutePath);
            mapView.Markers.Add(marker);
            
            treeMarker_creation(absolutePath);

            // Add the events to the marker's shape
            marker.Shape.MouseEnter += Marker_MouseEnter;
            marker.Shape.MouseLeave += Marker_MouseLeave;
            mapView.DragButton = MouseButton.Left;
            marker.Shape.MouseLeftButtonDown += Marker_MouseLeftButtonDown;
            //mapView.MouseDoubleClick += mapView_MouseDoubleClick;
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
                           "JOIN public.position ON public.arbre.id = public.position.arbreid WHERE arbre.clientid='"+id_user+"'";

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
                    mapView.Markers.Add(marker);
                }

                conn.Close();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"erreur query");
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



        //private void mapView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    PointLatLng mousePosition = mapView.FromLocalToLatLng((int)e.GetPosition(mapView).X, (int)e.GetPosition(mapView).Y);

        //    double latitude = mousePosition.Lat;
        //    double longitude = mousePosition.Lng;

        //    string imagePath = "icons8-location-96.png"; // Chemin relatif de l'image dans le dossier du projet
        //    string projectPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); // Chemin du dossier du projet
        //    string absolutePath = System.IO.Path.Combine(projectPath, imagePath); // Chemin absolu de l'image
        //    GMap.NET.WindowsPresentation.GMapMarker newMarker = CreateMarker(latitude, longitude, absolutePath);
        //    MessageBoxResult result = MessageBox.Show($"Un arbre a-t-il été planté à cette position ? \n Latitude: {latitude}\nLongitude: {longitude}", "Planter un arbre", MessageBoxButton.YesNo, MessageBoxImage.Question);
            

        //    switch (result)
        //    {
        //        case MessageBoxResult.Yes:
        //            mapView.Markers.Add(newMarker);

        //            // Add the events to the new marker's shape
        //            newMarker.Shape.MouseEnter += Marker_MouseEnter;
        //            newMarker.Shape.MouseLeave += Marker_MouseLeave;
        //            newMarker.Shape.MouseLeftButtonDown += Marker_MouseLeftButtonDown;
        //            //inserer_arbre_position(latitude, longitude);
        //            Test_op oper = new Test_op();
        //            oper.Show();


        //            break;
        //        case MessageBoxResult.No:

        //            break;

        //    }

        //}

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


