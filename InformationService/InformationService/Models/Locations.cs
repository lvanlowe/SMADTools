using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class Locations
    {
        public Locations()
        {
            CalendarItems = new HashSet<CalendarItems>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual ICollection<CalendarItems> CalendarItems { get; set; }
    }
}
