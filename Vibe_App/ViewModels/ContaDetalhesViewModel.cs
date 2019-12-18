using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using Vibe_App.Models;
using Vibe_App.Services;
using Xamarin.Forms;

namespace Vibe_App.ViewModels
{
    public class ContaDetalhesViewModel: BaseViewModel
    {
        private string _cpf;
        private string _nome;
        private DateTime _nascimento;

        public string Cpf {
            get
            {
                return _cpf;
            }
            set
            {
                SetProperty(ref _cpf, value);
            }
        }
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                SetProperty(ref _nome, value);
            }
        }
        public DateTime Nascimento
        {
            get
            {
                return _nascimento;
            }
            set
            {
                SetProperty(ref _nascimento, value);
            }
        }

        public DataService DataService { get; }
        public Command Sair { get; }

        public ContaDetalhesViewModel()
        {
            DataService = new DataService();
            GetDadosConta();
            Sair = new Command(SairExecute);
        }

        private void SairExecute(object obj)
        {
            CrossSecureStorage.Current.DeleteKey("CurrentUser");
            CrossSecureStorage.Current.DeleteKey("ListaClientes");
            NavigationService.SetLoginAsMainPage();
        }
        public async void GetDadosConta()
        {
            try
            {
                if (!CrossSecureStorage.Current.HasKey("CurrentUser"))
                {
                    CrossSecureStorage.Current.SetValue("CurrentUser", JsonConvert.SerializeObject(await DataService.GetUser()));
                }
                var currentUser = JsonConvert.DeserializeObject<User>(CrossSecureStorage.Current.GetValue("CurrentUser"));
                Cpf = currentUser.Cpf;
                Nome = currentUser.Nome;
                Nascimento = currentUser.Nascimento;
                
            }
            catch(Exception e)
            {
                MessageService.LongAlert(e.Message);
            }
        }

    }
}
