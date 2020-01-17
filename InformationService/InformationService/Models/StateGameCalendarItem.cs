using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class StateGameCalendarItem
    {
        public long id { get; set; }

        public long CalendarItemId { get; set; }

        public long SportId { get; set; }

        public virtual CalendarItem CalendarItem { get; set; }
    }
}
