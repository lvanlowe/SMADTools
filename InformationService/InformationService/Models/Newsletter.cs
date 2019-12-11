using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class Newsletter
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsVolunteer { get; set; }

        public bool IsAthlete { get; set; }
    }
}
