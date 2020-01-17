using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Location
    {
        public Location()
        {
            CalendarItems = new HashSet<CalendarItem>();
        }

        public long id { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public virtual ICollection<CalendarItem> CalendarItems { get; set; }
    }
}
