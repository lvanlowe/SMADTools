using System;
using System.Collections.Generic;
using System.Linq;
using InformationService.Interfaces;
using InformationService.Models;
using InformationService.Repositories;
using InterfaceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using TrainingManagingWorker;
using Xunit;

namespace TrainingManagingWorkerTest
{
    public class RegistrantWorkerTest
    {
        private Mock<ITrainingRepository> _mockTrainingRepository;
        private Mock<IOrganizationRepository> _mockOrganizationRepository;
        private RegistrantWorker _worker;
        private Athletes _athlete1;
        private Athletes _athlete2;

        public RegistrantWorkerTest()
        {
            _mockTrainingRepository = new Mock<ITrainingRepository>();
            _mockOrganizationRepository = new Mock<IOrganizationRepository>();
            _athlete1 = new Athletes { Id = 67, BirthDate = new DateTime(1985, 1, 17), MF = "M", MedicalExpirationDate = new DateTime(2 / 22 / 2020) };
            _athlete2 = new Athletes { Id = 219, BirthDate = new DateTime(1970, 10, 29), MF = "M", MedicalExpirationDate = new DateTime(8 / 5 / 2018) };
            List<Athletes> athletes = new List<Athletes> { _athlete1, _athlete2 };

            _mockOrganizationRepository.Setup(repository => repository.GetAllAthletes()).ReturnsAsync(athletes);

            _worker = new RegistrantWorker(_mockTrainingRepository.Object, _mockOrganizationRepository.Object);
        }


        [Theory]
        [InlineData(1, 1)]
        public void PerpareRegistrantDataForClient_When_executed_return_dto(int sportId, int expected)

        {
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2
            };
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(registrant.FirstName, actual.FirstName);
            Assert.Equal(registrant.LastName, actual.LastName);
            Assert.Equal(registrant.NickName, actual.NickName);
            Assert.Equal(registrant.Id, actual.Id);
            Assert.Equal(registrant.ProgramId, actual.ProgramId);
            Assert.Equal(registrant.SportId, actual.SportId);
            Assert.Equal(registrant.Size, actual.Size);
            Assert.Equal(registrant.SportTypeId, actual.SportTypeId);

        }

        [Theory]
        [InlineData(1, 1)]
        public void PerpareRegistrantDataForClient_With_Optional_When_executed_return_dto(int sportId, int expected)

