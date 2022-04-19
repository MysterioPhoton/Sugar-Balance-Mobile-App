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

namespace SugrBalance_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BloodPress : ContentPage
    {
        public BloodPress()
        {
            InitializeComponent();
        }
        internal static string UnixToDate(int Timestamp, string ConvertFormat)
        {
            DateTime ConvertedUnixTime = DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime;
            return ConvertedUnixTime.ToString(ConvertFormat);
        }
        private string pres = "sys";
        private async void RenderChart()
        {
            var userBPList = await FirebaseDataH.GetUserBPList();

            List<ChartEntry> entries = new List<ChartEntry>();

            foreach (BPData x in userBPList)
            {
                if (pres == "sys")
                {
                    entries.Add(new ChartEntry((float)x.Sys)
                    {
                        Label = UnixToDate(x.time, "dd/MM"),
                        ValueLabel = x.Sys.ToString(),
                        Color = SKColor.Parse("#FF0000")
                    });
                }
                else
                {
                    entries.Add(new ChartEntry((float)x.Dia)
                    {
                        Label = UnixToDate(x.time, "dd/MM"),
                        ValueLabel = x.Dia.ToString(),
                        Color = SKColor.Parse("#FF0000")
                    });
                }
            }

            var chart = new LineChart
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18,
                LabelTextSize = 30
            };
            chartView.Chart = chart;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RenderChart();
        }

        private void sysButton_Clicked(object sender, EventArgs e)
        {
            pres = "sys";
            RenderChart();
        }

        private void diaButton_Clicked(object sender, EventArgs e)
        {
            pres = "dia";
            RenderChart();
        }
    }
}