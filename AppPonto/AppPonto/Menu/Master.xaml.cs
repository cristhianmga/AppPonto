using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPonto.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public Master()
        {
            InitializeComponent();
            string oauthToken = string.Empty;
            try
            {
                oauthToken = SecureStorage.GetAsync("oauth_token_app_ponto").Result;
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
            if (string.IsNullOrEmpty(oauthToken))
            {
                Detail = new Pages.LoginPage();
            }
        }

        private void GoToPerfil(object sender, EventArgs args)
        {
            Detail = new MainPage();
        }

        private void Logout(object sender, EventArgs args)
        {
            try
            {
                var result = SecureStorage.Remove("oauth_token_app_ponto");

                if (result)
                {
                    Detail = new Pages.LoginPage();
                }
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }
    }
}