using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Vibe_App.Models;
using Vibe_App.Services;
using Xamarin.Forms;

namespace Vibe_App.ViewModels
{
    public class RegistroViewModel : BaseViewModel
    {
        private string _cpf;
        private string _nome;
        private string _nascimento;
        private string _senha;
        private string _confirmacaoSenha;
        private bool _isSignin;

        public string Cpf
        {
            get
            {
                return _cpf;
            }
            set
            {
                if (SetProperty(ref _cpf, value))
                    Registrar.ChangeCanExecute();                               
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
                Registrar.ChangeCanExecute();
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
                if(SetProperty(ref _nome, value))
                Registrar.ChangeCanExecute();
            }
        }
        public string Nascimento
        {
            get
            {
                return _nascimento;
            }
            set
            {
                if(SetProperty(ref _nascimento, value))
                Registrar.ChangeCanExecute();
            }
        }
        public string ConfirmacaoSenha
        {
            get
            {
                return _confirmacaoSenha;
            }
            set
            {
               if(SetProperty(ref _confirmacaoSenha, value))
                Registrar.ChangeCanExecute();
            }
        }
        public bool IsSignin
        {
            get
            {
                return _isSignin;
            }
            set
            {
                if(SetProperty(ref _isSignin, value))
                    Registrar.ChangeCanExecute();
            }
        }

        public Command Registrar { get; }
        public DataService DataService { get; }

        public RegistroViewModel()
        {           
            Registrar = new Command(RegistrarExecute, CanRegistrarExecute); 
            DataService = new DataService();
            InicializarCampos();
        }
        private void InicializarCampos()
        {
            Cpf = "";
            Senha = "";
            Nascimento = "";
            ConfirmacaoSenha = "";

        }
        private bool CanRegistrarExecute(object arg)
        {
            if (string.IsNullOrWhiteSpace(Cpf) || string.IsNullOrWhiteSpace(Senha) || string.IsNullOrWhiteSpace(Nascimento) || string.IsNullOrWhiteSpace(ConfirmacaoSenha)|| IsSignin == true)
            {
                return false;
            } 
            return true;
        }
        private async void RegistrarExecute(object obj)
        {
            IsSignin = true;
            try
            {
                if (Senha != ConfirmacaoSenha)
                {
                    MessageService.ShortAlert("As senhas são diferentes!");
                    return;
                }
                if (DateTime.TryParse(Nascimento, out _) == false || DateTime.Parse(Nascimento).Year > DateTime.Now.Year)
                {
                    MessageService.ShortAlert("A data é inválida!");
                    return;
                }

                User user = new User()
                {
                    Cpf = this.Cpf,
                    Nome = this.Nome,
                    Nascimento = DateTime.Parse(this.Nascimento),
                    Senha = this.Senha
                };
                MessageService.ShortAlert(await DataService.CriarUsuario(user));
                await NavigationService.PopToRootAsync();
            }
            catch(Exception e)
            {
                MessageService.ShortAlert(e.Message);
            }
            finally
            {
                IsSignin = false;
            }
        }
    }
}
