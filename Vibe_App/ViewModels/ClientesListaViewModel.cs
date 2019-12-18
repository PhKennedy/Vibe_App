using Android.Widget;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Vibe_App.Models;
using Vibe_App.Services;
using Xamarin.Forms;

namespace Vibe_App.ViewModels
{
    public class ClientesListaViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private Cliente _cliente;

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
        public Cliente Cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                if (SetProperty(ref _cliente, value))               
                    GetDetalhesCliente();
            }
        }
       
        public ObservableCollection<Cliente> Clientes{ get; }
        public DataService DataService { get; } 
        public Command Refresh { get; }       

        public ClientesListaViewModel()
        {
            DataService = new DataService();
            Clientes = new ObservableCollection<Cliente>();
            Refresh = new Command(RefreshExecute);
            CarregarClientes();
        }

        private async void GetDetalhesCliente()
        {
            try
            {
                ComplementoCliente complemento = await DataService.GetDetalhesCliente(Cliente.Id);
                Cliente cliente = this.Cliente;
                cliente.Complemento = complemento;
                var json = JsonConvert.SerializeObject(cliente);
                CrossSecureStorage.Current.SetValue("CurrentCliente", json);
                await NavigationService.NavigateToDetalhesClientePage();
            }
            catch
            {
                MessageService.ShortAlert("Erro ao buscar os dados do cliente, tente novamente mais tarde.");
            }
        }
        private void RefreshExecute(object obj)
        {
            IsRefreshing = true;
            CarregarClientes();
            MessageService.LongAlert("Atualizando lista...");
            IsRefreshing = false;
        }
        public async void CarregarClientes()
        {
            List<Cliente> clientes;
            try
            {
                clientes = await DataService.GetClientes();
                if (clientes.Count != 0)
                {
                    Clientes.Clear();
                    foreach (var item in clientes)
                    {
                            Clientes.Add(item);
                    }
                    var json = JsonConvert.SerializeObject(clientes);
                    CrossSecureStorage.Current.SetValue("ListaClientes", json);
                }               
            }
            catch
            {
                MessageService.LongAlert("Erro ao carregar a lista de clientes, mostrando os últimos dados armazenados...");
                if (CrossSecureStorage.Current.HasKey("ListaClientes"))
                {
                    clientes = JsonConvert.DeserializeObject<List<Cliente>>(CrossSecureStorage.Current.GetValue("ListaClientes"));
                    foreach (var item in clientes)
                    {
                        if (!Clientes.Contains(item))
                            Clientes.Add(item);
                    }
                }
            }           
        }            
    }
}
