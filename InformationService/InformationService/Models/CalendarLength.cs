using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class CalendarLength
    {
        public CalendarLength()
        {
            PracticeCalendarItems = new HashSet<PracticeCalendarItem>();
        }

        public long id { get; set; }

        public string TimeLength { get; set; }

        public virtual ICollection<PracticeCalendarItem> PracticeCalendarItems { get; set; }
    }
}
