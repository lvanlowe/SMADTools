using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class StateGameCalendarItems
    {
        public long Id { get; set; }
        public long CalendarItemId { get; set; }
        public long SportId { get; set; }

        public virtual CalendarItems CalendarItem { get; set; }
        public virtual Sports Sport { get; set; }
    }
}
