using Android.App;
using Android.Widget;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vibe_App.Models;

namespace Vibe_App.Services
{
    public class DataService
    {
        private readonly string uri = "https://vibeselecao.azurewebsites.net/api";
        private static readonly HttpClient Client = new HttpClient();
        private readonly Criptografia Criptografia = new Criptografia();
        private readonly string AccessKey = CrossSecureStorage.Current.GetValue("Token");
        private readonly string CurrentCpfValue = CrossSecureStorage.Current.GetValue("CpfUsuario");
        private readonly string CurrentPassword = CrossSecureStorage.Current.GetValue("SenhaUsuario");
        private readonly string MensagemErro = "Erro de rede ou serviço não disponível";
        private ServerResult ServerResponse { get; set; }


        //Existe um laço nos métodos de requisição e dentro desse laço o método "Autenticar" é chamado para refazer a validação da token, caso o mesmo tenha expirado.
        public async Task<string> Autenticar(string cpf, string senha)
        {
            try
            {
                Client.CancelPendingRequests();
                var senhaEncriptada = Criptografia.CriptografarMD5(senha);

                var json = JsonConvert.SerializeObject(new { cpf, senha = senhaEncriptada });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var uri = $"{this.uri}/autenticacao";

                HttpResponseMessage result = await Client.PostAsync(uri, content);
                ServerResponse = JsonConvert.DeserializeObject<ServerResult>(result.Content.ReadAsStringAsync().Result);
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    CrossSecureStorage.Current.SetValue("SenhaUsuario", senha);
                    CrossSecureStorage.Current.SetValue("CpfUsuario", cpf);                     
                    CrossSecureStorage.Current.SetValue("Token", ServerResponse.Chave);

                    return ServerResponse.Chave;
                }
                return ServerResponse.Mensagem;
            }
            catch
            {
               throw new Exception(MensagemErro);
            }
        }
        public async Task<string> CriarUsuario(User user)
        {
            try
            {
                Client.CancelPendingRequests();
                user.Senha = Criptografia.CriptografarMD5(user.Senha);


                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var uri = $"{this.uri}/usuario";

                HttpResponseMessage result = await Client.PostAsync(uri, content);
                ServerResponse = JsonConvert.DeserializeObject<ServerResult>(result.Content.ReadAsStringAsync().Result);
                if (result.StatusCode == HttpStatusCode.OK)
                    return "Usuário criado com sucesso!";

                return ServerResponse.Mensagem;
            }
            catch
            {
                throw new Exception(MensagemErro);
            }
        }
        public async Task<List<Cliente>> GetClientes()
        {
            try
            {
                Client.CancelPendingRequests();
                List<Cliente> clientes = null;
                for (int i = 0; i < 2; i++)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AcessKey);
                    var result = await Client.GetAsync($"{uri}/cliente");
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        await Autenticar(CurrentCpfValue, CurrentPassword);
                        continue;
                    }
                    clientes = JsonConvert.DeserializeObject<List<Cliente>>(result.Content.ReadAsStringAsync().Result);
                    break;      
                }
                return clientes;
            }
            catch
            {
                throw new Exception(MensagemErro);
            }
        }
        public async Task<User> GetUser()
        {
            User user = null;
            try
            {
                Client.CancelPendingRequests();
                for (int i = 0; i < 2; i++)
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AcessKey);
                    var result = await Client.GetAsync($"{uri}/usuario/{CurrentCpfValue}");
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        await Autenticar(CurrentCpfValue, CurrentPassword);
                        continue;
                    }
                        user = JsonConvert.DeserializeObject<User>(result.Content.ReadAsStringAsync().Result);
                        break;
                }
                
                return user;
            }
            catch
            {
                throw new Exception(MensagemErro);
            }
        }
        public async Task<ComplementoCliente> GetDetalhesCliente(string id)
        {
            ComplementoCliente clienteData = null;
            try
            {
                for (int i = 0; i < 2; i++) {
                    Client.CancelPendingRequests();
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AcessKey);
                    var result = await Client.GetAsync($"{uri}/cliente/{id}");
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        await Autenticar(CurrentCpfValue, CurrentPassword);
                        continue;
                    }
                        clienteData = JsonConvert.DeserializeObject<ComplementoCliente>(result.Content.ReadAsStringAsync().Result);
                        break;      
                }
                return clienteData;
            }
            catch
            {
                throw new Exception(MensagemErro);
            }
        }
    }
}
