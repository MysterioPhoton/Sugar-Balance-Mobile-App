using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using SugrBalance_App.Services;

namespace SugrBalance_App.ViewModels
{
    class BloodPressViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string bp;
        public string BP
        {
            get { return bp; }
            set
            {
                bp = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BP"));
            }
        }
        public Command AddNewBP
        {
            get
            {
                return new Command(() => { addBP(); });
            }
        }

        private async void addBP()
        {
            await FirebaseDataH.AddBPToFirebase(bp);
        }
    }
}
