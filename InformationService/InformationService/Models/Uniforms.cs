using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Uniforms
    {
        public Uniforms()
        {
            RegisteredAthlete = new HashSet<RegisteredAthlete>();
        }

        public long Id { get; set; }
        public string Size { get; set; }
        public int Jersey { get; set; }
        public string UniqueId { get; set; }
        public string Comments { get; set; }
        public bool Aassigned { get; set; }
        public bool? Accepted { get; set; }
        public bool? Returned { get; set; }
        public long? SportId { get; set; }

        public virtual ICollection<RegisteredAthlete> RegisteredAthlete { get; set; }
    }
}
