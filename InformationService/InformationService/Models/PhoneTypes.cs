using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class PhoneTypes
    {
        public PhoneTypes()
        {
            RegistrantPhone = new HashSet<RegistrantPhone>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool AllowText { get; set; }

        public virtual ICollection<RegistrantPhone> RegistrantPhone { get; set; }
    }
}
