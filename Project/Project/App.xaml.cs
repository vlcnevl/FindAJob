﻿using Project.Model;
using Project.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

          MainPage = new NavigationPage(new Signin());
         //   MainPage = new NavigationPage(new Tabbed());
        }
            
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static String UserID { get; set; } 

        public static Work getWork { get; set; }
    }
}
