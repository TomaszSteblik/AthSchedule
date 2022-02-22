using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Ical.Net;

namespace AthSchedule
{
    public class Schedule 
    {
        public List<Class> Classes { get;}
        private int _weekNumber;
        private readonly List<DateTime> _thisWeek;
        private readonly int _groupId;
        
        public Schedule(int groupId)
        {
            _groupId = groupId;
            DateTime date = DateTime.Now;
            _weekNumber = 479 + (date - new DateTime(2020,10,26)).Days/7;
            _thisWeek = new List<DateTime> {date};
            while (date.DayOfWeek != DayOfWeek.Sunday)
            {
                date = date.Add(TimeSpan.FromDays(1));
                _thisWeek.Add(date.Date);
            }
            date = DateTime.Now;
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.Subtract(TimeSpan.FromDays(1));
                _thisWeek.Add(date.Date);
            }
            Classes = new List<Class>();
            
        }

        public async Task DownloadWeekSchedule()
        {
            Classes.Clear();
            using var client = new HttpClient();
            // ReSharper disable once StringLiteralTypo
            try
            {
                var content = await client.GetStringAsync($"https://plany.ath.bielsko.pl/plan.php?type=0&id={_groupId}&w={_weekNumber}&cvsfile=true");
                var calendar = Calendar.Load(content);

                foreach (var eEvent in calendar.Events)
                {
                    if (_thisWeek.Contains(eEvent.DtStart.Date) )
                    {
                        Classes.Add(new Class(eEvent.DtStart.AsUtc.ToLocalTime(),eEvent.DtEnd.AsUtc.ToLocalTime(),eEvent.Summary));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Classes.Add(new Class(DateTime.MinValue, DateTime.MaxValue, "Brak danych"));
            }

            
        }
        public void DownloadWeekScheduleSync()
        {
            Classes.Clear();
            using var client = new HttpClient();
            // ReSharper disable once StringLiteralTypo
            var content = client.GetStringAsync($"https://plany.ath.bielsko.pl/plan.php?type=0&id={_groupId}&w={_weekNumber}&cvsfile=true").Result;
            try
            {
                var calendar = Calendar.Load(content);

                foreach (var eEvent in calendar.Events)
                {
                    if (_thisWeek.Contains(eEvent.DtStart.Date) || eEvent.DtStart.Date == DateTime.Now.Date )
                    {
                        Classes.Add(new Class(eEvent.DtStart.AsUtc.ToLocalTime(),eEvent.DtEnd.AsUtc.ToLocalTime(),eEvent.Summary));
                    }
                }
                Console.WriteLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Classes.Add(new Class(DateTime.MinValue, DateTime.MaxValue, "Brak danych"));
            }

        }
        
        public async Task GoNextWeek()
        {
            for (int i = 0; i < _thisWeek.Count; i++)
            {
                _thisWeek[i] = _thisWeek[i].AddDays(7);
            }
            _weekNumber++;
            await DownloadWeekSchedule(); 

        }

        public async Task GoLastWeek()
        {
            for (int i = 0; i < _thisWeek.Count; i++)
            {
                _thisWeek[i] = _thisWeek[i].Subtract(TimeSpan.FromDays(7));
            }
            _weekNumber--;
            await DownloadWeekSchedule();

        }
    }
}