using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Team
    {
        public Team()
        {
            Athlete = new HashSet<Athlete>();
        }

        public int TeamId { get; set; }
        public int AreaId { get; set; }
        public string TeamName { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<Athlete> Athlete { get; set; }
    }
}
