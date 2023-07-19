﻿using System;
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
           // webView.Source = new Uri("https://maps.google.com/maps?key=ATAO_ETO_LE_KEY");
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
        //private void mapView_Loaded(object sender, RoutedEventArgs e)
        //{
        //    GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
        //    // choose your provider here
        //    mapView.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
        //    mapView.MinZoom = 2;
        //    mapView.MaxZoom = 17;
        //    // whole world zoom
        //    mapView.Zoom = 2;
        //    // lets the map use the mousewheel to zoom
        //    mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
        //    // lets the user drag the map
        //    mapView.CanDragMap = true;
        //    // lets the user drag the map with the left mouse button
        //    mapView.DragButton = MouseButton.Left;
        //}
        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            // choose your provider here
            mapView.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
            mapView.MinZoom = 2;
            mapView.MaxZoom = 17;



            // Set Antananarivo coordinates (latitude and longitude)
            double antananarivoLat = -18.8792;
            double antananarivoLng = 47.5079;
            

            // Set the center of the map to Antananarivo
            mapView.Position = new GMap.NET.PointLatLng(antananarivoLat, antananarivoLng);
            GMap.NET.WindowsPresentation.GMapMarker marker = new GMap.NET.WindowsPresentation.GMapMarker(new GMap.NET.PointLatLng(antananarivoLat, antananarivoLng));
            marker.Shape = new Ellipse
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Red,
                Fill = Brushes.Red,
                Opacity = 0.6
            };

            // Add the marker to the map
            mapView.Markers.Add(marker);

            // lets the map use the mousewheel to zoom
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;

            // Set the desired zoom level
            int zoomLevel = 12; // Adjust this value as needed
            mapView.Zoom = zoomLevel;

            // lets the map use the mousewheel to zoom
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            // lets the user drag the map
            mapView.CanDragMap = true;
            // lets the user drag the map with the left mouse button
            mapView.DragButton = MouseButton.Left;
        }

    }
}




