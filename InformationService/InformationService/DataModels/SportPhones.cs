using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.DataModels
{
    public class SportPhones
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public long ProgramId { get; set; }
        public long? SportTypeId { get; set; }
        public long? TeamId { get; set; }
        public bool? Selected { get; set; }
        public bool? IsVolunteer { get; set; }
        public string Phone { get; set; }
    }
}
