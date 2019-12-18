using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vibe_App.Views;
using Xamarin.Forms;

namespace Vibe_App.Services
{
    public class NavigationService : INavigationService
    {
       
        public async Task NavigateToDetalhesClientePage()
        {
            if (App.Current.MainPage.Navigation.NavigationStack.Count == 0 ||
                App.Current.MainPage.Navigation.NavigationStack.Last().GetType() != typeof(ClienteDetalhesPage))
            {
                await App.Current.MainPage.Navigation.PushAsync(new ClienteDetalhesPage());
            }
            
        }
        public async Task NavigateToDetalhesContaPage()
        {
            if (App.Current.MainPage.Navigation.NavigationStack.Count == 0 ||
                   App.Current.MainPage.Navigation.NavigationStack.Last().GetType() != typeof(ContaDetalhesPage))
            {
                await App.Current.MainPage.Navigation.PushAsync(new ContaDetalhesPage());
            }
        }
        public async Task NavigateToLoginPage()
        {
            if (App.Current.MainPage.Navigation.NavigationStack.Count == 0 ||
                   App.Current.MainPage.Navigation.NavigationStack.Last().GetType() != typeof(LoginPage))
            {
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
        }
        public async Task NavigateToMainPage()
        {
            if (App.Current.MainPage.Navigation.NavigationStack.Count == 0 ||
                   App.Current.MainPage.Navigation.NavigationStack.Last().GetType() != typeof(MainPage))
            {
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
        }
        public async Task NavigateToRegistroPage()
        {
            if (App.Current.MainPage.Navigation.NavigationStack.Count == 0 ||
                   App.Current.MainPage.Navigation.NavigationStack.Last().GetType() != typeof(RegistroPage))
            {
                await App.Current.MainPage.Navigation.PushAsync(new RegistroPage());
            }           
        }
        public async Task PopToRootAsync()
        {
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }
        public void SetLoginAsMainPage()
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
        public void SetMainPage()
        {
             App.Current.MainPage =new NavigationPage(new MainPage());            
        }
    }
}
