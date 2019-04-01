using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Sport
    {
        public Sport()
        {
            Location = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanRegister { get; set; }

        public virtual ICollection<Location> Location { get; set; }
    }
}
