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
using SevenTimerAstroWeather.Models;
using System.Windows.Navigation;
using System.Globalization;

namespace SevenTimerAstroWeather
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        private AppSettings settings;

        public SettingsPage()
        {
            InitializeComponent();
            settings = new AppSettings();

            if (settings.GPSModeSetting == GPSMode.Automatic)
            {
                GPSModeSwitch.IsChecked = true;
                LatitudeTextBox.IsEnabled = false;
                LongitudeTextBox.IsEnabled = false;
            }
            else
            {
                GPSModeSwitch.IsChecked = false;
                LatitudeTextBox.IsEnabled = true;
                LongitudeTextBox.IsEnabled = true;
            }

            LatitudeTextBox.Text = settings.GPSLatitudeSetting.ToString("0.000").Replace(",", ".");
            LongitudeTextBox.Text = settings.GPSLongitudeSetting.ToString("0.000").Replace(",", ".");

            if (settings.TemperatureUnitSetting == TemperatureUnit.C)
            {
                CelciusRadioButton.IsChecked = true;
            }
            else if (settings.TemperatureUnitSetting == TemperatureUnit.F)
            {
                FahrenheitRadioButton.IsChecked = true;
            }

        }

        private void GPSModeSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (GPSModeSwitch.IsChecked == true)
            {
                LatitudeTextBox.IsEnabled = false;
                LongitudeTextBox.IsEnabled = false;
            }
            else
            {
                LatitudeTextBox.IsEnabled = true;
                LongitudeTextBox.IsEnabled = true;
            }

        }

        #region ApplicationBar

        private void Save_Click(object sender, EventArgs e)
        {
            string latitudeValue = LatitudeTextBox.Text.Replace(",", ".");
            string longitudeValue = LongitudeTextBox.Text.Replace(",", ".");
            double latitude = 0;
            double longitude = 0;
            bool valid = false;

            if (GPSModeSwitch.IsChecked == true)
            {
                valid = true;
            }
            else
            {
                try
                {
                    latitude = Double.Parse(latitudeValue, CultureInfo.InvariantCulture);
                    longitude = Double.Parse(longitudeValue, CultureInfo.InvariantCulture);

                    if (latitude >= -90 && latitude <= 90 && longitude >= -180 && longitude <= 180)
                    {
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                    }
                }
                catch (Exception)
                {
                    valid = false;
                }
            }

            if (valid)
            {

                if (GPSModeSwitch.IsChecked == true)
                {
                    settings.GPSModeSetting = GPSMode.Automatic;
                }
                else
                {
                    settings.GPSModeSetting = GPSMode.Manual;
                    settings.GPSLatitudeSetting = latitude;
                    settings.GPSLongitudeSetting = longitude;
                }

                if (CelciusRadioButton.IsChecked == true)
                {
                    settings.TemperatureUnitSetting = TemperatureUnit.C;
                }
                else
                {
                    settings.TemperatureUnitSetting = TemperatureUnit.F;
                }

                settings.SettingsModified = true;
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("The latitude and longitude don't represent a valid location, ensure that the latitude is between -90 and 90 and the longitude is between -180 and 180.", "Invalid Location", MessageBoxButton.OK);
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        #endregion
    }
}