using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class CalendarLengths
    {
        public CalendarLengths()
        {
            PracticeCalendarItems = new HashSet<PracticeCalendarItems>();
        }

        public long Id { get; set; }
        public string TimeLength { get; set; }

        public virtual ICollection<PracticeCalendarItems> PracticeCalendarItems { get; set; }
    }
}
