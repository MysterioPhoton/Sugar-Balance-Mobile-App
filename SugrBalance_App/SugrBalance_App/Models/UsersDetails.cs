using System;
using System.Collections.Generic;
using System.Text;

namespace SugrBalance_App.Models
{
    public class UsersDetails
    {
        public string Email { get; set; }
        public string FirstName { get; set;}
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Sex { get; set; }
        public int Age { get; set; }
        public string EmergencyContact {get; set;}
    }
}
