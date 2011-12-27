using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace SevenTimerAstroWeather.Models
{
    public class AppSettings
    {
        IsolatedStorageSettings settings;

        // Isolated storage key names
        const string GPSModeKeyName = "GPSMode";
        const string GPSLatitudeKeyName = "GPSLatitude";
        const string GPSLongitudeKeyName = "GPSLongitude";
        const string TempratureUnitKeyName = "TempratureUnit";
        const string AppStartCounterKeyName = "AppStartCounter";
        const string SettingsModifiedKeyName = "SettingsModified";

        // The default values
        const GPSMode GPSModeDefault = GPSMode.Automatic;
        const double GPSLatitudeDefault = 0.0;
        const double GPSLongitudeDefault = 0.0;
        const TemperatureUnit TempratureUnitDefault = TemperatureUnit.C;
        const int AppStartCounterDefault = 0;
        const bool SettingsModifiedDefault = false;

        public AppSettings()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

        public T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value = defaultValue;

            if (settings.Contains(key))
            {
                value = (T)settings[key];
            }

            return value;
        }

        public void UpdateValue(string key, Object value)
        {
            if (settings.Contains(key))
            {
                settings[key] = value;
            }
            else
            {
                settings.Add(key, value);
            }
            settings.Save();
        }

        public GPSMode GPSModeSetting
        {
            get
            {
                return GetValueOrDefault<GPSMode>(GPSModeKeyName, GPSModeDefault);
            }
            set
            {
                UpdateValue(GPSModeKeyName, value);
            }
        }

        public double GPSLatitudeSetting
        {
            get
            {
                return GetValueOrDefault<double>(GPSLatitudeKeyName, GPSLatitudeDefault);
            }
            set
            {
                UpdateValue(GPSLatitudeKeyName, value);
            }
        }

        public double GPSLongitudeSetting
        {
            get
            {
                return GetValueOrDefault<double>(GPSLongitudeKeyName, GPSLongitudeDefault);
            }
            set
            {
                UpdateValue(GPSLongitudeKeyName, value);
            }
        }

        public TemperatureUnit TemperatureUnitSetting
        {
            get
            {
                return GetValueOrDefault<TemperatureUnit>(TempratureUnitKeyName, TempratureUnitDefault);
            }
            set
            {
                UpdateValue(TempratureUnitKeyName, value);
            }
        }

        public int AppStartCounterSetting
        {
            get
            {
                return GetValueOrDefault<int>(AppStartCounterKeyName, AppStartCounterDefault);
            }
            set
            {
                UpdateValue(AppStartCounterKeyName, value);
            }
        }

        public bool SettingsModified
        {
            get
            {
                return GetValueOrDefault<bool>(SettingsModifiedKeyName, SettingsModifiedDefault);
            }
            set
            {
                UpdateValue(SettingsModifiedKeyName, value);
            }
        }

    }
}
