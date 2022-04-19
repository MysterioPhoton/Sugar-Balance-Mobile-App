using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SugrBalance_App.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SugrBalance_App.Services;
using System.Diagnostics;

namespace SugrBalance_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public async void goToBloodpress(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BloodPress());
        }
        public async void goToBloodsugar(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BloodSug());
        }
        public async void goToMedpage(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new MedPage());
        }

        private void nhsButton_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync("tel:4857773456");
        }

        private async void emergencyButton_Clicked(object sender, EventArgs e)
        {
            string contact = "tel:" + await FirebaseDataH.GetUserEmergencyContact();
            await Launcher.OpenAsync(contact);
        }
    }
}