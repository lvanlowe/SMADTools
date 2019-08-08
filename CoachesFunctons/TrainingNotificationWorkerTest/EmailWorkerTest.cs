using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using InformationService.DataModels;
using InformationService.Interfaces;
using InformationService.Models;
using InformationService.Repositories;
using InterfaceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NotificationService.Interfaces;
using NotificationService.Repositories;
using TrainingNotificationWorker;
using Xunit;

namespace TrainingNotificationWorkerTest
{


    public class EmailWorkerTest
    {
        private List<List<SportEmails>> _emailList;
        private Mock<ITrainingRepository> _mockTrainingRepository;
        private Mock<IEmailRepository> _mockEmailRepository;
        private EmailWorker _worker;
        public EmailWorkerTest()
        {
            _mockTrainingRepository = new Mock<ITrainingRepository>();
            _mockEmailRepository = new Mock<IEmailRepository>();
            _worker = new EmailWorker(_mockTrainingRepository.Object, _mockEmailRepository.Object);

        }

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
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "superman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "manbat@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "robin@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "flash@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batgirl@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batwoman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "ace@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "batman@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "nightwing@dc.com", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6, IsVolunteer = true });
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
            _mockTrainingRepository.Setup(repository => repository.GetEmailsBySport(sportId)).ReturnsAsync(_emailList[sportId - 1]);
            var actual = _worker.GetEmailsForSport(sportId);
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
            var emails = _emailList[sportId - 1];
            var actual = _worker.GetEmailsForLocation(locationId, emails);
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
            var emails = _emailList[sportId - 1];
            var actual = _worker.GetEmailsForCategory(categoryId, emails);
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
            var emails = _emailList[sportId - 1];
            var actual = _worker.GetEmailsForTeam(teamId, emails);
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
            var emails = _emailList[sportId - 1];
            var actual = _worker.GetEmailsForSelected(isSelected, emails);
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
            var emails = _emailList[sportId - 1];
            var actual = _worker.GetEmailsForVolunteers(isVolunteer, emails);
            Assert.Equal(expected, actual.Count);

        }


        [Theory]
        [InlineData(1, 1)]
        public void RemoveDuplicateEmails_When_executed_create_list_of_Emails_wityh_no_dups(int sportId, int expected)

        {
            LoadEmails();
            var emails = _emailList[sportId - 1];
            var actual = _worker.RemoveDuplicateEmails(emails);
            Assert.Equal(expected, actual.Count);

        }

        [Theory]
        [InlineData(1, 5)]
        public void SendEmails_When_executed_x_emails_sent(int sportId, int expected)

        {
            LoadEmails();
            var emails = _emailList[sportId - 1];
            var emailList = emails.Select(e => e.Email).ToList();
            const string fromEmail = "superman@dc.com";
            const string subject = "Sending with SendGrid is Fun";
            const string plainTextContent = "and easy to do anywhere, even with C#";
            const string htmlContent = "<br>Hi {{deacon}}<br><br>&nbsp;&nbsp;&nbsp;&nbsp;Just a reminder you are the Deacon on Duty for {{month}},<br><br>&nbsp;&nbsp;&nbsp;&nbsp;You will responsible to lock up the church on Sundays after worship. If you are not going to be there then it is your responsibility to get another Deacon to close up for you. You are responsible for taking out the trash. Also make sure the offering baskets are out for the next week.<br><br>&nbsp;&nbsp;&nbsp;&nbsp;If you are going to miss more than one Sunday in {{month}} please change with another deacon";


            _worker.SendEmails(emailList, fromEmail, subject, plainTextContent, htmlContent);
            _mockEmailRepository.Verify(mock => mock.SendEmailString(fromEmail, It.IsAny<string>(), subject, plainTextContent, htmlContent), Times.Exactly(expected));

        }

        [Theory]
        [InlineData(1, null, null, null, null, null, 2)]
        [InlineData(3, null, null, null, null, true, 6)]
        [InlineData(3, null, null, null, true, null, 9)]
        [InlineData(3, 3, null, null, null, null, 4)]
        [InlineData(3, null, 3, null, null, null, 6)]
        [InlineData(3, null, null, 5, null, null, 5)]
        [InlineData(3, null, 3, null, true, null, 5)]
        [InlineData(3, null, 3, null, null, true, 3)]
        public void SendEmailsForSport_When_executed_x_emails_sent(int sportId, int? locationId, int? categoryId, int? teamId, bool? selected, bool? volunteerOnly,  int expected)

        {
            LoadEmails();
            _mockTrainingRepository.Setup(repository => repository.GetEmailsBySport(sportId)).ReturnsAsync(_emailList[sportId - 1]);

            const string fromEmail = "superman@dc.com";
            const string subject = "Sending with SendGrid is Fun";
            const string plainTextContent = "and easy to do anywhere, even with C#";
            const string htmlContent = "<br>Hi {{deacon}}<br><br>&nbsp;&nbsp;&nbsp;&nbsp;Just a reminder you are the Deacon on Duty for {{month}},<br><br>&nbsp;&nbsp;&nbsp;&nbsp;You will responsible to lock up the church on Sundays after worship. If you are not going to be there then it is your responsibility to get another Deacon to close up for you. You are responsible for taking out the trash. Also make sure the offering baskets are out for the next week.<br><br>&nbsp;&nbsp;&nbsp;&nbsp;If you are going to miss more than one Sunday in {{month}} please change with another deacon";
            CoachEmailDto message = new CoachEmailDto { SportId = sportId,
                                                        From = fromEmail,
                                                        HtmlContent = htmlContent,
                                                        PlainTextContent = plainTextContent,
                                                        Subject = subject,
                                                        IsVolunteer = volunteerOnly,
                                                        Selected = selected,
                                                        ProgramId = locationId,
                                                        SportTypeId = categoryId,
                                                        TeamId = teamId
            };

            _worker.SendEmailsForSport(message);
            _mockEmailRepository.Verify(mock => mock.SendEmailString(fromEmail, It.IsAny<string>(), subject, plainTextContent, htmlContent), Times.Exactly(expected));

        }

        [Theory]
        [InlineData(6, null, null, null, null, null, 1)]
        public void SendEmailsForSport_WithoutMock(int sportId, int? locationId, int? categoryId, int? teamId, bool? selected, bool? volunteerOnly, int expected)

        {

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var connectionString = config["TrainingDatabase"];
            var options = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(connectionString).Options;

            PwsodbContext context = new PwsodbContext(options);

            var fromEmail = config["FromEmail"]; 
            const string subject = "Sending with SendGrid is Fun";
            const string plainTextContent = "and easy to do anywhere, even with C#";
            const string htmlContent = "<br>Hi {{deacon}}<br><br>&nbsp;&nbsp;&nbsp;&nbsp;Just a reminder you are the Deacon on Duty for {{month}},<br><br>&nbsp;&nbsp;&nbsp;&nbsp;You will responsible to lock up the church on Sundays after worship. If you are not going to be there then it is your responsibility to get another Deacon to close up for you. You are responsible for taking out the trash. Also make sure the offering baskets are out for the next week.<br><br>&nbsp;&nbsp;&nbsp;&nbsp;If you are going to miss more than one Sunday in {{month}} please change with another deacon";
            CoachEmailDto message = new CoachEmailDto
            {
                SportId = sportId,
                From = fromEmail,
                HtmlContent = htmlContent,
                PlainTextContent = plainTextContent,
                Subject = subject,
                IsVolunteer = volunteerOnly,
                Selected = selected,
                ProgramId = locationId,
                SportTypeId = categoryId,
                TeamId = teamId
            };
            ITrainingRepository trainingRepository = new TrainingRepository(context);
            var apiKey = config["ApiKey"];
            IEmailRepository emailRepository = new EmailRepository(apiKey);
            EmailWorker worker = new EmailWorker(trainingRepository, emailRepository);


            //worker.SendEmailsForSport(message);

        }
    }
}
