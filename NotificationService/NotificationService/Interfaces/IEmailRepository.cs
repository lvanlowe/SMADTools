using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace NotificationService.Interfaces
{
    public interface IEmailRepository
    {
        Task<Response> SendEmail(EmailAddress from, EmailAddress to, EmailAddress cc, string subject, string plainTextContent,
            string htmlContent);
    }
}
