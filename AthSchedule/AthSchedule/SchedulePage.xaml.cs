using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AthSchedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        public Schedule _schedule;
        public SchedulePage()
        {
            InitializeComponent();
            _schedule = new Schedule(Int32.Parse(Application.Current.Properties["groupId"].ToString()));
            _schedule.DownloadWeekScheduleSync();
            SetupListView();
        }


        private void Button_OnClicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new MainPage();
        }


        private void SetupListView()
        {
            List<ClassGroup> listClassGroup = new List<ClassGroup>();
            var cultureInfo = new CultureInfo("pl-PL");
            for (DayOfWeek i = DayOfWeek.Monday; i <= DayOfWeek.Saturday; i++)
            {
                
                var classGroup = new ClassGroup(cultureInfo.DateTimeFormat.GetDayName(i), cultureInfo.DateTimeFormat.GetDayName(i).Substring(0, 1));
                var classes = _schedule.Classes.FindAll(p => p.DayOfWeek == i);
                foreach (var cClass in classes)
                {
                    classGroup.Add(cClass);
                }
                listClassGroup.Add(classGroup);
            }
            
            
            
            List<Class> list1 = _schedule.Classes.Where(p => p.DayOfWeek == DayOfWeek.Sunday).ToList();
            var classGroup1 = new ClassGroup(cultureInfo.DateTimeFormat.GetDayName(DayOfWeek.Sunday),cultureInfo.DateTimeFormat.GetDayName(DayOfWeek.Sunday).Substring(0,1));
                
            foreach (var cClass in list1)
            {
                classGroup1.Add(cClass);
            }
            listClassGroup.Add(classGroup1);
            


            ListViewClasses.RowHeight = 80;
            ListViewClasses.ItemsSource = listClassGroup;
            ListViewClasses.IsGroupingEnabled = true;
            ListViewClasses.SeparatorVisibility = SeparatorVisibility.None;
            ListViewClasses.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty,"Name");
                nameLabel.MaxLines = 2;
                nameLabel.HorizontalTextAlignment = TextAlignment.Center;
                nameLabel.FontAttributes = FontAttributes.Bold;

                Label startLabel = new Label();
                startLabel.SetBinding(Label.TextProperty,"StartTime");
                
                Label endLabel = new Label();
                endLabel.SetBinding(Label.TextProperty,"EndTime");

                Label arrowLabel = new Label();
                arrowLabel.Text = "--->";

                return new ViewCell
                {
                    View = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                        {
                            arrowLabel,
                            nameLabel,
                            new StackLayout()
                            {
                                Orientation =  StackOrientation.Horizontal,
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                Children =
                                {
                                    startLabel,
                                    arrowLabel,
                                    endLabel
                                }
                            }
                            
                        }
                    }
                };
            });
            ListViewClasses.GroupHeaderTemplate = new DataTemplate(() =>
            {
                Label header = new Label();
                header.SetBinding(Label.TextProperty,"Title");
                header.FontAttributes = FontAttributes.Bold;
                header.FontSize = 30;
                header.TextColor = Color.FromHex("#2196F3");
                header.HorizontalOptions =LayoutOptions.Center;
                header.VerticalOptions = LayoutOptions.Center;
                header.HorizontalTextAlignment = TextAlignment.Center;
                header.VerticalTextAlignment = TextAlignment.Center;
                return new ViewCell
                {
                    View = new StackLayout(){
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Children =
                        {
                            header
                        }
                    }
                };
            });
        }
    }
}