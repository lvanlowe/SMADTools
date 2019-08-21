using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModels
{
    public class CoachTextDto : IMessageDto
    {
        public string Message { get; set; }
        public string Phone { get; set; }
        public long SportId { get; set; }
        public long? ProgramId { get; set; }
        public long? SportTypeId { get; set; }
        public long? TeamId { get; set; }
        public bool? Selected { get; set; }
        public bool? IsVolunteer { get; set; }
    }
}
