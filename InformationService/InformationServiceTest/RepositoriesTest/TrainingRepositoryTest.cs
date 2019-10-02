using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationService.Models;
using InformationService.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InformationServiceTest.RepositoriesTest
{
    public class TrainingRepositoryTest
    {
        private PwsodbContext _context;

        public TrainingRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PwsodbContext>().
                UseInMemoryDatabase(databaseName: "TrainingRepository")
                .Options;
            _context = new PwsodbContext(options);
        }

        private void InitializeRegistrants()
        {
            var emails = _context.RegistrantEmail.ToListAsync();
            _context.RemoveRange(emails.Result);
            var phones = _context.RegistrantPhone.ToListAsync();
            _context.RemoveRange(phones.Result);
            var athletes = _context.RegisteredAthlete.ToListAsync();
            _context.RemoveRange(athletes.Result);
            var registrants = _context.Registrant.ToListAsync();
            _context.RemoveRange(registrants.Result);
            _context.SaveChanges();
        }

        private void LoadRegistrants()
        {
            RegistrantEmail email11 = new RegistrantEmail() { Id = 1, Email = "clark@superman.com", RegistrantId = 1 };
            RegistrantEmail email21 = new RegistrantEmail() { Id = 2, Email = "bruce@batman.com", RegistrantId = 2 };
            RegistrantEmail email22 = new RegistrantEmail() { Id = 3, Email = "alfred@batman.com", RegistrantId = 2 };
            RegistrantEmail email41 = new RegistrantEmail() { Id = 4, Email = "hal@greenlantern.org", RegistrantId = 4 };
            RegistrantEmail email51 = new RegistrantEmail() { Id = 5, Email = "diana@wonder.com", RegistrantId = 5 };
            RegistrantEmail email52 = new RegistrantEmail() { Id = 6, Email = "steve@wonder.com", RegistrantId = 5 };
            RegistrantEmail email53 = new RegistrantEmail() { Id = 7, Email = "fannie@wonder.com", RegistrantId = 5 };
            RegistrantEmail email61 = new RegistrantEmail() { Id = 8, Email = "oliver@arrow.net", RegistrantId = 6};
            RegistrantEmail email71 = new RegistrantEmail() { Id = 9, Email = "ray@atom.org", RegistrantId = 7 };
            RegistrantEmail email72 = new RegistrantEmail() { Id = 10, Email = "palmer@atom.org", RegistrantId = 7 };
            RegistrantEmail email81 = new RegistrantEmail() { Id = 11, Email = "jj@manhunter.com", RegistrantId = 8 };
            RegistrantEmail email82 = new RegistrantEmail() { Id = 12, Email = "jones@manhunter.com", RegistrantId = 8 };
            RegistrantEmail email83 = new RegistrantEmail() { Id = 13, Email = "john@manhunter.com", RegistrantId = 8 };
            RegistrantEmail email91 = new RegistrantEmail() { Id = 14, Email = "dick@batman.com", RegistrantId = 9 };
            RegistrantEmail email01 = new RegistrantEmail() { Id = 15, Email = "barbara@batman.com", RegistrantId = 10 };
            RegistrantEmail email02 = new RegistrantEmail() { Id = 16, Email = "iris@flash.net", RegistrantId = 10 };

            RegistrantPhone phone11 = new RegistrantPhone() { Id = 1, RegistrantId = 1, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "7035551212" };
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = 2, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = 2, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            RegistrantPhone phone31 = new RegistrantPhone() { Id = 4, RegistrantId = 3, CanText = false, PhoneTypeId = 2, Phone = "2025551212" };
            RegistrantPhone phone41 = new RegistrantPhone() { Id = 5, RegistrantId = 4, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "7185551212" };
            RegistrantPhone phone42 = new RegistrantPhone() { Id = 6, RegistrantId = 4, CanText = false, PhoneTypeId = 2, Phone = "2035551212" };
            RegistrantPhone phone51 = new RegistrantPhone() { Id = 7, RegistrantId = 5, CanText = false, PhoneTypeId = 2, Phone = "4045551212" };
            RegistrantPhone phone52 = new RegistrantPhone() { Id = 8, RegistrantId = 5, CanText = false, PhoneTypeId = 3, Phone = "9185551212" };
            RegistrantPhone phone61 = new RegistrantPhone() { Id = 9, RegistrantId = 6, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "2045551212" };
            RegistrantPhone phone62 = new RegistrantPhone() { Id = 10, RegistrantId = 6, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "2055551212" };
            RegistrantPhone phone63 = new RegistrantPhone() { Id = 11, RegistrantId = 6, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "2065551212" };
            RegistrantPhone phone71 = new RegistrantPhone() { Id = 12, RegistrantId = 7, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "2075551212" };
            RegistrantPhone phone72 = new RegistrantPhone() { Id = 13, RegistrantId = 7, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "2085551212" };
            RegistrantPhone phone73 = new RegistrantPhone() { Id = 14, RegistrantId = 7, CanText = false, PhoneTypeId = 3, Phone = "2125551212" };
            RegistrantPhone phone81 = new RegistrantPhone() { Id = 15, RegistrantId = 8, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "2135551212" };
            RegistrantPhone phone82 = new RegistrantPhone() { Id = 16, RegistrantId = 8, CanText = false, PhoneTypeId = 3, Phone = "2145551212" };
            RegistrantPhone phone83 = new RegistrantPhone() { Id = 17, RegistrantId = 8, CanText = false, PhoneTypeId = 2, Phone = "2155551212" };
            RegistrantPhone phone91 = new RegistrantPhone() { Id = 18, RegistrantId = 9, CanText = false, PhoneTypeId = 2, Phone = "2165551212" };
            RegistrantPhone phone92 = new RegistrantPhone() { Id = 19, RegistrantId = 9, CanText = false, PhoneTypeId = 3, Phone = "2175551212" };
            RegistrantPhone phone93 = new RegistrantPhone() { Id = 20, RegistrantId = 9, CanText = false, PhoneTypeId = 3, Phone = "2185551212" };


            RegisteredAthlete athlete1 = new RegisteredAthlete() { AthletesId = 10, RegistrantId = 1 };
            RegisteredAthlete athlete2 = new RegisteredAthlete() { AthletesId = 11, RegistrantId = 2 };
            RegisteredAthlete athlete4 = new RegisteredAthlete() { AthletesId = 12, RegistrantId = 4 };
            RegisteredAthlete athlete7 = new RegisteredAthlete() { AthletesId = 13, RegistrantId = 7 };
            RegisteredAthlete athlete0 = new RegisteredAthlete() { AthletesId = 14, RegistrantId = 10 };

            var registrant1 = new Registrant()
            {
                Id = 1,
                FirstName = "Clark",
                LastName = "Kent",
                ProgramId = 1,
                SportId = 1,
                SportTypeId = 1,
                TeamId = 1,
                Selected = true,

            }; var registrant2 = new Registrant()
            {
                Id = 2,
                FirstName = "Bruce",
                LastName = "Wayne",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 1,
                TeamId = 1,
                Selected = true,
                Year = "1999"
            }; var registrant3 = new Registrant()
            {
                Id = 3,
                FirstName = "Barry",
                LastName = "Allen",
                ProgramId = 3,
                SportId = 2,
                SportTypeId = 1,
                TeamId = 2,
                IsVolunteer = true
            }; var registrant4 = new Registrant()
            {
                Id = 4,
                FirstName = "Hal",
                LastName = "Jordan",
                ProgramId = 3,
                SportId = 1,
                SportTypeId = 1,
                TeamId = 2,
                Selected = true
            }; var registrant5 = new Registrant()
            {
                Id = 5,
                FirstName = "Diana",
                LastName = "Prince",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 2,
                TeamId = 3
            }; var registrant6 = new Registrant()
            {
                Id = 6,
                FirstName = "Oliver",
                LastName = "Queen",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 2,
                TeamId = 3
            }; var registrant7 = new Registrant()
            {
                Id = 7,
                FirstName = "Ray",
                LastName = "Palmer",
                ProgramId = 3,
                SportId = 2,
                SportTypeId = 4,
                TeamId = 4
            }; var registrant8 = new Registrant()
            {
                Id = 8,
                FirstName = "John",
                LastName = "Jones",
                ProgramId = 3,
                SportId = 2,
                SportTypeId = 4,
                TeamId = 4
            }; var registrant9 = new Registrant()
            {
                Id = 9,
                FirstName = "Dick",
                LastName = "Grayson",
                ProgramId = 4,
                SportId = 3,
                IsVolunteer = true
            }; var registrant0  = new Registrant()
            {
                Id = 10,
                FirstName = "Iris",
                LastName = "West",
                ProgramId = 4,
                SportId = 3,
            };
            registrant0.RegistrantEmail.Add(email01);
            registrant0.RegistrantEmail.Add(email02);
            registrant1.RegistrantEmail.Add(email11);
            registrant2.RegistrantEmail.Add(email21);
            registrant2.RegistrantEmail.Add(email22);
            registrant4.RegistrantEmail.Add(email41);
            registrant5.RegistrantEmail.Add(email51);
            registrant5.RegistrantEmail.Add(email52);
            registrant5.RegistrantEmail.Add(email53);
            registrant6.RegistrantEmail.Add(email61);
            registrant7.RegistrantEmail.Add(email71);
            registrant7.RegistrantEmail.Add(email72);
            registrant8.RegistrantEmail.Add(email81);
            registrant8.RegistrantEmail.Add(email82);
            registrant8.RegistrantEmail.Add(email83);
            registrant9.RegistrantEmail.Add(email91);
            registrant1.RegistrantPhone.Add(phone11);
            registrant2.RegistrantPhone.Add(phone21);
            registrant2.RegistrantPhone.Add(phone22);
            registrant3.RegistrantPhone.Add(phone31);
            registrant4.RegistrantPhone.Add(phone41);
            registrant4.RegistrantPhone.Add(phone42);
            registrant5.RegistrantPhone.Add(phone51);
            registrant5.RegistrantPhone.Add(phone52);
            registrant6.RegistrantPhone.Add(phone61);
            registrant6.RegistrantPhone.Add(phone62);
            registrant6.RegistrantPhone.Add(phone63);
            registrant7.RegistrantPhone.Add(phone71);
            registrant7.RegistrantPhone.Add(phone72);
            registrant7.RegistrantPhone.Add(phone73);
            registrant8.RegistrantPhone.Add(phone81);
            registrant8.RegistrantPhone.Add(phone82);
            registrant8.RegistrantPhone.Add(phone83);
            registrant9.RegistrantPhone.Add(phone91);
            registrant9.RegistrantPhone.Add(phone92);
            registrant9.RegistrantPhone.Add(phone93);

            registrant0.RegisteredAthlete.Add(athlete0);
            registrant1.RegisteredAthlete.Add(athlete1);
            registrant2.RegisteredAthlete.Add(athlete2);
            registrant4.RegisteredAthlete.Add(athlete4);
            registrant6.RegisteredAthlete.Add(athlete7);

            _context.Registrant.Add(registrant0);
            _context.Registrant.Add(registrant1);
            _context.Registrant.Add(registrant2);
            _context.Registrant.Add(registrant3);
            _context.Registrant.Add(registrant4);
            _context.Registrant.Add(registrant5);
            _context.Registrant.Add(registrant6);
            _context.Registrant.Add(registrant7);
            _context.Registrant.Add(registrant8);
            _context.Registrant.Add(registrant9);

            _context.SaveChanges();
        }

        [Theory]
        [InlineData(1, 8)]
        [InlineData(2, 5)]
        [InlineData(3, 3)]
        public void GetEmailsBySport_When_executed_create_list_of_SportEmails(int sportId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            var repository = new TrainingRepository(_context);
            var actual = repository.GetEmailsBySport(sportId);

            Assert.Equal(expected, actual.Result.Count);

        }

        [Theory]
        [InlineData(1, 7)]
        [InlineData(2, 3)]
        [InlineData(3, 0)]
        public void GetPhonesBySport_When_executed_create_list_of_SportPhoness(int sportId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            var repository = new TrainingRepository(_context);
            var actual = repository.GetPhonesBySport(sportId);

            Assert.Equal(expected, actual.Result.Count);

        }

        [Theory]
        [InlineData(1, 4, 1, 1, 1)]
        [InlineData(2, 3, 0, 1, 0)]
        [InlineData(3, 2, 2, 0, 1)]
        public void GetRegistrantsBySport_When_executed_create_list_of_SportRegistrants(int sportId, int count, int emails, int phones, int hasMedical)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            var repository = new TrainingRepository(_context);
            var actual = repository.GetRegistrantsBySport(sportId);

            Assert.Equal(count, actual.Result.Count);
            Assert.Equal(emails, actual.Result[0].RegistrantEmail.Count);
            Assert.Equal(phones, actual.Result[0].RegistrantPhone.Count);
            Assert.Equal(hasMedical, actual.Result[0].RegisteredAthlete.Count);

        }

        [Theory]
        [InlineData(3, 10)]
        public void AddRegisteredAthlete_When_executed_add_record(int registrantId, int athleteId)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            int registeredAthleteCount = _context.RegisteredAthlete.Count();

            var repository = new TrainingRepository(_context);
            repository.AddRegisteredAthlete(registrantId, athleteId);

            int newRegisteredAthleteCount = _context.RegisteredAthlete.Count();
            Registrant registrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);

            Assert.Equal(registeredAthleteCount + 1, newRegisteredAthleteCount);
            Assert.True(registrant.RegisteredAthlete.Count > 0);
            Assert.Equal(athleteId, registrant.RegisteredAthlete.FirstOrDefault().AthletesId);

        }

        [Theory]
        //[InlineData(3, 3)]
        [InlineData(2, 4)]
        //[InlineData(10, 2)]
        public void AddPhone_When_executed_add_record_when_2_existing(int registrantId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            RegistrantPhone phone1 = new RegistrantPhone() { RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, Phone = "5715551212" };
            RegistrantPhone phone2 = new RegistrantPhone() { RegistrantId = registrantId, CanText = false, PhoneTypeId = 2, Phone = "4045551212" };

            List<RegistrantPhone> phoneList = new List<RegistrantPhone> {phone1, phone2};

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.AddPhone(phoneList, originalPhoneList);

            Assert.Equal(expected, originalPhoneList.Count);
        }

        [Theory]
        [InlineData(3, 3)]
        //[InlineData(2, 4)]
        //[InlineData(10, 2)]
        public void AddPhone_When_executed_add_record_when_1_existing(int registrantId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            RegistrantPhone phone1 = new RegistrantPhone() { RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, Phone = "5715551212" };

            List<RegistrantPhone> phoneList = new List<RegistrantPhone> { phone1};

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.AddPhone(phoneList, originalPhoneList);

            Assert.Equal(expected, originalPhoneList.Count);
        }

        [Theory]
        [InlineData(10, 2)]
        public void AddPhone_When_executed_add_record_when_0_existing(int registrantId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            List<RegistrantPhone> phoneList = new List<RegistrantPhone> {};

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.AddPhone(phoneList, originalPhoneList);

            Assert.Equal(expected, originalPhoneList.Count);
        }


        [Theory]
        //[InlineData(1, 1, 0)]
        [InlineData(2, 1)]
        //[InlineData(4, 2, 0)]
        public void RemovePhone_When_executed_remove_records_1_of_2(int registrantId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            RegistrantPhone phone1 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };

            List<RegistrantPhone> phoneList = new List<RegistrantPhone> { phone1 };

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.RemovePhone(phoneList, originalPhoneList);

            Assert.Equal(expected, originalPhoneList.Count);
        }

        [Theory]
        //[InlineData(1, 1, 0)]
        //[InlineData(2, 1, 1)]
        [InlineData(4, 0)]
        public void RemovePhone_When_executed_remove_records_2_of_2(int registrantId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            RegistrantPhone phone1 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone2 = new RegistrantPhone() { Id = 3, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };

            List<RegistrantPhone> phoneList = new List<RegistrantPhone> { phone1, phone2 };

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.RemovePhone(phoneList, originalPhoneList);

            Assert.Equal(expected, originalPhoneList.Count);
        }

        [Theory]
        [InlineData(1, 0)]
        //[InlineData(2, 1, 1)]
        //[InlineData(4, 2, 0)]
        public void RemovePhone_When_executed_remove_records_1_of_1(int registrantId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            RegistrantPhone phone1 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };

            List<RegistrantPhone> phoneList = new List<RegistrantPhone> { phone1 };

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            originalPhoneList.Add(phone21);

            var repository = new TrainingRepository(_context);
            repository.RemovePhone(phoneList, originalPhoneList);

            Assert.Equal(expected, originalPhoneList.Count);
        }


        [Theory]
        [InlineData(2, false)]
        public void UpdatePhone_When_executed_update_records_1(int registrantId, bool canText)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantPhone> phoneList = new List<RegistrantPhone>();
            RegistrantPhone phone = new RegistrantPhone() { Id = 2, RegistrantId = 2, CanText = canText, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            phoneList.Add(phone);

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.UpdatePhone(phoneList, originalPhoneList);
            Assert.Equal(canText, phone21.CanText);
            Assert.Equal(!canText, phone22.CanText);
        }

        [Theory]
        [InlineData(2, false)]
        public void UpdatePhone_When_executed_update_records_2(int registrantId, bool canText)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantPhone> phoneList = new List<RegistrantPhone>();
            RegistrantPhone phone = new RegistrantPhone() { Id = 3, RegistrantId = 2, CanText = canText, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            phoneList.Add(phone);

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.UpdatePhone(phoneList, originalPhoneList);
            Assert.Equal(!canText, phone21.CanText);
            Assert.Equal(canText, phone22.CanText);
        }

        [Theory]
        [InlineData(2, false)]
        public void UpdatePhone_When_executed_update_records_1_2(int registrantId, bool canText)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantPhone> phoneList = new List<RegistrantPhone>();
            RegistrantPhone phoneA = new RegistrantPhone() { Id = 2, RegistrantId = 2, CanText = canText, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phoneB = new RegistrantPhone() { Id = 3, RegistrantId = 2, CanText = canText, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            phoneList.Add(phoneA);
            phoneList.Add(phoneB);


            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = registrantId, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.UpdatePhone(phoneList, originalPhoneList);
            Assert.Equal(canText, phone21.CanText);
            Assert.Equal(canText, phone22.CanText);
        }

        [Theory]
        [InlineData(2, false)]
        public void ModifyPhone_When_executed_modify_records_When_only_new(int registrantId, bool canText)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantPhone> phoneList = new List<RegistrantPhone>();
            RegistrantPhone phoneA = new RegistrantPhone() { Id = 2, RegistrantId = 2, CanText = false, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phoneB = new RegistrantPhone() { Id = 3, RegistrantId = 2, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            RegistrantPhone phoneC = new RegistrantPhone() { Id = 0, RegistrantId = 2, CanText = false, PhoneTypeId = 1, CarrierId = 1, Phone = "4125551212" };
            phoneList.Add(phoneA);
            phoneList.Add(phoneB);
            phoneList.Add(phoneC);

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = 2, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = 2, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.ModifyPhone(phoneList, originalPhoneList);
            var registrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);
            Assert.Equal(3, originalPhoneList.Count);
            var phone = originalPhoneList.Where(p => p.Id == 2).FirstOrDefault();
            Assert.Equal(false, phone.CanText);

        }

        [Theory]
        [InlineData(2, false)]
        public void ModifyPhone_When_executed_modify_records_When_remove_and_new(int registrantId, bool canText)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantPhone> phoneList = new List<RegistrantPhone>();
            RegistrantPhone phoneB = new RegistrantPhone() { Id = 3, RegistrantId = 2, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            RegistrantPhone phoneC = new RegistrantPhone() { Id = 0, RegistrantId = 2, CanText = false, PhoneTypeId = 1, CarrierId = 1, Phone = "4125551212" };
            phoneList.Add(phoneB);
            phoneList.Add(phoneC);

            List<RegistrantPhone> originalPhoneList = new List<RegistrantPhone>();
            RegistrantPhone phone21 = new RegistrantPhone() { Id = 2, RegistrantId = 2, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phone22 = new RegistrantPhone() { Id = 3, RegistrantId = 2, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            originalPhoneList.Add(phone21);
            originalPhoneList.Add(phone22);

            var repository = new TrainingRepository(_context);
            repository.ModifyPhone(phoneList, originalPhoneList);
            Assert.Equal(2, originalPhoneList.Count);
        }

        [Theory]
        //[InlineData(1, 3)]
        [InlineData(2, 4)]
        //[InlineData(3, 2)]
        public void AddEmail_When_executed_add_record_when_2_existing(int registrantId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            RegistrantEmail email1 = new RegistrantEmail() { RegistrantId = registrantId,  Email = "arrow@dc.com" };
            RegistrantEmail email2 = new RegistrantEmail() { RegistrantId = registrantId, Email = "speddy@dc.com" };

            List<RegistrantEmail> emailList = new List<RegistrantEmail> { email1, email2 };

            List<RegistrantEmail> originalEmailList = new List<RegistrantEmail>();
            RegistrantEmail email21 = new RegistrantEmail() { Id = 2, Email = "bruce@batman.com", RegistrantId = 2 };
            RegistrantEmail email22 = new RegistrantEmail() { Id = 3, Email = "alfred@batman.com", RegistrantId = 2 };
            originalEmailList.Add(email21);
            originalEmailList.Add(email22);

            var repository = new TrainingRepository(_context);
            repository.AddEmail(emailList, originalEmailList);

            Assert.Equal(expected, originalEmailList.Count);
        }


        [Theory]
        [InlineData(1, 3)]
        //[InlineData(2, 4)]
        //[InlineData(3, 2)]
        public void AddEmail_When_executed_add_record_when_1_existing(int registrantId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            RegistrantEmail email1 = new RegistrantEmail() { RegistrantId = registrantId, Email = "arrow@dc.com" };

            List<RegistrantEmail> emailList = new List<RegistrantEmail> { email1};

            List<RegistrantEmail> originalEmailList = new List<RegistrantEmail>();
            RegistrantEmail email21 = new RegistrantEmail() { Id = 2, Email = "bruce@batman.com", RegistrantId = 2 };
            RegistrantEmail email22 = new RegistrantEmail() { Id = 3, Email = "alfred@batman.com", RegistrantId = 2 };
            originalEmailList.Add(email21);
            originalEmailList.Add(email22);

            var repository = new TrainingRepository(_context);
            repository.AddEmail(emailList, originalEmailList);

            Assert.Equal(expected, originalEmailList.Count);
        }


        [Theory]
        //[InlineData(1, 3)]
        //[InlineData(2, 4)]
        [InlineData(3, 2)]
        public void AddEmail_When_executed_add_record_when_0_existing(int registrantId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            List<RegistrantEmail> emailList = new List<RegistrantEmail> { };

            List<RegistrantEmail> originalEmailList = new List<RegistrantEmail>();
            RegistrantEmail email21 = new RegistrantEmail() { Id = 2, Email = "bruce@batman.com", RegistrantId = 2 };
            RegistrantEmail email22 = new RegistrantEmail() { Id = 3, Email = "alfred@batman.com", RegistrantId = 2 };
            originalEmailList.Add(email21);
            originalEmailList.Add(email22);

            var repository = new TrainingRepository(_context);
            repository.AddEmail(emailList, originalEmailList);

            Assert.Equal(expected, originalEmailList.Count);
        }

        [Theory]
        [InlineData(1, 1, 0)]
        [InlineData(2, 1, 1)]
        [InlineData(4, 2, 0)]
        public void RemoveEmail_When_executed_remove_records(int registrantId, int count, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantEmail> emailList = new List<RegistrantEmail>();

            Registrant beginRegistrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);

            int i = 0;
            foreach (var phone in beginRegistrant.RegistrantEmail)
            {
                if (i < count)
                {
                    emailList.Add(phone);
                }

                i++;
            }


            var repository = new TrainingRepository(_context);
            //repository.RemoveEmail(emailList);
            //Registrant registrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);
            //_context.SaveChanges();
            //if (registrant != null) Assert.Equal(expected, registrant.RegistrantEmail.Count);
        }

        [Theory]
        [InlineData(2, "atom@dc.com")]
        public void UpdateEmail_When_executed_update_records_1(int registrantId, string address)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantEmail> emailList = new List<RegistrantEmail>();
            RegistrantEmail email = new RegistrantEmail() { Id = 2, RegistrantId = 2, Email = address};
            emailList.Add(email);

            var repository = new TrainingRepository(_context);
            //repository.UpdateEmail(emailList);
            //var registrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);
            //var email1 = registrant.RegistrantEmail.FirstOrDefault(r => r.Id == 2);
            //var email2 = registrant.RegistrantEmail.FirstOrDefault(r => r.Id == 3);
            //Assert.Equal(address, email1.Email);
            //Assert.NotEqual(address, email2.Email);
        }

        [Theory]
        [InlineData(2, "atom@dc.com")]
        public void UpdateEmail_When_executed_update_records_1_2(int registrantId, string address)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantEmail> emailList = new List<RegistrantEmail>();
            RegistrantEmail emailA = new RegistrantEmail() { Id = 2, RegistrantId = 2,  Email= address };
            RegistrantEmail emailB = new RegistrantEmail() { Id = 3, RegistrantId = 2, Email = address };
            emailList.Add(emailA);
            emailList.Add(emailB);

            var repository = new TrainingRepository(_context);
            //repository.UpdateEmail(emailList);
            //var registrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);
            //var email1 = registrant.RegistrantEmail.FirstOrDefault(r => r.Id == 2);
            //var email2 = registrant.RegistrantEmail.FirstOrDefault(r => r.Id == 3);
            //Assert.Equal(address, email1.Email);
            //Assert.Equal(address, email2.Email);
        }

        [Theory]
        [InlineData(2, "atom@dc.com")]
        public void UpdateEmail_When_executed_update_records_2(int registrantId, string address)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantEmail> emailList = new List<RegistrantEmail>();
            RegistrantEmail email = new RegistrantEmail() { Id = 3, RegistrantId = 2, Email = address };
            emailList.Add(email);

            var repository = new TrainingRepository(_context);
            //repository.UpdateEmail(emailList);
            //var registrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);
            //var email1 = registrant.RegistrantEmail.FirstOrDefault(r => r.Id == 2);
            //var email2 = registrant.RegistrantEmail.FirstOrDefault(r => r.Id == 3);
            //Assert.Equal(address, email2.Email);
            //Assert.NotEqual(address, email1.Email);
        }

        [Theory]
        [InlineData(2, "atom@dc.com")]
        public void ModifyEmail_When_executed_modify_records_When_only_new(int registrantId, string address)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantEmail> emailList = new List<RegistrantEmail>();
            RegistrantEmail emailA = new RegistrantEmail() { Id = 2, RegistrantId = 2, Email = address };
            RegistrantEmail emailB = new RegistrantEmail() { Id = 3, RegistrantId = 2, Email = address };
            RegistrantEmail emailC = new RegistrantEmail() { Id = 0, RegistrantId = 2, Email = address };
            emailList.Add(emailA);
            emailList.Add(emailB);
            emailList.Add(emailC);

            var repository = new TrainingRepository(_context);
            //repository.ModifyEmail(emailList);
            //var registrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);
            //Assert.Equal(3, registrant.RegistrantEmail.Count);
            //var email = registrant.RegistrantEmail.Where(p => p.Id == 2).FirstOrDefault();
            //Assert.Equal(address, email.Email);

        }

        [Theory]
        [InlineData(2, "atom@dc.com")]
        public void ModifyEmail_When_executed_modify_records_When_remove_and_new(int registrantId, string address)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantEmail> emailList = new List<RegistrantEmail>();
            RegistrantEmail emailB = new RegistrantEmail() { Id = 3, RegistrantId = 2, Email = address };
            RegistrantEmail emailC = new RegistrantEmail() { Id = 0, RegistrantId = 2, Email = address };
            emailList.Add(emailB);
            emailList.Add(emailC);

            var repository = new TrainingRepository(_context);
            //repository.ModifyEmail(emailList);
            //var registrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);
            //_context.SaveChangesAsync();

            //Assert.Equal(2, registrant.RegistrantEmail.Count);
        }

        [Theory]
        [InlineData(2, "atom@dc.com")]
        public void UpdateRegistrant_When_executed_update_email_phone_record(int registrantId, string address)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();
            List<RegistrantEmail> emailList = new List<RegistrantEmail>();
            RegistrantEmail email = new RegistrantEmail() { Id = 3, RegistrantId = 2, Email = address };
            emailList.Add(email);

            List<RegistrantPhone> phoneList = new List<RegistrantPhone>();
            RegistrantPhone phoneA = new RegistrantPhone() { Id = 2, RegistrantId = 2, CanText = false, PhoneTypeId = 1, CarrierId = 1, Phone = "3015551212" };
            RegistrantPhone phoneB = new RegistrantPhone() { Id = 3, RegistrantId = 2, CanText = true, PhoneTypeId = 1, CarrierId = 1, Phone = "3105551212" };
            RegistrantPhone phoneC = new RegistrantPhone() { Id = 0, RegistrantId = 2, CanText = false, PhoneTypeId = 1, CarrierId = 1, Phone = "4125551212" };
            phoneList.Add(phoneA);
            phoneList.Add(phoneB);
            phoneList.Add(phoneC);

            var registrant = new Registrant()
            {
                Id = 2,
                FirstName = "Bruce",
                LastName = "Wayne",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 1,
                TeamId = 1,
                Selected = false,
                Year = "1999",
                RegistrantPhone = phoneList,
                RegistrantEmail = emailList
            };

            var repository = new TrainingRepository(_context);
            repository.UpdateRegistrant(registrant);
            var newRegistrant = _context.Registrant.FirstOrDefault(r => r.Id == registrantId);
            Assert.Equal(false, newRegistrant.Selected);
            Assert.Equal(3, newRegistrant.RegistrantPhone.Count);
            //Assert.Equal(1, newRegistrant.RegistrantEmail.Count);
        }

    }
}
