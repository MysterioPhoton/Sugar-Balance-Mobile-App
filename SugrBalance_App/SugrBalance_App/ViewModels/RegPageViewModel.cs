using SugrBalance_App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using SugrBalance_App.Models;
using Xamarin.Forms;
using SugrBalance_App.Services;
using static SugrBalance_App.App;

namespace SugrBalance_App.ViewModels
{
    public class RegPageViewModel : INotifyPropertyChanged
    {
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }


        //public event PropertyChangedEventHandler PropertyChanged;

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        private string middleName;
        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MiddleName"));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName= value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        private string sex;
        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Sex"));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Age"));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        private string emergencyContact;
        public string EmergencyContact
        {
            get { return emergencyContact; }
            set
            {
                emergencyContact = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EmergencyContact"));
            }
        }

        public Command SignUpCommand
        {
            get
            {
                return new Command(() =>
                {
                    SignUp();
                });
            }
        }
        private async void SignUp()
        {


            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await Application.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {

                var user = await FirebaseDataH.AddUser(Email, Password, FirstName, MiddleName, LastName, Sex, Age, EmergencyContact);

                if (user)
                {
                    await Application.Current.MainPage.DisplayAlert("SignUp Success", "", "Ok");

                    string modEmail = Email.Replace(".", "%");

                    GlobalVars.userEmail = modEmail;


                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");

            }
        }
    }
}
