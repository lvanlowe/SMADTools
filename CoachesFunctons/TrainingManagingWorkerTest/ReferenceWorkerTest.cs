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
        public void PrepareChampionshipEmail_When_executed_return_message()

        {
            ReferenceWorker.TournamentDetails details = new ReferenceWorker.TournamentDetails
            {
                TeamName = "Bulls",
                FirstGameTime = "9:00 AM",
                SecondGameTime = "1:00 PM",
                StartTime = "8:30 AM",
                LocationName = "H. H. Poole Middle School",
                LocationAddress = "800 Eustace Road",
                LocationCity = "Stafford",
                LocationState = "VA",
                LocationZip = "22554"
            };


            var expected =
                "<p>Hi Bulls Athletes, Athletes family, Coaches and Volunteers:<br /></p><p style=\"margin-left:30px;\"><strong>Everyone should be there at 8:30 AM.</strong><br /><br />The first game is at 9:00 AM.<br /><br />The second game is at 1:00 PM.<br /><br /><br />The completion will be at  H. H. Poole Middle School (800 Eustace Road, Stafford, VA 22554 )<br /><br /><br /><p style=\"margin-left:30px;\">You must return your uniform to your coach RIGHT AFTER SATURDAY’S COMPETITION. Remember bring a change of clothes. Failure to return your uniform may reflect on your athlete being selected for future Basketball Championships. There will also be a charge of $30 for jersey and $20 for shorts for all uniforms not returned at that time.<br /><br /><em>Remember no food or drinks are allowed in the Gymnasiums, only water.</em><br /><br /><br />Let me know if you have any questions.<br /><br /><br /></p><p>Van&nbsp;<br /><br /><br />L. A. Van Lowe&nbsp;<br /><br />Basketball Coordinator&nbsp;<br /><br /><br />P. O. 1073&nbsp;<br /><br />Woodbridge, VA 22195-1073&nbsp;<br /><br />Fax: (866) 558-8780&nbsp;<br /></p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            string actual = _worker.PrepareChampionshipEmail(details);
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void PrepareChampionshipDetails_When_executed_return_message()
        {
            TournamentTeamDto dto = new TournamentTeamDto
            {
                TeamId = 25,
                Game1TimeId = 7,
                Game2TimeId = 15,
                LocationId = 50
            };

            Teams team = new Teams{Id = 25, Name = "Bulls" };
            CalendarTimes startTime = new CalendarTimes { Id = 6, TimeHour = "8:30 AM" };
            CalendarTimes firstTime = new CalendarTimes { Id = 7, TimeHour = "9:00 AM" };
            CalendarTimes secondTime = new CalendarTimes { Id = 15, TimeHour = "1:00 PM" };
            Location location = new Location
            {
                id = 50,
                Name = "H. H. Poole Middle School",
                 Street = "800 Eustace Road",
                 City = "Stafford",
                 State = "VA",
                 Zip = "22554"
            };

            ReferenceWorker.TournamentDetails expected = new ReferenceWorker.TournamentDetails
            {
                TeamName = "Bulls",
                FirstGameTime = "9:00 AM",
                SecondGameTime = "1:00 PM",
                StartTime = "8:30 AM",
                LocationName = "H. H. Poole Middle School",
                LocationAddress = "800 Eustace Road",
                LocationCity = "Stafford",
                LocationState = "VA",
                LocationZip = "22554"
            };

            _mockReferenceRepository.Setup(repository => repository.GetTeamByTeamId(25)).ReturnsAsync(team);
            _mockReferenceRepository.Setup(repository => repository.GetTimeByTimeId(6)).ReturnsAsync(startTime);
            _mockReferenceRepository.Setup(repository => repository.GetTimeByTimeId(7)).ReturnsAsync(firstTime);
            _mockReferenceRepository.Setup(repository => repository.GetTimeByTimeId(15)).ReturnsAsync(secondTime);
            _mockReferenceRepository.Setup(repository => repository.GetLocationByLocationId(50)).ReturnsAsync(location);


            //var expected =
            //    "<p>Hi Bulls Athletes, Athletes family, Coaches and Volunteers:<br /></p><p style=\"margin-left:30px;\"><strong>Everyone should be there at 8:30 AM.</strong><br /><br />The first game is at 9:00 AM.<br /><br />The second game is at 1:00 PM.<br /><br /><br />The completion will be at  H. H. Poole Middle School (800 Eustace Road, Stafford, VA 22554 )<br /><br /><br /><p style=\"margin-left:30px;\">You must return your uniform to your coach RIGHT AFTER SATURDAY’S COMPETITION. Remember bring a change of clothes. Failure to return your uniform may reflect on your athlete being selected for future Basketball Championships. There will also be a charge of $30 for jersey and $20 for shorts for all uniforms not returned at that time.<br /><br /><em>Remember no food or drinks are allowed in the Gymnasiums, only water.</em><br /><br /><br />Let me know if you have any questions.<br /><br /><br /></p><p>Van&nbsp;<br /><br /><br />L. A. Van Lowe&nbsp;<br /><br />Basketball Coordinator&nbsp;<br /><br /><br />P. O. 1073&nbsp;<br /><br />Woodbridge, VA 22195-1073&nbsp;<br /><br />Fax: (866) 558-8780&nbsp;<br /></p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            ReferenceWorker.TournamentDetails actual = _worker.PrepareChampionshipDetails(dto);
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
