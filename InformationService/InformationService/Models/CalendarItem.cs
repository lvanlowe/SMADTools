using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class CalendarItem
    {
        public CalendarItem()
        {
            PracticeCalendarItems = new HashSet<PracticeCalendarItem>();
            StateGameCalendarItems = new HashSet<StateGameCalendarItem>();
            TournamentCalendarItems = new HashSet<TournamentCalendarItem>();
        }

        public long id { get; set; }

        public DateTime ItemDate { get; set; }

        public short ItemType { get; set; }

        public string ItemName { get; set; }

        public string Comments { get; set; }

        public string ItemTime { get; set; }

        public long LocationId { get; set; }

        public long? CalendarTimeId { get; set; }

        public string CancelReason { get; set; }

        public virtual CalendarTime CalendarTime { get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<PracticeCalendarItem> PracticeCalendarItems { get; set; }

        public virtual ICollection<StateGameCalendarItem> StateGameCalendarItems { get; set; }

        public virtual ICollection<TournamentCalendarItem> TournamentCalendarItems { get; set; }
    }
}
