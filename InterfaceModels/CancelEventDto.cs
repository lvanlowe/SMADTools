using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModels
{
    public class CancelEventDto
    {
        public string CancelReason { get; set; }
        public string CancelNote { get; set; }
        public long PracticeId { get; set; }
    }
}
