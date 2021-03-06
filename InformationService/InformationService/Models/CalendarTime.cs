﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class CalendarTimes
    {
        public CalendarTimes()
        {
            CalendarItems = new HashSet<CalendarItems>();
            TournamentGames = new HashSet<TournamentGames>();
        }

        public long Id { get; set; }
        public string TimeHour { get; set; }

        public virtual ICollection<CalendarItems> CalendarItems { get; set; }
        public virtual ICollection<TournamentGames> TournamentGames { get; set; }
    }
}