        {
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2,
                IsVolunteer = true,
                Selected = true,
                TeamId = 8
            };
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(registrant.IsVolunteer, actual.IsVolunteer);
            Assert.Equal(registrant.Selected, actual.Selected);
            Assert.Equal(registrant.TeamId, actual.TeamId);

        }

        [Theory]
        [InlineData(1, 1)]
        public void PerpareRegistrantDataForClient_With_Athlete_When_executed_return_dto(int sportId, int expected)

        {

            RegisteredAthlete athlete = new RegisteredAthlete{AthletesId = 219, Id = 67};
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2,
                IsVolunteer = true,
                Selected = true,
                TeamId = 8,
            };
            registrant.RegisteredAthlete.Add(athlete);
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(athlete.Id, actual.RegisteredAthletesId);
            Assert.Equal(athlete.AthletesId, actual.AthletesId);
            Assert.Equal(_athlete1.BirthDate, actual.BirthDate);
            Assert.Equal(_athlete1.MedicalExpirationDate, actual.MedicalExpirationDate);
            Assert.Equal(_athlete1.MF, actual.Gender);

        }

        [Theory]
        [InlineData(1, 1)]
        public void PrepareRegistrantDataForClient_With_1_Phone_When_executed_return_dto(int sportId, int expected)

        {
            RegisteredAthlete athlete = new RegisteredAthlete { AthletesId = 219, Id = 67 };
            RegistrantPhone phone1 = new RegistrantPhone { Phone = "703-555-1212", CanText = true, PhoneType = "cell", Id = 15};
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2,
                IsVolunteer = true,
                Selected = true,
                TeamId = 8,
            };
            registrant.RegisteredAthlete.Add(athlete);
            registrant.RegistrantPhone.Add(phone1);
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(phone1.Id, actual.RegistrantPhone1Id);
            Assert.Equal(phone1.CanText, actual.CanText1);
            Assert.Equal(phone1.Phone, actual.Phone1);
            Assert.Equal(phone1.PhoneType, actual.PhoneType1);

        }

        [Theory]
        [InlineData(1, 1)]
        public void PrepareRegistrantDataForClient_With_2_Phone_When_executed_return_dto(int sportId, int expected)

        {
            RegisteredAthlete athlete = new RegisteredAthlete { AthletesId = 219, Id = 67 };
            RegistrantPhone phone1 = new RegistrantPhone { Phone = "703-555-1212", CanText = true, PhoneType = "cell", Id = 15 };
            RegistrantPhone phone2 = new RegistrantPhone { Phone = "202-555-1212", CanText = false, PhoneType = "home", Id = 16 };
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2,
                IsVolunteer = true,
                Selected = true,
                TeamId = 8,
            };
            registrant.RegisteredAthlete.Add(athlete);
            registrant.RegistrantPhone.Add(phone1);
            registrant.RegistrantPhone.Add(phone2);
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(phone1.Id, actual.RegistrantPhone1Id);
            Assert.Equal(phone1.CanText, actual.CanText1);
            Assert.Equal(phone1.Phone, actual.Phone1);
            Assert.Equal(phone1.PhoneType, actual.PhoneType1);
            Assert.Equal(phone2.Id, actual.RegistrantPhone2Id);
            Assert.Equal(phone2.CanText, actual.CanText2);
            Assert.Equal(phone2.Phone, actual.Phone2);
            Assert.Equal(phone2.PhoneType, actual.PhoneType2);

        }

        [Theory]
        [InlineData(1, 1)]
        public void PrepareRegistrantDataForClient_With_3_Phone_When_executed_return_dto(int sportId, int expected)

        {
            RegisteredAthlete athlete = new RegisteredAthlete { AthletesId = 219, Id = 67 };
            RegistrantPhone phone1 = new RegistrantPhone { Phone = "703-555-1212", CanText = true, PhoneType = "cell", Id = 15 };
            RegistrantPhone phone2 = new RegistrantPhone { Phone = "202-555-1212", CanText = false, PhoneType = "home", Id = 16 };
            RegistrantPhone phone3 = new RegistrantPhone { Phone = "301-555-1212", CanText = false, PhoneType = "other", Id = 34 };
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2,
                IsVolunteer = true,
                Selected = true,
                TeamId = 8,
            };
            registrant.RegisteredAthlete.Add(athlete);
            registrant.RegistrantPhone.Add(phone1);
            registrant.RegistrantPhone.Add(phone2);
            registrant.RegistrantPhone.Add(phone3);
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(phone1.Id, actual.RegistrantPhone1Id);
            Assert.Equal(phone1.CanText, actual.CanText1);
            Assert.Equal(phone1.Phone, actual.Phone1);
            Assert.Equal(phone1.PhoneType, actual.PhoneType1);
            Assert.Equal(phone2.Id, actual.RegistrantPhone2Id);
            Assert.Equal(phone2.CanText, actual.CanText2);
            Assert.Equal(phone2.Phone, actual.Phone2);
            Assert.Equal(phone2.PhoneType, actual.PhoneType2);
            Assert.Equal(phone3.Id, actual.RegistrantPhone3Id);
            Assert.Equal(phone3.CanText, actual.CanText3);
            Assert.Equal(phone3.Phone, actual.Phone3);
            Assert.Equal(phone3.PhoneType, actual.PhoneType3);

        }
        [Theory]
        [InlineData(1, 1)]
        public void PrepareRegistrantDataForClient_With_1_Email_When_executed_return_dto(int sportId, int expected)

        {
            RegisteredAthlete athlete = new RegisteredAthlete { AthletesId = 219, Id = 67 };
            RegistrantEmail email1 = new RegistrantEmail() { Email = "batman@dc.com", Id = 15 };
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2,
                IsVolunteer = true,
                Selected = true,
                TeamId = 8,
            };
            registrant.RegisteredAthlete.Add(athlete);
            registrant.RegistrantEmail.Add(email1);
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(email1.Id, actual.RegistrantEmail1Id);
            Assert.Equal(email1.Email, actual.Email1);

        }

        [Theory]
        [InlineData(1, 1)]
        public void PrepareRegistrantDataForClient_With_2_Email_When_executed_return_dto(int sportId, int expected)

        {
            RegisteredAthlete athlete = new RegisteredAthlete { AthletesId = 219, Id = 67 };
            RegistrantEmail email1 = new RegistrantEmail() { Email = "batman@dc.com", Id = 15 };
            RegistrantEmail email2 = new RegistrantEmail() { Email = "superman@dc.com", Id = 14 };
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2,
                IsVolunteer = true,
                Selected = true,
                TeamId = 8,
            };
            registrant.RegisteredAthlete.Add(athlete);
            registrant.RegistrantEmail.Add(email1);
            registrant.RegistrantEmail.Add(email2);
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(email1.Id, actual.RegistrantEmail1Id);
            Assert.Equal(email1.Email, actual.Email1);
            Assert.Equal(email2.Id, actual.RegistrantEmail2Id);
            Assert.Equal(email2.Email, actual.Email2);

        }


        [Theory]
        [InlineData(1, 1)]
        public void PrepareRegistrantDataForClient_With_3_Email_When_executed_return_dto(int sportId, int expected)

        {
            RegisteredAthlete athlete = new RegisteredAthlete { AthletesId = 219, Id = 67 };
            RegistrantEmail email1 = new RegistrantEmail() { Email = "batman@dc.com", Id = 15 };
            RegistrantEmail email2 = new RegistrantEmail() { Email = "superman@dc.com", Id = 14 };
            RegistrantEmail email3 = new RegistrantEmail() { Email = "flash@dc.com", Id = 28};
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2,
                IsVolunteer = true,
                Selected = true,
                TeamId = 8,
            };
            registrant.RegisteredAthlete.Add(athlete);
            registrant.RegistrantEmail.Add(email1);
            registrant.RegistrantEmail.Add(email2);
            registrant.RegistrantEmail.Add(email3);
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(email1.Id, actual.RegistrantEmail1Id);
            Assert.Equal(email1.Email, actual.Email1);
            Assert.Equal(email2.Id, actual.RegistrantEmail2Id);
            Assert.Equal(email2.Email, actual.Email2);
            Assert.Equal(email3.Id, actual.RegistrantEmail3Id);
            Assert.Equal(email3.Email, actual.Email3);

        }

        [Theory]
        [InlineData(2, 4)]
        public void GetRegistrantsForSport_WithoutMock(int sportId, int expected)

        {

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var trainingConnectionString = config["TrainingDatabase"];
            var organizationConnectionString = config["OrganizationDatabase"];
            var trainingOptions = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(trainingConnectionString).Options;
            var organizationOptions = new DbContextOptionsBuilder<PwsoContext>().UseSqlServer(organizationConnectionString).Options;

            PwsodbContext trainingContext = new PwsodbContext(trainingOptions);
            PwsoContext organizationContext = new PwsoContext(organizationOptions);
            ITrainingRepository trainingRepository = new TrainingRepository(trainingContext);
            IOrganizationRepository organizationRepository = new OrganizationRepository(organizationContext);
            RegistrantWorker worker = new RegistrantWorker(trainingRepository, organizationRepository);


            //var actual = worker.GetRegistrantsForSport(sportId);
            //Assert.Equal(expected, actual.Result.Count);

        }

        [Theory]
        [InlineData(1)]
        public void AddNumberForEvent_With_event_found_return_message(int eventId)

        {
            List<NotificationEntity> notificationEntities = new List<NotificationEntity>();
            notificationEntities.Add(new NotificationEntity { RowKey = "track", ProgramId = 1, SportId = 1, Message = "this track event" });
            notificationEntities.Add(new NotificationEntity { RowKey = "polar", ProgramId = 2, SportId = 2, Message = "this polar plunge event" });
            Mock<IRefRepository> mockRefRepository = new Mock<IRefRepository>();
            mockRefRepository.Setup(repository => repository.GetEventByName(notificationEntities[0].RowKey)).ReturnsAsync(notificationEntities[0]);
            mockRefRepository.Setup(repository => repository.GetEventByName(notificationEntities[1].RowKey)).ReturnsAsync(notificationEntities[1]);
            RegistrantWorker worker = new RegistrantWorker(_mockTrainingRepository.Object, mockRefRepository.Object);
            EventTextDto dto = new EventTextDto{Zip = "22193", City = "Dale City", From = "17035551212", Message = notificationEntities[eventId].RowKey};

            var actual = worker.AddNumberForEvent(dto);
            Assert.Equal(notificationEntities[eventId].Message, actual.Result.Message);
            //Assert.Equal(email1.Email, actual.Email1);
            //Assert.Equal(email2.Id, actual.RegistrantEmail2Id);
            //Assert.Equal(email2.Email, actual.Email2);
            //Assert.Equal(email3.Id, actual.RegistrantEmail3Id);
            //Assert.Equal(email3.Email, actual.Email3);

        }

    }
}
