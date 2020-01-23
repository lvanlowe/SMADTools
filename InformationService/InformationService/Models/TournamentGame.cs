using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class TournamentGames
    {
        public long Id { get; set; }
        public string Field { get; set; }
        public long CalendarTimeId { get; set; }
        public long TournamentCalendarItem { get; set; }

        public virtual CalendarTimes CalendarTime { get; set; }
        public virtual TournamentCalendarItems TournamentCalendarItemNavigation { get; set; }
    }
}
