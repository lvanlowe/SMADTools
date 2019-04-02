using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Event
    {
        public Event()
        {
            AthleteEvent = new HashSet<AthleteEvent>();
        }

        public int EventId { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }

        public virtual ICollection<AthleteEvent> AthleteEvent { get; set; }
    }
}
