using System;

namespace AthSchedule
{
    public class Class
    {
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }
        public string Name { get; }
        public DayOfWeek DayOfWeek { get; }
        public string StartTime { get; }
        public string EndTime { get; }

        public Class(DateTime startDateTime, DateTime endDateTime, string name)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Name = name;
            DayOfWeek = StartDateTime.DayOfWeek;
            StartTime = startDateTime.ToShortTimeString();
            EndTime = endDateTime.ToShortTimeString();
        }
    }
}