using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.DataModels
{
    public class EventInformation
    {
        public string Message { get; set; }
        public string From { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public long SportId { get; set; }
        public long ProgramId { get; set; }


    }
}
