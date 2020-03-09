using AppPonto.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPonto.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly HttpClient client;
        public LoginPage()
        {
            InitializeComponent();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            client = new HttpClient(handler);
        }

        private async void Login(object sender,EventArgs args)
        {
            try
            {
                var user = new
                {
                    cpf = CPF.Text,
                    senha = sha256_hash(Senha.Text)
                };

                var content = JsonConvert.SerializeObject(user);
                var response = await client.PostAsync("https://10.0.2.2:5001/api/Autenticacao", new StringContent(content,Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var obj = JsonConvert.DeserializeObject<ResponseAutenticacao>(responseString);

                    if (obj.Authenticated)
                    {
                        await SecureStorage.SetAsync("oauth_token_app_ponto", obj.AccessToken);
                        App.Current.MainPage = new Menu.Master();
                        
                    }
                    else
                    {
                        DisplayAlert("Erro", "Usuário ou senha inválidos", "Ok");
                    }

                }
                else
                {
                    DisplayAlert("Alerta", "Erro ao realizar login.", "Ok");
                }
                
                
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }

        private String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}