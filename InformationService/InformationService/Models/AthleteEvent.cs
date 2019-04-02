using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class AthleteEvent
    {
        public int AthleteEventId { get; set; }
        public int AthleteId { get; set; }
        public int EventId { get; set; }
        public TimeSpan? TrackTime { get; set; }
        public decimal? FieldDistance { get; set; }
        public int? Heat { get; set; }
        public int? Division { get; set; }

        public virtual Athlete Athlete { get; set; }
        public virtual Event Event { get; set; }
    }
}
