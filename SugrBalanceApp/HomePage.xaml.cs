using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
      
    }
}
