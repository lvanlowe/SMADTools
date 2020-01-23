using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Registrant
    {
        public Registrant()
        {
            RegisteredAthlete = new HashSet<RegisteredAthlete>();
            RegistrantEmail = new HashSet<RegistrantEmail>();
            RegistrantPhone = new HashSet<RegistrantPhone>();
        }

        public int Id { get; set; }
        public long SportId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public long ProgramId { get; set; }
        public string Size { get; set; }
        public long? SportTypeId { get; set; }
        public long? TeamId { get; set; }
        public bool? Selected { get; set; }
        public string Year { get; set; }
        public int? SizeId { get; set; }
        public bool? IsVolunteer { get; set; }
        public virtual Sports Sport { get; set; }
        public virtual Teams Team { get; set; }
        public virtual ICollection<RegisteredAthlete> RegisteredAthlete { get; set; }
        public virtual ICollection<RegistrantEmail> RegistrantEmail { get; set; }
        public virtual ICollection<RegistrantPhone> RegistrantPhone { get; set; }
    }
}
