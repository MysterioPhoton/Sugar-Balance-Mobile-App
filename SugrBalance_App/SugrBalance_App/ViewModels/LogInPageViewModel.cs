using SugrBalance_App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SugrBalance_App.Services;
using static SugrBalance_App.App;

namespace SugrBalance_App.ViewModels
{
    public class LogInPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
        public Command LoginCommand
        {
            get
            {
                return new Command(() => { Login(); });
            }
        }

        private async void Login()
        {

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await Application.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {

                var user = await FirebaseDataH.GetUser(Email);

                if (user != null)
                {
                    if (Email == user.Email && Password == user.Password)
                    {
                        await Application.Current.MainPage.DisplayAlert("Login Success", "", "Ok");

                        string modEmail = Email.Replace(".", "%");

                        GlobalVars.userEmail = modEmail;

                        await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Login Fail", "User does not exist", "Ok");
            }
        }
    }
}