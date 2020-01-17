using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class PracticeCalendarItem
    {
        public long id { get; set; }

        public int Length { get; set; }

        public long CalendarItemId { get; set; }

        public long ProgramId { get; set; }

        public long? SportTypeId { get; set; }

        public long? CalendarLengthId { get; set; }

        public long? TeamId { get; set; }

        public virtual CalendarItem CalendarItem { get; set; }

        public virtual CalendarLength CalendarLength { get; set; }
    }
}
