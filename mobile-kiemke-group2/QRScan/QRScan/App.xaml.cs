using QRScan.Models;
using QRScan.Services;
using QRScan.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRScan
{
    public partial class App : Application
    {
        public  App()
        {
            InitializeComponent();
            //FileService.CreateFileData();

            //MainPage = new NavigationPage( new StartUpView());
            MainPage = new NavigationPage(LoginView.GetInstance());
            //LocalData.KiemKes
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
    }
}
