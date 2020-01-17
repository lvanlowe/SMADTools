using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class TournamentGame
    {
        public long id { get; set; }

        public string Field { get; set; }

        public long CalendarTimeId { get; set; }

        public long TournamentCalendarItem { get; set; }

        public virtual CalendarTime CalendarTime { get; set; }

        public virtual TournamentCalendarItem TournamentCalendarItem1 { get; set; }
    }
}
