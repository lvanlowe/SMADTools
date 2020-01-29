using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModels
{
    public class SportLocationDto
    {
        public long SportId { get; set; }
        public string SportName { get; set; }
        public bool IsTeamSport { get; set; }
        public bool CanRegister { get; set; }
        public bool HasUniform { get; set; }
        public string Email { get; set; }
        public long ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int Year { get; set; }

    }
}
