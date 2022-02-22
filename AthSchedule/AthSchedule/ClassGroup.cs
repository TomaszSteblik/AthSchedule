using System.Collections.Generic;

namespace AthSchedule
{
    public class ClassGroup : List<Class>
    {
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public ClassGroup(string title, string shortTitle)
        {
            Title = title;
            ShortTitle = shortTitle;
        }
    }
}