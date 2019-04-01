using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class RegisteredAthlete
    {
        public int Id { get; set; }
        public int RegistrantId { get; set; }
        public int AthletesId { get; set; }
        public long? UniformsId { get; set; }

        public virtual Registrant Registrant { get; set; }
        public virtual Uniforms Uniforms { get; set; }
    }
}
