using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Athlete
    {
        public Athlete()
        {
            AthleteEvent = new HashSet<AthleteEvent>();
        }

        public int AthleteId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
        public bool ScratchInd { get; set; }

        public virtual Teams Teams { get; set; }
        public virtual ICollection<AthleteEvent> AthleteEvent { get; set; }
    }
}
