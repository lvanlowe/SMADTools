 using System;

namespace CoreModels
{
    public class CoachEmail
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Copy { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
        public long ProgramId { get; set; }
        public long? SportTypeId { get; set; }
        public long? TeamId { get; set; }
        public bool? Selected { get; set; }
        public bool? IsVolunteer { get; set; }

    }
}
