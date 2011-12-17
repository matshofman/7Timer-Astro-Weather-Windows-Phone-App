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
using Microsoft.Phone.Controls;
using AstroPanel.Data;
using Newtonsoft.Json;
using System.Device.Location;

namespace AstroPanel
{

    // Weather Icons by http://superdit.com/2011/10/13/60-free-mobile-application-style-icon-sets/ MerlinTheRed under the Creative Commons BY-NC-ND 3.0 license

    public partial class MainPage : PhoneApplicationPage
    {
        private GeoCoordinateWatcher watcher;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            SkyListBox.Visibility = Visibility.Collapsed;

            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default); 
            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            watcher.Start();

            ApplicationBar.IsVisible = true;

        }
        
        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Disabled || e.Status == GeoPositionStatus.NoData)
            {
                LoadingText.Text = "Unable to find your location.";
                ProgressBar.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (e.Status == GeoPositionStatus.Initializing)
            {
                LoadingText.Text = "Looking for your location...";
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            watcher.Stop();

            double latitude = e.Position.Location.Latitude;
            double longitude = e.Position.Location.Longitude;
            double altitude = e.Position.Location.Altitude;

            Uri url = WeatherData.GetRequestUri(latitude, longitude, altitude);

            WebClient client = new WebClient();
            client.DownloadStringAsync(url);
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadCompleted);

            LoadingText.Text = "Loading weather data...";

        }

        private void DownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(e.Result);
                SkyListBox.ItemsSource = weatherData.WeatherFragments;
                SkyListBox.Visibility = Visibility.Visible;
                ProgressBar.Visibility = Visibility.Collapsed;
                LoadingText.Visibility = Visibility.Collapsed;
            }
        }

        private void Help_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/HelpPage.xaml", UriKind.Relative));
        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
    }
}