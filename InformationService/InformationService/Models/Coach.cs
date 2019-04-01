using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Coach
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
    }
}
