using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Location
    {
        public Location()
        {
            Coach = new HashSet<Coach>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int SportId { get; set; }

        public virtual Sport Sport { get; set; }
        public virtual ICollection<Coach> Coach { get; set; }
    }
}
