using System;
using System.Collections.Generic;

namespace InformationService.Models
{
    public partial class Athletes
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string Street { get; set; }
        public DateTime? BirthDate { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string MF { get; set; }
        public string HomePhone { get; set; }
        public DateTime? MedicalDate { get; set; }
        public DateTime? MedicalExpirationDate { get; set; }
        public string ParentWorkPhone { get; set; }
        public string Mother { get; set; }
        public string Email { get; set; }
        public string Father { get; set; }
        public string Guardian { get; set; }
        public string TeeShirtSize { get; set; }
        public bool HasTeeShirt { get; set; }
    }
}
