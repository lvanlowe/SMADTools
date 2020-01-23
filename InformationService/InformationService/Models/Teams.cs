using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Teams
    {
        public Teams()
        {
            Registrant = new HashSet<Registrant>();
            TournamentCalendarItems = new HashSet<TournamentCalendarItems>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long ProgramId { get; set; }
        public long? SportType { get; set; }

        public virtual Programs Program { get; set; }
        public virtual SportTypes SportTypeNavigation { get; set; }
        public virtual ICollection<Registrant> Registrant { get; set; }
        public virtual ICollection<TournamentCalendarItems> TournamentCalendarItems { get; set; }

    }
}
