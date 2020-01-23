using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class TournamentCalendarItems
    {
        public TournamentCalendarItems()
        {
            TournamentGames = new HashSet<TournamentGames>();
        }

        public long Id { get; set; }
        public long CalendarItemId { get; set; }
        public long SportId { get; set; }
        public long? SportTypeId { get; set; }
        public long? TeamId { get; set; }

        public virtual CalendarItems CalendarItem { get; set; }
        public virtual Sports Sport { get; set; }
        public virtual SportTypes SportType { get; set; }
        public virtual Teams Team { get; set; }
        public virtual ICollection<TournamentGames> TournamentGames { get; set; }
    }
}
