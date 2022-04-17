using System;
using System.Collections.Generic;
using System.Text;

namespace SugrBalance_App
{
    public class RegPageModel
    {
        public string FirstName { get; set;}
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Sex { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string BloodType { get; set; }
        public int DateofBirth { get; set; }
        public string Email { get; set; }
        public int EmergencyContact {get; set;}
        public string Password { get; set; }
        public RegPageModel(string Email, string Password)
        {
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.Sex = Sex;
            this.Height = Height;
            this.Weight = Weight;
            this.BloodType = BloodType;
            this.DateofBirth = DateofBirth;
            this.Email = Email;
            this.EmergencyContact = EmergencyContact;
            this.Password = Password;
        }
    }
}
