using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModels
{
    public class RegistrantDto
    {
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
        public bool? IsVolunteer { get; set; }
        public int AthletesId { get; set; }
        public int RegisteredAthletesId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public DateTime? MedicalExpirationDate { get; set; }
        public int RegistrantEmail1Id { get; set; }
        public string Email1 { get; set; }
        public int RegistrantEmail2Id { get; set; }
        public string Email2 { get; set; }
        public int RegistrantEmail3Id { get; set; }
        public string Email3 { get; set; }
        public int RegistrantPhone1Id { get; set; }
        public string Phone1 { get; set; }
        public string PhoneType1 { get; set; }
        public bool CanText1 { get; set; }
        public int RegistrantPhone2Id { get; set; }
        public string Phone2 { get; set; }
        public string PhoneType2 { get; set; }
        public bool CanText2 { get; set; }
        public int RegistrantPhone3Id { get; set; }
        public string Phone3 { get; set; }
        public string PhoneType3 { get; set; }
        public bool CanText3 { get; set; }

    }
}
