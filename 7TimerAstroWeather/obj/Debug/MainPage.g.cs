﻿#pragma checksum "C:\Users\Mats\Development\7Timer-Astro-Weather\AstroPanel\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FC4DAA5906E949126B0B0FBF98EDE9CE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SevenTimerAstroWeather {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.ListBox SkyListBox;
        
        internal System.Windows.Controls.ProgressBar ProgressBar;
        
        internal System.Windows.Controls.TextBlock LoadingText;
        
        internal System.Windows.Controls.TextBlock LatitudeCoordinate;
        
        internal System.Windows.Controls.TextBlock LongitudeCoordinate;
        
        internal System.Windows.Controls.TextBlock LatitudeHemisphere;
        
        internal System.Windows.Controls.TextBlock LongitudeHemisphere;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/SevenTimerAstroWeather;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.SkyListBox = ((System.Windows.Controls.ListBox)(this.FindName("SkyListBox")));
            this.ProgressBar = ((System.Windows.Controls.ProgressBar)(this.FindName("ProgressBar")));
            this.LoadingText = ((System.Windows.Controls.TextBlock)(this.FindName("LoadingText")));
            this.LatitudeCoordinate = ((System.Windows.Controls.TextBlock)(this.FindName("LatitudeCoordinate")));
            this.LongitudeCoordinate = ((System.Windows.Controls.TextBlock)(this.FindName("LongitudeCoordinate")));
            this.LatitudeHemisphere = ((System.Windows.Controls.TextBlock)(this.FindName("LatitudeHemisphere")));
            this.LongitudeHemisphere = ((System.Windows.Controls.TextBlock)(this.FindName("LongitudeHemisphere")));
        }
    }
}
