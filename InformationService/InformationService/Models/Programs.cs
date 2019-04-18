using System;
using System.Collections.Generic;
using System.Text;

namespace InformationService.Models
{
    public partial class Programs
    {
        public Programs()
        {
            Teams = new HashSet<Teams>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public long Sport { get; set; }

        public virtual Sports SportNavigation { get; set; }
        public virtual ICollection<Teams> Teams { get; set; }
    }
}
