using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModels
{
    public class TournamentTeamDto
    {
        public long TeamId { get; set; }
        public long CategoryId { get; set; }
        public long LocationId { get; set; }
        public long Game1TimeId { get; set; }
        public long Game2TimeId { get; set; }
        public long OrTimeId { get; set; }
        public string Game1Note { get; set; }
        public string Game2Note { get; set; }
        public string Note { get; set; }
    }
}
