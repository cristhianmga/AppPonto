using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPonto.Controles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressBarPage : ContentPage
    {
        public ProgressBarPage()
        {
            InitializeComponent();
        }

        private void Modificar(object sender, EventArgs args)
        {
            bar1.ProgressTo(1,100000,Easing.Linear);
            bar1.ProgressColor = Color.Green;
            bar2.ProgressTo(0.7, 100000, Easing.Linear); ;
            bar2.ProgressColor = Color.Red;
            bar3.ProgressTo(0.3, 100000, Easing.SpringIn);
            bar3.ProgressColor = Color.Blue;
        }

        private void OnDateSelect(object sender, DateChangedEventArgs args)
        {
            labelDate.Text = args.NewDate.ToShortDateString();
        }
    }
}