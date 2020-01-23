using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class SportTypes
    {
        public SportTypes()
        {
            Teams = new HashSet<Teams>();
            TournamentCalendarItems = new HashSet<TournamentCalendarItems>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public long SportId { get; set; }

        public virtual Sports Sport { get; set; }
        public virtual ICollection<Teams> Teams { get; set; }
        public virtual ICollection<TournamentCalendarItems> TournamentCalendarItems { get; set; }

    }
}
