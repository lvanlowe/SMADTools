using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Interfaces;
using InformationService.Models;
using InterfaceModels;
using Moq;
using TrainingManagingWorker;
using Xunit;

namespace TrainingManagingWorkerTest
{
    public class ReferenceWorkerTest
    {
        private Mock<IReferenceRepository> _mockReferenceRepository;
        private ReferenceWorker _worker;


        public ReferenceWorkerTest()
        {
            _mockReferenceRepository = new Mock<IReferenceRepository>();
            var sport1 = new Sports()
            {
                Id = 1,
                Name = "BasketBall",
                Email = "superman@dc.com",
            };
            var sport2 = new Sports()
            {
                Id = 2,
                Name = "Soccer",
                Email = "batman@dc.com",
            };
            var sport3 = new Sports()
            {
                Id = 3,
                Name = "Aquatics",
                Email = "flash@dc.com",
            };
            var sport4 = new Sports()
            {
                Id = 4,
                Name = "Track",
                Email = "atom@dc.com",
            };


            var location1 = new Programs()
            {
                Id = 1,
                Name = "Stonewall",
            };
            var location2 = new Programs()
            {
                Id = 2,
                Name = "Hampton"
            };
            var location3 = new Programs()
            {
                Id = 3,
                Name = "Saunders"
            };
            var location4 = new Programs()
            {
                Id = 4,
                Name = "Woodbridge"
            };
            var location5 = new Programs()
            {
                Id = 5,
                Name = "Woodbridge"
            };
            var location6 = new Programs()
            {
                Id = 6,
                Name = "Dale City"
            };
            var location7 = new Programs()
            {
                Id = 7,
                Name = "Gainesville"
            };
            var location8 = new Programs()
            {
                Id = 8,
                Name = "Manassas"
            };
            var location9 = new Programs()
            {
                Id = 9,
                Name = "Colgan"
            };
            var location10 = new Programs()
            {
                Id = 10,
                Name = "Freedom"
            };

            sport1.Programs.Add(location1);
            sport1.Programs.Add(location2);
            sport1.Programs.Add(location3);
            sport1.Programs.Add(location4);
            sport2.Programs.Add(location5);
            sport2.Programs.Add(location6);
            sport2.Programs.Add(location7);
            sport3.Programs.Add(location8);
            sport3.Programs.Add(location9);
            sport4.Programs.Add(location10);

            List<Sports> sports = new List<Sports> { sport1, sport2, sport3, sport4 };

            _mockReferenceRepository.Setup(repository => repository.GetSports()).ReturnsAsync(sports);
            _worker = new ReferenceWorker(_mockReferenceRepository.Object);

        }

        [Fact]
        public void PrepareChampionshipText_When_executed_return_message()

        {
            var teamName = "Bulls";
            var locationName = "Rodney E. Thompson Middle School";
            var arrivalTime = "10:30 AM";
            var expected =
                "Bulls completion will be at Rodney E. Thompson Middle School, everyone should arrive at 10:30 AM ";

            string actual = _worker.PrepareChampionshipText(teamName, locationName, arrivalTime );
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void PrepareSportsDataForClient_When_executed_return_dto()

        {
            var sport = new Sports()
            {
                Id = 1,
                Name = "BasketBall",
                Email = "superman@dc.com",
            };
            var location = new Programs()
            {
                Id = 2,
                Name = "Stonewall",
                SportNavigation = sport
            };
            sport.Programs.Add(location);


            SportLocationDto actual = _worker.PrepareSportsDataForClient(location);
            Assert.Equal(sport.Email, actual.Email);
            Assert.Equal(sport.Name, actual.SportName);
            Assert.Equal(sport.Id, actual.SportId);
            Assert.Equal(location.Id, actual.ProgramId);
            Assert.Equal(location.Name, actual.ProgramName);

        }

    }
}
