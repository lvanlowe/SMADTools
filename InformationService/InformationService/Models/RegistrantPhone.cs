using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class RegistrantPhone
    {
        public int Id { get; set; }
        public int RegistrantId { get; set; }
        public string Phone { get; set; }
        public int? CarrierId { get; set; }
        public string PhoneType { get; set; }
        public bool CanText { get; set; }
        public int? PhoneTypeId { get; set; }

        public virtual Carrier Carrier { get; set; }
        public virtual PhoneTypes PhoneTypeNavigation { get; set; }
        public virtual Registrant Registrant { get; set; }
    }
}
