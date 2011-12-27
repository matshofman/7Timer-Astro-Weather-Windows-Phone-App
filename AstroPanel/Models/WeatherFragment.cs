using Newtonsoft.Json;
using System;
using System.Windows;
namespace SevenTimerAstroWeather.Models
{
    public class WeatherFragment
    {
        [JsonProperty(PropertyName = "timepoint")]
        public int TimePointBase { get; set; }

        [JsonIgnore]
        public DateTime DateTime { get; set; }

        [JsonProperty(PropertyName = "cloudcover")]
        public int CloudCover { get; set; }

        [JsonProperty(PropertyName = "seeing")]
        public int Seeing { get; set; }

        [JsonProperty(PropertyName = "transparency")]
        public int Transparency { get; set; }

        [JsonProperty(PropertyName = "lifted_index")]
        public int LiftedIndex { get; set; }

        [JsonProperty(PropertyName = "rh2m")]
        public int Humidity { get; set; }

        [JsonProperty(PropertyName = "temp2m")]
        public int Temperature { get; set; }

        [JsonProperty(PropertyName = "prec_type")]
        public Precipitation Precipitation { get; set; }

        [JsonProperty(PropertyName = "wind10m")]
        public WindData Wind { get; set; }

    #region DisplayProperties

        public int CloudCoverDegrees 
        { 
            get 
            {
                return (CloudCover - 1) * 45; 
            }
        }

        public int SeeingCircleSize 
        { 
            get 
            { 
                return 14 + (Seeing * 4); 
            } 
        }

        public string SeeingColor 
        { 
            get 
            {
                string[] colors = { "#FF003F7F", "#FF185898", "#FF3171B1", "#FF4A8ACA", "#FF63A3E3", "#FFCCCCCC", "#FFE5E5E5", "#FFF9F9F9" };
                if (Seeing <= colors.Length && Seeing >= 0)
                {
                    return colors[Seeing - 1];
                }
                else
                {
                    return "#00FFFFFF";
                }
            }
        }

        public string TransparencyColor
        {
            get
            {
                string[] colors = { "#FF003F7F", "#FF185898", "#FF3171B1", "#FF4A8ACA", "#FF63A3E3", "#FFCCCCCC", "#FFE5E5E5", "#FFF9F9F9" };
                if (Transparency <= colors.Length && Transparency >= 0)
                {
                    return colors[Transparency - 1];
                }
                else
                {
                    return "#00FFFFFF";
                }
            }
        }

        public Visibility TransparencyBar1
        {
            get
            {
                if (Transparency >= 1)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility TransparencyBar2
        {
            get
            {
                if (Transparency >= 2)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility TransparencyBar3
        {
            get
            {
                if (Transparency >= 3)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility TransparencyBar4
        {
            get
            {
                if (Transparency >= 4)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility TransparencyBar5
        {
            get
            {
                if (Transparency >= 5)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility TransparencyBar6
        {
            get
            {
                if (Transparency >= 6)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility TransparencyBar7
        {
            get
            {
                if (Transparency >= 7)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility TransparencyBar8
        {
            get
            {
                if (Transparency >= 8)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public string TemperatureString
        {
            get
            {
                AppSettings settings = new AppSettings();
                return Temperature + "°" + settings.TemperatureUnitSetting.ToString();
            }
        }

        public string HumidityPercentage
        {
            get
            {
                return (Humidity + 4) * 5 + "%";
            }
        }

        public string PrecipitationImageUrl
        {
            get
            {
                return "Images/" + Precipitation + ".png";
            }
        }

        public int TimePoint
        {
            get
            {
                return DateTime.Hour;
            }
        }

        public string DateString
        {
            get
            {
                return DateTime.ToLongDateString();
            }
        }

        public Visibility DateVisible
        {
            get
            {
                if (DateTime.Hour < 3 || TimePointBase == 0)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

    #endregion

    }
}
