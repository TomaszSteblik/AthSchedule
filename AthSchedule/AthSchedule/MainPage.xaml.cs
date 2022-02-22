using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AthSchedule
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (EntryId.Text is null)
            {
                await DisplayAlert("Błąd", "Puste pole ID", "OK");
            }
            else
            {
                if (EntryId.Text.Length == 0)
                {
                    await DisplayAlert("Błąd", "Puste pole ID", "OK");
                }
                else
                {
                    Application.Current.Properties["groupId"] = EntryId.Text;
                    SchedulePage schedulePage = new SchedulePage();
                    await schedulePage._schedule.DownloadWeekSchedule();
                    Application.Current.MainPage = schedulePage;
                }
                
            }
        }
    }
}