using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using InformationService.DataModels;
using InformationService.Interfaces;
using Moq;
using NotificationService.Interfaces;
using TrainingNotificationWorker;
using Xunit;

namespace TrainingNotificationWorkerTest
{


    public class EmailWorkerTest
    {
        private List<List<SportEmails>> _emailList;


        private void LoadEmails()
        {
            _emailList = new List<List<SportEmails>>();
            List<SportEmails> emails1 = new List<SportEmails>();
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "Batman@dc.com", LastName = "Wayne", NickName = "Batman", IsVolunteer = true});
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "batMan@dc.com", LastName = "Wayne", NickName = "Batman", Selected = true});
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "BatMan@dc.com", LastName = "Wayne", NickName = "Batman" });
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "BATMAN@dc.com", LastName = "Wayne", NickName = "Batman" });
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "batman@DC.com", LastName = "Wayne", NickName = "Batman"});
            _emailList.Add(emails1);


            List<SportEmails> emails2 = new List<SportEmails>();
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1});
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1});
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1});
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 1  });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 1});
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2});
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2});
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 1});
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            _emailList.Add(emails2);


            List<SportEmails> emails3 = new List<SportEmails>();
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            _emailList.Add(emails3);


            List<SportEmails> emails4 = new List<SportEmails>();
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 5 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 5 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 5 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, IsVolunteer = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, IsVolunteer = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8, IsVolunteer = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            _emailList.Add(emails4);


            List<SportEmails> emails5 = new List<SportEmails>();
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            _emailList.Add(emails5);

        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 39)]
        [InlineData(3, 22)]
        [InlineData(4, 16)]
        [InlineData(5, 39)]
        public void GetEmailsForSport_When_executed_create_list_of_SportEmails(int sportId, int expected)

        {
            LoadEmails();
            var repositoryMock = new Mock<ITrainingRepository>();
            var emails = _emailList[sportId - 1];
            repositoryMock.Setup(repository => repository.GetEmailsBySport(sportId)).ReturnsAsync(_emailList[sportId - 1]);
            var worker = new EmailWorker(repositoryMock.Object);
            var actual = worker.GetEmailsForSport(sportId);
            Assert.Equal(expected, actual.Result.Count);

        }

        [Theory]
        [InlineData(null, 1, 5)]
        [InlineData(1, 2, 5)]
        [InlineData(2, 3, 0)]
        [InlineData(0, 4, 16)]
        [InlineData(8, 5, 17)]
        public void GetEmailsForLocation_When_executed_create_list_of_SportEmails_for_location(int? locationId, int sportId, int expected)

        {
            LoadEmails();
            var repositoryMock = new Mock<ITrainingRepository>();
            var emails = _emailList[sportId - 1];
            var worker = new EmailWorker(repositoryMock.Object);
            var actual = worker.GetEmailsForLocation(locationId, emails);
            Assert.Equal(expected, actual.Count);

        }


        [Theory]
        [InlineData(null, 1, 5)]
        [InlineData(1, 2, 13)]
        [InlineData(2, 3, 0)]
        [InlineData(0, 4, 16)]
        [InlineData(8, 5, 39)]
        public void GetEmailsForCategory_When_executed_create_list_of_SportEmails_for_caregoryn(int? categoryId, int sportId, int expected)

        {
            LoadEmails();
            var repositoryMock = new Mock<ITrainingRepository>();
            var emails = _emailList[sportId - 1];
            var worker = new EmailWorker(repositoryMock.Object);
            var actual = worker.GetEmailsForCategory(categoryId, emails);
            Assert.Equal(expected, actual.Count);

        }

        [Theory]
        [InlineData(null, 1, 5)]
        [InlineData(1, 2, 7)]
        [InlineData(2, 3, 0)]
        [InlineData(0, 4, 16)]
        [InlineData(11, 5, 6)]
        public void GetEmailsForTeam_When_executed_create_list_of_SportEmails_for_team(int? teamId, int sportId, int expected)

        {
            LoadEmails();
            var repositoryMock = new Mock<ITrainingRepository>();
            var emails = _emailList[sportId - 1];
            var worker = new EmailWorker(repositoryMock.Object);
            var actual = worker.GetEmailsForTeam(teamId, emails);
            Assert.Equal(expected, actual.Count);

        }

        [Theory]
        [InlineData(null, 1, 5)]
        [InlineData(false, 2, 39)]
        [InlineData(true, 3, 12)]
        [InlineData(false, 4, 16)]
        [InlineData(true, 5, 21)]
        public void GetEmailsForSelected_When_executed_create_list_of_SportEmails_for_selected(bool? isSelected, int sportId, int expected)

        {
            LoadEmails();
            var repositoryMock = new Mock<ITrainingRepository>();
            var emails = _emailList[sportId - 1];
            var worker = new EmailWorker(repositoryMock.Object);
            var actual = worker.GetEmailsForSelected(isSelected, emails);
            Assert.Equal(expected, actual.Count);

        }

        [Theory]
        [InlineData(null, 1, 5)]
        [InlineData(false, 2, 39)]
        [InlineData(true, 3, 5)]
        [InlineData(false, 4, 16)]
        [InlineData(true, 5, 9)]
        public void GetEmailsForVolunteers_When_executed_create_list_of_SportEmails_for_volunteers(bool? isVolunteer, int sportId, int expected)

        {
            LoadEmails();
            var repositoryMock = new Mock<ITrainingRepository>();
            var emails = _emailList[sportId - 1];
            var worker = new EmailWorker(repositoryMock.Object);
            List<SportEmails> actual = worker.GetEmailsForVolunteers(isVolunteer, emails);
            Assert.Equal(expected, actual.Count);

        }


        [Theory]
        [InlineData(1, 1)]
        //[InlineData(false, 2, 39)]
        //[InlineData(true, 3, 5)]
        //[InlineData(false, 4, 16)]
        //[InlineData(true, 5, 9)]
        public void RemoveDuplicateEmails_When_executed_create_list_of_Emails_wityh_no_dups(int sportId, int expected)

        {
            LoadEmails();
            var repositoryMock = new Mock<ITrainingRepository>();
            var emails = _emailList[sportId - 1];
            var worker = new EmailWorker(repositoryMock.Object);
            var actual = worker.RemoveDuplicateEmails(emails);
            Assert.Equal(expected, actual.Count);

        }

        [Theory]
        [InlineData(1, 5)]
        //[InlineData(false, 2, 39)]
        //[InlineData(true, 3, 5)]
        //[InlineData(false, 4, 16)]
        //[InlineData(true, 5, 9)]
        public void SendEmails_When_executed_x_emails_sent(int sportId, int expected)

        {
            LoadEmails();
            var repositoryMock = new Mock<ITrainingRepository>();
            var emailRepositoryMock = new Mock<IEmailRepository>();
            var emails = _emailList[sportId - 1];
            var worker = new EmailWorker(repositoryMock.Object);
            var emailList = emails.Select(e => e.Email.ToList());

            worker.RemoveDuplicateEmails(emails);
            emailRepositoryMock.Verify(mock => mock.SendEmailString(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(expected));

        }
    }
}