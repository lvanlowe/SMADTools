using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class CalendarItems
    {
        public CalendarItems()
        {
            PracticeCalendarItems = new HashSet<PracticeCalendarItems>();
            StateGameCalendarItems = new HashSet<StateGameCalendarItems>();
            TournamentCalendarItems = new HashSet<TournamentCalendarItems>();
        }

        public long Id { get; set; }
        public DateTime ItemDate { get; set; }
        public short ItemType { get; set; }
        public string ItemName { get; set; }
        public string Comments { get; set; }
        public string ItemTime { get; set; }
        public long LocationId { get; set; }
        public long? CalendarTimeId { get; set; }
        public string CancelReason { get; set; }

        public virtual CalendarTimes CalendarTime { get; set; }
        public virtual Locations Location { get; set; }
        public virtual ICollection<PracticeCalendarItems> PracticeCalendarItems { get; set; }
        public virtual ICollection<StateGameCalendarItems> StateGameCalendarItems { get; set; }
        public virtual ICollection<TournamentCalendarItems> TournamentCalendarItems { get; set; }
    }
}
