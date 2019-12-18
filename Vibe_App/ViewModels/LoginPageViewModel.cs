using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vibe_App.Services;
using Vibe_App.Views;
using Xamarin.Forms;

namespace Vibe_App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _cpf;
        private string _senha;
        private bool _isLogging;

        public string Cpf
        {
            get
            {
                return _cpf;
            }
            set
            {
                if(SetProperty(ref _cpf, value))
                    Login.ChangeCanExecute();
            }
        }
        public string Senha
        {
            get
            {
                return _senha;
            }
            set
            {
                if(SetProperty(ref _senha, value))
                   Login.ChangeCanExecute();
            }
        }        
        public bool IsLogging
        {
            get
            {
                return _isLogging;
            }
            set
            {
                if (SetProperty(ref _isLogging, value))
                    Login.ChangeCanExecute();
            }
        }

        public Command Login { get; }
        public Command CriarUser { get; }
        public DataService DataService { get; }

        public LoginViewModel()
        {
            Login = new Command(LoginExecute,CanLoginExecute);
            CriarUser = new Command(CriarUserExecute);
            DataService = new DataService();

            InicializarCampos();
        }

        private bool CanLoginExecute(object arg)
        {
            if (IsLogging == true)
            {
                return false;
            }
            if(string.IsNullOrWhiteSpace(Cpf) || string.IsNullOrWhiteSpace(Senha))
            {
                return false;
            }
            return true;
        }
        private async void CriarUserExecute(object obj)
        {
            await NavigationService.NavigateToRegistroPage();
        }
        private async void LoginExecute(object obj)
        {
            IsLogging = true;
            if (Cpf == CrossSecureStorage.Current.GetValue("CpfUsuario") && Senha == CrossSecureStorage.Current.GetValue("SenhaUsuario"))
            {
                NavigationService.SetMainPage();
                return;
            }
            try 
            {
                var resposta = await DataService.Autenticar(Cpf, Senha);
                if (resposta == "Usuário ou senha inválidos")
                {
                    MessageService.ShortAlert(resposta);
                    return;
                }
                NavigationService.SetMainPage();
            }
            catch(Exception e)
            {
                MessageService.ShortAlert(e.Message);
            }
            finally
            {
                IsLogging = false;
            }
    }
        private void InicializarCampos()
        {
            Cpf = "";
            Senha = "";
        }
    }
}
