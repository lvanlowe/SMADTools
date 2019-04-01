using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Carrier
    {
        public Carrier()
        {
            RegistrantPhone = new HashSet<RegistrantPhone>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }

        public virtual ICollection<RegistrantPhone> RegistrantPhone { get; set; }
    }
}
