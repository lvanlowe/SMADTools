using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModels
{
    public class TournamentDto
    {
        public DateTime TournamentDate { get; set; }
        public string TournamentName { get; set; }
        public List<TournamentTeamDto> Teams { get; set; }
    }
}
