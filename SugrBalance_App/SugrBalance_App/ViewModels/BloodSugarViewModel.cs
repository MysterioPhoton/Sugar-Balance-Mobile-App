using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using SugrBalance_App.Services;

namespace SugrBalance_App.ViewModels
{
    class BloodSugarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string bs;
        public string BS
        {
            get { return bs; }
            set
            {
                bs = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BS"));
            }
        }
        public Command AddNewBS
        {
            get
            {
                return new Command(() => {addBS(); });
            }
        }
        private async void addBS()
        {
            await FirebaseDataH.AddBSToFirebase(Double.Parse(bs));
        }
    }
}
