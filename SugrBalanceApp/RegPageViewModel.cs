using SugrBalance_App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SugrBalance_App
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
        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {
                confirmpassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }
        public Command SignUpCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (Password == ConfirmPassword)
                        SignUp();
                    else
                        Application.Current.MainPage.DisplayAlert("", "Password must be same as above!", "OK");
                });
            }
        }
        private async void SignUp()
        {


            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await Application.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {

                var user = await FirebaseDataH.AddUser(Email, Password);

                if (user)
                {
                    await Application.Current.MainPage.DisplayAlert("SignUp Success", "", "Ok");


                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");

            }
        }
    }
}
