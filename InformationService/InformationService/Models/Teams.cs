using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Teams
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProgramId { get; set; }
        public long? SportType { get; set; }

        public virtual Programs Program { get; set; }
        public virtual SportTypes SportTypeNavigation { get; set; }
    }
}
