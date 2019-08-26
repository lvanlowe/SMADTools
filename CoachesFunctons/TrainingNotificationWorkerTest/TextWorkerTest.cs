using InformationService.DataModels;
using InformationService.Interfaces;
using Moq;
using NotificationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InformationService.Models;
using InformationService.Repositories;
using InterfaceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotificationService.Repositories;
using TrainingNotificationWorker;
using Xunit;

namespace TrainingNotificationWorkerTest
{
    public class TextWorkerTest
    {

        private List<List<SportEmails>> _phoneList;
        private Mock<ITrainingRepository> _mockTrainingRepository;
        private Mock<ISmsRepository> _mockSmsRepository;
        private TextWorker _worker;


        public TextWorkerTest()
        {
            _mockTrainingRepository = new Mock<ITrainingRepository>();
            _mockSmsRepository = new Mock<ISmsRepository>();
            _worker = new TextWorker(_mockTrainingRepository.Object, _mockSmsRepository.Object);

        }

        private void LoadPhones()
        {
            _phoneList = new List<List<SportEmails>>();
            List<SportEmails> emails1 = new List<SportEmails>();
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", IsVolunteer = true });
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212___", LastName = "Wayne", NickName = "Batman", Selected = true });
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "(703) 555-1212", LastName = "Wayne", NickName = "Batman" });
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "703-555-1212", LastName = "Wayne", NickName = "Batman" });
            emails1.Add(new SportEmails { FirstName = "Bruce", Email = "703_555_1212", LastName = "Wayne", NickName = "Batman" });
            _phoneList.Add(emails1);


            List<SportEmails> emails2 = new List<SportEmails>();
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 1, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 1, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 2, SportTypeId = 2, TeamId = 2 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 1, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 1 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 2, TeamId = 3, Selected = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4, IsVolunteer = true });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            emails2.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, TeamId = 4 });
            _phoneList.Add(emails2);


            List<SportEmails> emails3 = new List<SportEmails>();
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "3015551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "2025551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 3, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "2125551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "5715551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 3 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7185551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "2015551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "3105551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 5, Selected = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "2135551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6, IsVolunteer = true });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            emails3.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 4, SportTypeId = 4, TeamId = 6 });
            _phoneList.Add(emails3);


            List<SportEmails> emails4 = new List<SportEmails>();
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 5 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 5 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 5 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, IsVolunteer = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, IsVolunteer = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 6, TeamId = 7, Selected = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8, IsVolunteer = true });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            emails4.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 5, SportTypeId = 7, TeamId = 8 });
            _phoneList.Add(emails4);


            List<SportEmails> emails5 = new List<SportEmails>();
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 6, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 7, SportTypeId = 8, TeamId = 9 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 10, Selected = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11, IsVolunteer = true });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            emails5.Add(new SportEmails { FirstName = "Bruce", Email = "7035551212", LastName = "Wayne", NickName = "Batman", ProgramId = 8, SportTypeId = 8, TeamId = 11 });
            _phoneList.Add(emails5);

        }


        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 39)]
        [InlineData(3, 22)]
        [InlineData(4, 16)]
        [InlineData(5, 39)]
        public void GetPhonesForSport_When_executed_create_list_of_SportEmails(int sportId, int expected)

        {
            LoadPhones();
            _mockTrainingRepository.Setup(repository => repository.GetPhonesBySport(sportId)).ReturnsAsync(_phoneList[sportId - 1]);
            var actual = _worker.GetPhonesForSport(sportId);
            Assert.Equal(expected, actual.Result.Count);

        }

        [Theory]
        [InlineData(1, 1)]
        public void RemoveDuplicatePhones_When_executed_create_list_of_Phones_with_no_dups(int sportId, int expected)

        {
            LoadPhones();
            var phone = _phoneList[sportId - 1];
            List<string> actual = _worker.RemoveDuplicatePhones(phone);
            Assert.Equal(expected, actual.Count);

        }

        [Theory]
        [InlineData(1, 5)]
        public void SendSms_When_executed_x_emails_sent(int sportId, int expected)

        {
            LoadPhones();
            var phone = _phoneList[sportId - 1];
            var phoneList = phone.Select(e => e.Email).ToList();
            const string message = "text test with C#";


            _worker.SendSmsAsync(phoneList, message);
            _mockSmsRepository.Verify(mock => mock.SendSms(It.IsAny<string>(), message), Times.Exactly(expected));

        }

        [Theory]
        [InlineData(1, null, null, null, null, null, 1)]
        [InlineData(3, null, null, null, null, true, 5)]
        [InlineData(3, null, null, null, true, null, 8)]
        [InlineData(3, 3, null, null, null, null, 3)]
        [InlineData(3, null, 3, null, null, null, 5)]
        [InlineData(3, null, null, 5, null, null, 4)]
        [InlineData(3, null, 3, null, true, null, 4)]
        [InlineData(3, null, 3, null, null, true, 2)]
        public void SendSmsForSport_When_executed_x_text_sent(int sportId, int? locationId, int? categoryId, int? teamId, bool? selected, bool? volunteerOnly, int expected)

        {
            LoadPhones();
            _mockTrainingRepository.Setup(repository => repository.GetPhonesBySport(sportId)).ReturnsAsync(_phoneList[sportId - 1]);

            const string text = "and easy to do anywhere, even with C#";
            CoachTextDto message = new CoachTextDto
            {
                SportId = sportId,
                Message = text,
                IsVolunteer = volunteerOnly,
                Selected = selected,
                ProgramId = locationId,
                SportTypeId = categoryId,
                TeamId = teamId
            };

            var actual = _worker.SendSmsForSport(message);
            _mockSmsRepository.Verify(mock => mock.SendSms(It.IsAny<string>(),  text), Times.Exactly(expected));
            Assert.Equal(expected, actual.Result);

        }

        [Theory]
        [InlineData(6, null, null, null, null, null, 1)]
        public void SendEmailsForSport_WithoutMock(int sportId, int? locationId, int? categoryId, int? teamId, bool? selected, bool? volunteerOnly, int expected)

        {

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var connectionString = config["TrainingDatabase"];
            var options = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(connectionString).Options;

            PwsodbContext context = new PwsodbContext(options);

            const string plainTextContent = "testing PWSO sending text messages";
            CoachTextDto message = new CoachTextDto
            {
                SportId = sportId,
                Message = plainTextContent,
                IsVolunteer = volunteerOnly,
                Selected = selected,
                ProgramId = locationId,
                SportTypeId = categoryId,
                TeamId = teamId
            };
            ITrainingRepository trainingRepository = new TrainingRepository(context);
            var accountSid = config["AccountSid"];
            var authToken = config["AuthToken"];
            var fromPhone = config["FromPhone"];
            ISmsRepository emailRepository = new SmsRepository(accountSid,authToken, fromPhone);
            TextWorker worker = new TextWorker(trainingRepository, emailRepository);


            //var actual = worker.SendSmsForSport(message);
            //Assert.Equal(expected, actual.Result);

        }

    }
}
