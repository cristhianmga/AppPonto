using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        private static readonly HttpClient client = new HttpClient();
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login(object sender,EventArgs args)
        {
            try
            {
                var values = new Dictionary<string, string>
                {
                    { "cpf", "05203487103" },
                    { "senha", "user" }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://localhost:5001/api/Autenticacao/Login", content);

                var responseString = await response.Content.ReadAsStringAsync();

                var obj = JsonConvert.DeserializeObject<object>(responseString);

                await SecureStorage.SetAsync("oauth_token_app_ponto", "está logado");
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }
    }
}