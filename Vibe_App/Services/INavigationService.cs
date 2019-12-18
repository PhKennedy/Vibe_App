using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vibe_App.Services
{
    public interface INavigationService
    {
        Task NavigateToRegistroPage();
        Task NavigateToLoginPage();
        Task NavigateToMainPage();
        Task NavigateToDetalhesContaPage();
        Task NavigateToDetalhesClientePage();
        Task PopToRootAsync();
        void SetMainPage();
        void SetLoginAsMainPage();
    }
}
