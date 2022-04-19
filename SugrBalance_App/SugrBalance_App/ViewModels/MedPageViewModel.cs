using SugrBalance_App.Models;
using SugrBalance_App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

using System.Threading.Tasks;

namespace SugrBalance_App.ViewModels
{
    class MedPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Med> UserMeds { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string medName;
        public string MedName
        {
            get { return medName; }
            set
            {
                medName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MedName"));
            }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Time"));
            }
        }

        private string dose;
        public string Dose
        {
            get { return dose; }
            set
            {
                dose = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Dose"));
            }
        }
        public Command AddNewMed
        {
            get
            {
                return new Command(() => { addMed(); });
            }
        }

        private async void addMed()
        {
            await FirebaseDataH.AddMedToFirebase(MedName, Time.ToString(), Dose);
        }
        public async Task LoadDataAsync()
        {
            var allMeds = await FirebaseDataH.GetUserMedList();
            foreach (Med med in allMeds)
            {
                UserMeds.Add(med);
            }
        }
        public MedPageViewModel()
        {
            UserMeds = new ObservableCollection<Med>();
            LoadDataAsync();
        }
    }
}
