using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Recommender.Views;

namespace Recommender
{
    public partial class App : Application
    {
        public static string Algorithm { get; set; }
        private Page mainTabbedPage;
        public App()
        {
            InitializeComponent();

            MainPage = new LandingPage();
            Algorithm = "sar";
        }

        protected override void OnStart()
        {
            mainTabbedPage = new MainPage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
