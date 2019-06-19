using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NotificationService.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private SendGridClient _client;

        public EmailRepository(string apiKey)
        {
            var from = new EmailAddress("van@nuttin-but.net", "Van");
            var subject = "Deacon Of The Month";
            var to = new EmailAddress("lvanlowe@comcast.net", "Van Lowe");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent =
                "<br>Hi {{deacon}}<br><br>&nbsp;&nbsp;&nbsp;&nbsp;Just a reminder you are the Deacon on Duty for {{month}},<br><br>&nbsp;&nbsp;&nbsp;&nbsp;You will responsible to lock up the church on Sundays after worship. If you are not going to be there then it is your responsibility to get another Deacon to close up for you. You are responsible for taking out the trash. Also make sure the offering baskets are out for the next week.<br><br>&nbsp;&nbsp;&nbsp;&nbsp;If you are going to miss more than one Sunday in {{month}} please change with another deacon";

            _client = new SendGridClient(apiKey);

        }

        public async Task<Response> SendEmail(EmailAddress from, EmailAddress to, EmailAddress cc, string subject,
            string plainTextContent,
            string htmlContent)
        {
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            if (cc != null)
            {
                msg.AddCc(cc);
            }

            var response = await _client.SendEmailAsync(msg);
            return response;
        }

    }
}
