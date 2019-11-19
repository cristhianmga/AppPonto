using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        private void GoToActivityIndicator(object sender, EventArgs args)
        {
            Detail = new Controles.ActivityIndicatorPage();
        }

        private void GoToProgressBar(object sender, EventArgs args)
        {
            Detail = new Controles.ProgressBarPage();
        }
    }
}