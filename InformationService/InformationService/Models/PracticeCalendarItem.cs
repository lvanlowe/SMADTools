using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class PracticeCalendarItems
    {

        public long Id { get; set; }
        public int Length { get; set; }
        public long CalendarItemId { get; set; }
        public long ProgramId { get; set; }
        public long? SportTypeId { get; set; }
        public long? CalendarLengthId { get; set; }
        public long? TeamId { get; set; }

        public virtual CalendarItems CalendarItem { get; set; }
        public virtual CalendarLengths CalendarLength { get; set; }
        public virtual Programs Program { get; set; }
        public virtual SportTypes SportType { get; set; }
        public virtual Teams Team { get; set; }
    }
}
