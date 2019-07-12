using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using NotificationService.Interfaces;
using NotificationService.Repositories;
using SendGrid.Helpers.Mail;
using Xunit;

namespace NotificationServiceTest.RepositoriesTest
{
    public class EmailRepositoryTest
    {
        private IEmailRepository _repository;
        private string _toEmail;
        private string _fromEmail;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        public EmailRepositoryTest()
        {
            var config = InitConfiguration();
            var apiKey = config["ApiKey"];
            _toEmail = config["ToEmail"];
            _fromEmail = config["FromEmail"];

            _repository = new EmailRepository(apiKey);

        }

        [Fact]
        public void TestSendEmail()
        {
            var from = new EmailAddress(_fromEmail);
            const string subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(_toEmail);
            const string plainTextContent = "and easy to do anywhere, even with C#";
            const string htmlContent = "<br>Hi {{deacon}}<br><br>&nbsp;&nbsp;&nbsp;&nbsp;Just a reminder you are the Deacon on Duty for {{month}},<br><br>&nbsp;&nbsp;&nbsp;&nbsp;You will responsible to lock up the church on Sundays after worship. If you are not going to be there then it is your responsibility to get another Deacon to close up for you. You are responsible for taking out the trash. Also make sure the offering baskets are out for the next week.<br><br>&nbsp;&nbsp;&nbsp;&nbsp;If you are going to miss more than one Sunday in {{month}} please change with another deacon";

            _repository.SendEmail(from,to,null, subject,plainTextContent,htmlContent);
        }

        [Fact]
        public void SendEmailString()
        {
            var from = new EmailAddress(_fromEmail);
            const string subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(_toEmail);
            const string plainTextContent = "and easy to do anywhere, even with C#";
            const string htmlContent = "<br>Hi {{deacon}}<br><br>&nbsp;&nbsp;&nbsp;&nbsp;Just a reminder you are the Deacon on Duty for {{month}},<br><br>&nbsp;&nbsp;&nbsp;&nbsp;You will responsible to lock up the church on Sundays after worship. If you are not going to be there then it is your responsibility to get another Deacon to close up for you. You are responsible for taking out the trash. Also make sure the offering baskets are out for the next week.<br><br>&nbsp;&nbsp;&nbsp;&nbsp;If you are going to miss more than one Sunday in {{month}} please change with another deacon";

            _repository.SendEmailString(_fromEmail, _toEmail, subject, plainTextContent, htmlContent);
        }


    }
}
