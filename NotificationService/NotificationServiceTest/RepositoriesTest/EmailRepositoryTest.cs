using System;
using System.Collections.Generic;
using System.Text;
using NotificationService.Interfaces;
using NotificationService.Repositories;
using SendGrid.Helpers.Mail;
using Xunit;

namespace NotificationServiceTest.RepositoriesTest
{
    public class EmailRepositoryTest
    {
        private IEmailRepository _repository;

        public EmailRepositoryTest()
        {
            const string apiKey = "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";

            _repository = new EmailRepository(apiKey);

        }
        [Fact]
        public void TestSendEmail()
        {
            var from = new EmailAddress("van@nuttin-but.net", "Van");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("lvanlowe@comcast.net", "Van Lowe");
            var cc = new EmailAddress("webmaster@pwsova.org", "Deacons");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent =
                "<br>Hi {{deacon}}<br><br>&nbsp;&nbsp;&nbsp;&nbsp;Just a reminder you are the Deacon on Duty for {{month}},<br><br>&nbsp;&nbsp;&nbsp;&nbsp;You will responsible to lock up the church on Sundays after worship. If you are not going to be there then it is your responsibility to get another Deacon to close up for you. You are responsible for taking out the trash. Also make sure the offering baskets are out for the next week.<br><br>&nbsp;&nbsp;&nbsp;&nbsp;If you are going to miss more than one Sunday in {{month}} please change with another deacon";
            _repository.SendEmail(from, to, cc, subject, plainTextContent, htmlContent).Wait();
        }

    }
}
