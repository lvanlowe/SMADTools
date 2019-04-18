using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Area
    {
        public Area()
        {
            Team = new HashSet<Teams>();
        }

        public int AreaId { get; set; }
        public int Number { get; set; }

        public virtual ICollection<Teams> Team { get; set; }
    }
}
