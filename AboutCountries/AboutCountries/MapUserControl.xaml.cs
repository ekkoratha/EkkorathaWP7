using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;

namespace AboutCountries
{
    public partial class MapUserControl : UserControl
    {
        private GeoCoordinate mapCenter;
        GeoCoordinateWatcher myCoordinateWatcher; 
        double _latitude=0.0, _longitude=0.0;
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
       

        public MapUserControl()
        {
            InitializeComponent();
            //// Retrieve the center of the current map view.
            ////mapCenter = map1.Center;
            //double zoom;
            //zoom = map1.ZoomLevel;
            //map1.ZoomLevel = zoom + 4;
            //mapCenter = new GeoCoordinate(51.36, 0.0);  
            
            //Latitude = 51.36;
            //Longitude = 0.0;           
        }
        public void display()
        {
            map1.Center = new GeoCoordinate(Latitude, Longitude);
            map1.ZoomLevel = 5;
            //map1.ZoomBarVisibility = Visibility.Visible;
            myCoordinateWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            myCoordinateWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(myCoordinateWatcher_PositionChanged);

            mapCenter = map1.Center;
            // Create a pushpin to put at the center of the view.
            Pushpin pin1 = new Pushpin();

            pin1.Style = this.Resources["PushpinStyle"] as Style;

            pin1.Location = mapCenter;
            map1.Children.Add(pin1);
            map1.SetView(mapCenter,6.0);
        }
        void myCoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e) 
        {             
            if (!e.Position.Location.IsUnknown)                  
            { 
                Latitude = e.Position.Location.Latitude; 
                Longitude = e.Position.Location.Longitude; 
                map1.Center = new GeoCoordinate(Latitude, Longitude);
                
            } 
        }         
    }
}
