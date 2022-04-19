using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SugrBalance_App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SugrBalance_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {
        public RegPage()
        {
            InitializeComponent();
        }
        public async void goToLogin(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LogInPage());
        }
    }
}