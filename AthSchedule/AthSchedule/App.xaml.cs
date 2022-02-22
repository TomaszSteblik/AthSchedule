using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace AthSchedule
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            if (Application.Current.Properties.ContainsKey("groupId")) {
                SchedulePage schedulePage = new SchedulePage();
                schedulePage._schedule.DownloadWeekScheduleSync();
                Application.Current.MainPage = schedulePage;
            }else {
                MainPage = new MainPage();
            }
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}