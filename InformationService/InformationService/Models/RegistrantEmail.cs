using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class RegistrantEmail
    {
        public int Id { get; set; }
        public int RegistrantId { get; set; }
        public string Email { get; set; }

        public virtual Registrant Registrant { get; set; }
    }
}
