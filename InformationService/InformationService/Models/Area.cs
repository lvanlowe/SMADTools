using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Area
    {
        public Area()
        {
            Team = new HashSet<Team>();
        }

        public int AreaId { get; set; }
        public int Number { get; set; }

        public virtual ICollection<Team> Team { get; set; }
    }
}
