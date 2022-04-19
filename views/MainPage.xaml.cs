using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SugrBalance_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void goToLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LogInPage());
        }
        public async void goToRegPage(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new RegPage());
        }
    }
}
