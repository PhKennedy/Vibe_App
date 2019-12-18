using Plugin.SecureStorage;
using System;
using Vibe_App.Services;
using Vibe_App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vibe_App
{
    public partial class App : Application
    {

        public App()
        {
            DependencyService.Register<INavigationService, NavigationService>();
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
