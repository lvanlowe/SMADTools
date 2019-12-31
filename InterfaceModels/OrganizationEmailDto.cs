using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModels
{
    public class OrganizationEmailDto
    {
        public string From { get; set; }
        public string Copy { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
        public bool IsAthlete { get; set; }
        public bool IsVolunteer { get; set; }

    }
}
