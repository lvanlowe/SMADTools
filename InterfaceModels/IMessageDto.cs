using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModels
{
    public interface IMessageDto
    {
        long SportId { get; set; }
        long? ProgramId { get; set; }
        long? SportTypeId { get; set; }
        long? TeamId { get; set; }
        bool? Selected { get; set; }
        bool? IsVolunteer { get; set; }

    }
}
