using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class CalendarTime
    {
        public CalendarTime()
        {
            CalendarItems = new HashSet<CalendarItem>();
            TournamentGames = new HashSet<TournamentGame>();
        }

        public long id { get; set; }

        public string TimeHour { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalendarItem> CalendarItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TournamentGame> TournamentGames { get; set; }
    }
}
