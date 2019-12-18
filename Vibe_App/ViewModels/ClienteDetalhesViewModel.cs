using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using Vibe_App.Models;
using Xamarin.Forms;

namespace Vibe_App.ViewModels
{
    public class ClienteDetalhesViewModel : BaseViewModel
    {
        private Cliente _cliente;
        private string _imageSource;
        private bool _especial;
        private string _cpf;
        private string _id;
        private string _nomeCliente;
        private string _endereco;
        private string _numero;
        private string _complemento;
        private string _cidade;
        private string _empresa;

        public Cliente Cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                SetProperty(ref _cliente, value);
            }
        }
        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                SetProperty(ref _imageSource, value);
            }
        }
        public string Cpf
        {
            get
            {
                return _cpf;
            }
            set
            {
                SetProperty(ref _cpf, value);
            }
        }
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }
        public bool Especial
        {
            get
            {
                return _especial;
            }
            set
            {
                SetProperty(ref _especial, value);
            }
        }
        public string NomeCliente
        {
            get
            {
                return _nomeCliente;
            }
            set
            {
                SetProperty(ref _nomeCliente, value);
            }
        }
        public string Cidade
        {
            get
            {
                return _cidade;
            }
            set
            {
                SetProperty(ref _cidade, value);
            }
        }
        public string Endereco
        {
            get
            {
                return _endereco;
            }
            set
            {
                SetProperty(ref _endereco, value);
            }
        }
        public string Numero
        {
            get
            {
                return _numero;
            }
            set
            {
                SetProperty(ref _numero, value);
            }
        }
        public string Complemento
        {
            get
            {
                return _complemento;
            }
            set
            {
                SetProperty(ref _complemento, value);
            }
        }
        public string Empresa
        {
            get
            {
                return _empresa;
            }
            set
            {
                SetProperty(ref _empresa, value);
            }
        }

        public ClienteDetalhesViewModel()
        {
            InicializarCampos();
        }
        public void InicializarCampos()
        {
            Cliente = JsonConvert.DeserializeObject<Cliente>(CrossSecureStorage.Current.GetValue("CurrentCliente"));
            ImageSource = Cliente.Complemento.UrlImagem;
            NomeCliente = Cliente.Nome;
            Cpf = Cliente.Cpf;
            Id = Cliente.Id;
            Especial = Cliente.Especial;
            Cidade = Cliente.Complemento.Endereco.Cidade;
            Endereco = GetEnderecoFormatado(Cliente.Complemento.Endereco.endereco);
            Numero = Cliente.Complemento.Endereco.Numero;
            Complemento = Cliente.Complemento.Endereco.Complemento;
            Empresa = Cliente.Complemento.Empresa;
        }
        public string GetEnderecoFormatado(string endereco)
        {
            string primeiraParte = endereco.Substring(endereco.IndexOf(' '));
            string segundaParte = endereco.Substring(0, endereco.IndexOf(' '));
            string final = $"{primeiraParte} {segundaParte}";
            return final;
        }
    }
}
