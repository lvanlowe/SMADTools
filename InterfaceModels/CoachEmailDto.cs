﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModels
{
    public class CoachEmailDto : IMessageDto
    {
        public string From { get; set; }
        public string Copy { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
        public long SportId { get; set; }
        public long? ProgramId { get; set; }
        public long? SportTypeId { get; set; }
        public long? TeamId { get; set; }
        public bool? Selected { get; set; }
        public bool? IsVolunteer { get; set; }

    }
}
