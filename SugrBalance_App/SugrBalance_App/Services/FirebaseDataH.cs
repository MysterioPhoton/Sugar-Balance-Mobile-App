using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SugrBalance_App.Models;
using static SugrBalance_App.App;
using Newtonsoft.Json;

namespace SugrBalance_App.Services
{
     public class FirebaseDataH
    {
        public static FirebaseClient firebase = new FirebaseClient("https://sugrbalanceapp-default-rtdb.firebaseio.com/:null");
        //connecting app with database
        public static async Task<List<Users>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    Email = item.Object.Email,
                    Password = item.Object.Password
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }

        }
        public static async Task<Users> GetUser(string email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("Users")
                .OnceAsync<Users>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<bool> AddUser(string email, string password, string FirstName, string MiddleName, string LastName, string Sex, int Age, string EmergencyContact)
        {
            try
            {
                await firebase
                .Child("Users")
                .PostAsync(new Users() { Email = email, Password = password });

                await firebase
                    .Child("Details")
                    .PostAsync(new UsersDetails() { Email = email, FirstName = FirstName, MiddleName = MiddleName, LastName = LastName, Sex = Sex, Age = Age, EmergencyContact = EmergencyContact });

                await AddBPToFirebaseBulk(new BPData[]
                {
                        new BPData() { Sys = 80, Dia = 50, time = 1650289857, email = email },
                        new BPData() { Sys = 150, Dia = 100, time = 1650203457, email = email },
                        new BPData() { Sys =170, Dia =90, time=1650289857, email = email  },
                        new BPData() { Sys= 120, Dia= 80 , time= 1650117057, email = email  },
                        new BPData() { Sys= 90, Dia= 60 , time= 1650030657, email = email },
                        new BPData() { Sys= 70, Dia= 80 , time= 1649944257, email = email  },
                        new BPData() { Sys= 70, Dia= 40 , time= 1649857857, email = email  },
                        new BPData() { Sys= 110, Dia= 80 , time= 1649771457, email = email  },
                        new BPData() { Sys= 110, Dia= 60 , time= 1649685057, email = email  },
                        new BPData() { Sys= 140, Dia= 80 , time= 1649598657, email = email  },
                        new BPData() { Sys= 120, Dia= 40 , time= 1649512257, email = email  },
                        new BPData() { Sys= 160, Dia= 90 , time= 1649425857, email = email  },
                        new BPData() { Sys= 120, Dia= 78 , time= 1649339457 , email = email },
                        new BPData() { Sys= 70, Dia= 60 , time= 1649253057 , email = email  },
                        new BPData() { Sys= 80, Dia= 55 , time= 1649166657, email = email   },
                        new BPData() { Sys= 105, Dia= 73 , time= 1649080257 , email = email },
                        new BPData() { Sys= 132, Dia= 83 , time= 1648993857 , email = email },
                        new BPData() { Sys= 115, Dia= 80 , time= 1648907457 , email = email },
                        new BPData() { Sys= 108, Dia= 70 , time= 1648821057, email = email  },
                    });

                await AddBSToFirebaseBulk(new BSData[]
                {
                    new BSData() { BS = 3.6, time= 1650289857, email = email },
                    new BSData() { BS = 3.9, time= 1650203457, email = email },
                    new BSData() { BS = 4.2, time= 1650289857, email = email },
                    new BSData() { BS = 5.2, time= 1650203457, email = email },
                    new BSData() { BS = 6.4, time= 1650117057, email = email },
                    new BSData() { BS = 9.3, time= 1650030657, email = email },
                    new BSData() { BS = 12.6,time= 1649944257, email = email },
                    new BSData() { BS = 8.7, time= 1649857857, email = email },
                    new BSData() { BS = 5.7, time= 1649771457, email = email },
                    new BSData() { BS = 7.8, time= 1649685057, email = email },
                    new BSData() { BS = 5.5, time= 1649598657, email = email },
                    new BSData() { BS = 4.3, time= 1649512257, email = email },
                    new BSData() { BS = 7.1, time= 1649425857, email = email },
                    new BSData() { BS = 10.5,time= 1649339457, email = email },
                    new BSData() { BS = 6.2, time= 1649253057, email = email },
                    new BSData() { BS = 5.3, time= 1649166657, email = email },
                    new BSData() { BS = 4.8, time= 1649080257, email = email },
                    new BSData() { BS = 7.6, time= 1648993857, email = email },
                });

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        internal static async Task<bool> AddBPToFirebaseBulk(BPData[] ListOfBP)
        {
            foreach (BPData bp in ListOfBP)
            {
                try
                {
                    await firebase
                    .Child("bpData")
                    .PostAsync(bp);
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error:{e}");
                    return false;
                }
            }
            return true;
        }

        public static async Task<bool> AddBPToFirebase(string bp)
        {
            int Timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            int sys = Int32.Parse(bp.Split('/')[0]);
            int dia = Int32.Parse(bp.Split('/')[1]);

            try
            {
                await firebase
                .Child("bpData")
                .PostAsync(new BPData() {
                    Sys = sys,
                    Dia = dia,
                    time = Timestamp,
                    email = GlobalVars.userEmail
                });

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<List<BPData>> GetUserBPList()
        {
            try
            {
                var allBSData = (await firebase
                    .Child("bpData")
                    .OnceAsync<BPData>()).Select(item =>
                new BPData
                {
                    Dia = item.Object.Dia,
                    Sys = item.Object.Sys,
                    time = item.Object.time,
                    email = item.Object.email
                }).ToList(); ;

                return allBSData.Where(a => a.email == GlobalVars.userEmail).ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }

        }
        internal static async Task<bool> AddBSToFirebaseBulk(BSData[] ListOfBS)
        {
            foreach (BSData bs in ListOfBS)
            {
                try
                {
                    await firebase
                    .Child("bsData")
                    .PostAsync(bs);
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error:{e}");
                    return false;
                }
            }
            return true;
        }
        public static async Task<bool> AddBSToFirebase(double bs)
        {
            int Timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            try
            {
                Debug.WriteLine(GlobalVars.userEmail);

                await firebase
                .Child("bsData")
                .PostAsync(new BSData() {
                    BS = bs,
                    time = Timestamp,
                    email = GlobalVars.userEmail
                });

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<List<BSData>> GetUserBSList()
        {
            try
            {
                var allBSData = (await firebase
                    .Child("bsData")
                    .OnceAsync<BSData>()).Select(item =>
                new BSData
                {
                    BS = item.Object.BS,
                    time = item.Object.time,
                    email = item.Object.email
                }).ToList(); ;

                return allBSData.Where(a => a.email == GlobalVars.userEmail).ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }

        }
        public static async Task<bool> AddMedToFirebase(string medName, string time, string dose)
        {
            int Timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            try
            {
                await firebase
                    .Child("meds")
                    .PostAsync(new Med() {
                        MedName = medName,
                        Time = time,
                        Dose = dose,
                        email = GlobalVars.userEmail
                    });

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<List<Med>> GetUserMedList()
        {
            try
            {
                var allMedData = (await firebase
                    .Child("meds")
                    .OnceAsync<Med>()).Select(item =>
                new Med
                {
                    Dose = item.Object.Dose,
                    MedName = item.Object.MedName,
                    Time = item.Object.Time,
                    email = item.Object.email
                }).ToList(); ;

                return allMedData.Where(a => a.email == GlobalVars.userEmail).ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<string> GetUserEmergencyContact()
        {
            try
            {
                var allDetails = (await firebase
                    .Child("Details")
                    .OnceAsync<UsersDetails>()).Select(item =>
                new UsersDetails
                {
                    Age = item.Object.Age,
                    Email = item.Object.Email,
                    EmergencyContact = item.Object.EmergencyContact,
                    FirstName = item.Object.FirstName,
                    LastName = item.Object.LastName,
                    MiddleName = item.Object.MiddleName,
                    Sex = item.Object.Sex
                }).ToList();

                return allDetails.Where(a => a.Email == GlobalVars.userEmail).FirstOrDefault().EmergencyContact;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
    }
}
