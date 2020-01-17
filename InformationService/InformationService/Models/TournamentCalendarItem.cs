using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class TournamentCalendarItem
    {
        public TournamentCalendarItem()
        {
            TournamentGames = new HashSet<TournamentGame>();
        }

        public long id { get; set; }

        public long CalendarItemId { get; set; }

        public long SportId { get; set; }

        public long? SportTypeId { get; set; }

        public long? TeamId { get; set; }

        public virtual CalendarItem CalendarItem { get; set; }

        public virtual ICollection<TournamentGame> TournamentGames { get; set; }
    }
}
