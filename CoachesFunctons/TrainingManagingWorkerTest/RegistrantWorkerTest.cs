using System;
using System.Collections.Generic;
using InformationService.Interfaces;
using InformationService.Models;
using InterfaceModels;
using Moq;
using TrainingManagingWorker;
using Xunit;

namespace TrainingManagingWorkerTest
{
    public class RegistrantWorkerTest
    {
        private Mock<ITrainingRepository> _mockTrainingRepository;
        private RegistrantWorker _worker;

        public RegistrantWorkerTest()
        {
            _mockTrainingRepository = new Mock<ITrainingRepository>();
            _worker = new RegistrantWorker(_mockTrainingRepository.Object);
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

    }
}
