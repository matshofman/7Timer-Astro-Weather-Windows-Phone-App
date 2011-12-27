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
using Newtonsoft.Json;
using System.Device.Location;
using AstroPanel.Models;

namespace AstroPanel
{

    public partial class MainPage : PhoneApplicationPage
    {
        private GeoCoordinateWatcher watcher;
        private AppSettings settings;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            SkyListBox.Visibility = Visibility.Collapsed;
            ApplicationBar.IsVisible = true;
            settings = new AppSettings();

            if (settings.GPSModeSetting == GPSMode.Automatic)
            {
                GetLocationAndWeatherData();
            }
            else
            {
                GetWeatherData();
            }

        }

        private void GetLocationAndWeatherData()
        {
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            watcher.Start();
        }

        private void GetWeatherData()
        {
            double latitude = settings.GPSLatitudeSetting;
            double longitude = settings.GPSLongitudeSetting;
            TemperatureUnit temperature = settings.TemperatureUnitSetting;

            LatitudeCoordinate.Text = Math.Abs(latitude).ToString("0.000").Replace(",", ".");
            LongitudeCoordinate.Text = Math.Abs(longitude).ToString("0.000").Replace(",", ".");

            if (latitude >= 0.0)
                LatitudeHemisphere.Text = "N";
            else
                LatitudeHemisphere.Text = "S";

            if (longitude >= 0.0)
                LongitudeHemisphere.Text = "E";
            else
                LongitudeHemisphere.Text = "W";

            Uri url = WeatherData.GetRequestUri(latitude, longitude, temperature);

            WebClient webclient = new WebClient();
            webclient.DownloadStringAsync(url);
            webclient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webclient_DownloadStringCompleted);

            LoadingText.Text = "Loading weather data...";
        }

        private void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Disabled || e.Status == GeoPositionStatus.NoData)
            {
                LoadingText.Text = "Unable to find your location";
                ProgressBar.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (e.Status == GeoPositionStatus.Initializing)
            {
                LoadingText.Text = "Looking for your location...";
            }
        }

        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            watcher.Stop();

            settings.GPSLatitudeSetting = e.Position.Location.Latitude;
            settings.GPSLongitudeSetting = e.Position.Location.Longitude;

            GetWeatherData();
        }

        private void webclient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(e.Result);
                    SkyListBox.ItemsSource = weatherData.WeatherFragments;
                    SkyListBox.Visibility = Visibility.Visible;
                    ProgressBar.Visibility = Visibility.Collapsed;
                    LoadingText.Visibility = Visibility.Collapsed;
                }
                catch (JsonSerializationException)
                {
                    LoadingText.Text = "Unable to get weather data for your location";
                    ProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        #region Help Events

        private void HelpCloudCover_Click(object sender, EventArgs e)
        {
            string title = "Cloud Cover";
            string text = "A pie-chart of cloud cover, blue is for clear while white is for clouds. The more blue the better.";
            MessageBox.Show(text, title, MessageBoxButton.OK);
        }

        private void HelpSeeing_Click(object sender, EventArgs e)
        {
            string title = "Astronomical Seeing";
            string text = "The smaller and bluer the dot, the better the seeing condition is. Astronomical seeing refers to the blurring and twinkling of objects in the sky caused by turbulence in the air.";
            MessageBox.Show(text, title, MessageBoxButton.OK);
        }

        private void HelpTransparency_Click(object sender, EventArgs e)
        {
            string title = "Transparency";
            string text = "The atmospheric transparency shown in bars, the bluer and less bars there are the better the transparency is. Basically a bad transparency means you will see less stars because less light will be able to pass through the Earth's atmosphere.";
            MessageBox.Show(text, title, MessageBoxButton.OK);
        }

        private void HelpTemperature_Click(object sender, EventArgs e)
        {
            string title = "Temperature";
            string text = "The air temperature at 2 meter or 6 feet in Celcius or Farenheit. This can be adjusted in the settings menu.";
            MessageBox.Show(text, title, MessageBoxButton.OK);
        }

        private void HelpHumidity_Click(object sender, EventArgs e)
        {
            string title = "Humidity";
            string text = "The air humidity at 2 meter or 6 feet. There is an increased chance of dew from 80% and higher.";
            MessageBox.Show(text, title, MessageBoxButton.OK);
        }

        private void HelpPrecipitation_Click(object sender, EventArgs e)
        {
            string title = "Precipitation";
            string text = "Expected precipitation in the form of rain or snow.";
            MessageBox.Show(text, title, MessageBoxButton.OK);
        }

        private void HelpWind_Click(object sender, EventArgs e)
        {
            string title = "Wind";
            string text = "The amount of wind and it's direction. In case the wind is picking up to a fresh breeze or higher the arrow starts to color from yellow to red.";
            MessageBox.Show(text, title, MessageBoxButton.OK);
        }

        #endregion

        #region ApplicationBar

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void Help_Click(object sender, EventArgs e)
        {
            string title = "Help";
            string text = "To get an explanation about the data click on one of the row names to view it.";
            MessageBox.Show(text, title, MessageBoxButton.OK);
        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        #endregion
    }
}