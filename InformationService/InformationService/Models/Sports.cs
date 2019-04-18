using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class Sports
    {
        public Sports()
        {
            Programs = new HashSet<Programs>();
            SportTypes = new HashSet<SportTypes>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsTeamSport { get; set; }
        public bool CanRegister { get; set; }
        public bool HasUniform { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Programs> Programs { get; set; }
        public virtual ICollection<SportTypes> SportTypes { get; set; }
    }
}
