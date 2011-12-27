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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text;
using System.Collections.Generic;

namespace SevenTimerAstroWeather.Models
{
    public class WeatherData
    {
        #region Private Properties
        private string initializeTime;
        private DateTime initializeDateTime;
        private List<WeatherFragment> weatherFragments;
        #endregion

        [JsonProperty(PropertyName = "product")]
        public string Product { get; set; }

        [JsonProperty(PropertyName = "init")]
        public string InitializeTime 
        { 
            get 
            {
                return initializeTime; 
            } 
            set 
            {
                initializeTime = value;
                InitializeDateTime = DateTime.ParseExact(value, "yyyyMMddHH", null); 
            }
        }

        [JsonIgnore]
        public DateTime InitializeDateTime
        { 
            get 
            {
                return initializeDateTime; 
            } 
            set 
            {
                initializeDateTime = value; 
                FillWeatherFragmentTimePoints();
            }
        } 

        [JsonProperty(PropertyName = "dataseries")]
        public List<WeatherFragment> WeatherFragments
        { 
            get 
            {
                return weatherFragments; 
            } 
            set 
            {
                weatherFragments = value; 
                FillWeatherFragmentTimePoints();
            }
        }

    #region Methods

        private void FillWeatherFragmentTimePoints() 
        {
            if (InitializeDateTime != null && WeatherFragments != null)
            {
                foreach (WeatherFragment fragment in WeatherFragments)
                {
                    fragment.DateTime = InitializeDateTime.AddHours(fragment.TimePointBase).ToLocalTime();
                }
            }
        }

        public static Uri GetRequestUri(double latitude, double longitude, TemperatureUnit temperature)
        {
            StringBuilder url = new StringBuilder();

            url.Append("http://www.7timer.com/v4/bin/astro.php");
            url.Append("?lat=" + latitude.ToString("0.000").Replace(",", "."));
            url.Append("&lon=" + longitude.ToString("0.000").Replace(",", "."));
            if (temperature == TemperatureUnit.C)
                url.Append("&unit=metric");
            else if (temperature == TemperatureUnit.F)
                url.Append("&unit=british");
            url.Append("&output=json");

            return new Uri(url.ToString());
        }

    #endregion
    
    }
}
