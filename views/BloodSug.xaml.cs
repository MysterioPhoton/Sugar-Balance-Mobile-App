using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SugrBalance_App.Models;
using SugrBalance_App.Services;
using System.Diagnostics;

namespace SugrBalance_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BloodSug : ContentPage
    {
        public BloodSug()
        {
            InitializeComponent();
        }
        internal static string UnixToDate(int Timestamp, string ConvertFormat)
        {
            DateTime ConvertedUnixTime = DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime;
            return ConvertedUnixTime.ToString(ConvertFormat);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var userBSList = await FirebaseDataH.GetUserBSList();

            List<ChartEntry> entries = new List<ChartEntry>();

            foreach (BSData x in userBSList)
            {
                entries.Add(new ChartEntry((float)x.BS)
                {
                    Label = UnixToDate(x.time, "dd/MM"),
                    ValueLabel = x.BS.ToString(),
                    Color = SKColor.Parse("#FF0000")
                });
            }

            var chart = new LineChart { 
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18,
                LabelTextSize = 30
            };
            chartView.Chart = chart;
        }
    }
}
