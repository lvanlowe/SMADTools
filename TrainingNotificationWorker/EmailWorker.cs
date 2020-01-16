using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using InformationService.DataModels;
using InformationService.Interfaces;
using InformationService.Models;
using InformationService.Repositories;
using InterfaceModels;
using Microsoft.EntityFrameworkCore;
using NotificationService.Interfaces;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;

namespace TrainingNotificationWorker
{
    public class EmailWorker : MessageWorker
    {

        private ITrainingRepository _trainingRepository;
        private IOrganizationRepository _organizationRepository;
        private IEmailRepository _emailRepository;

        public EmailWorker(ITrainingRepository trainingRepository, IEmailRepository emailRepository)
        {
            _trainingRepository = trainingRepository;
            _emailRepository = emailRepository;
        }

        public EmailWorker(IOrganizationRepository organizationRepository, IEmailRepository emailRepository)
        {
            _organizationRepository = organizationRepository;
            _emailRepository = emailRepository;
        }


        public async Task<List<SportEmails>> GetEmailsForSport(int sportId)
        {
            var emails = await _trainingRepository.GetEmailsBySport(sportId);
            return emails;
        }

        public List<string> RemoveDuplicateEmails(List<SportEmails> emails)
        {
            var emailList = emails.Select(e => e.Email.ToLower()).Distinct().ToList();
            return emailList;
        }

        public async Task SendEmailsAsync(List<string> emailList, string fromEmail, string subject, string plainTextContent, string htmlContent)
        {
            foreach (var toEmail in emailList)
            {
               await _emailRepository.SendEmailString(fromEmail, toEmail, subject, plainTextContent, htmlContent);
            }
        }

        public async Task<int> SendEmailsForSport(CoachEmailDto message)
        {
            var sportEmailList = await GetEmailsForSport(Convert.ToInt32(message.SportId));
            var volEmailList = GetAddresses(message, sportEmailList);
            var emailList = RemoveDuplicateEmails(volEmailList);
            emailList.Add(message.From);
            await SendEmailsAsync(emailList, message.From, message.Subject, message.PlainTextContent, message.HtmlContent);
            return emailList.Count;
        }


        public async Task<int> SendAdminEmails(OrganizationEmailDto message)
        {
            var emailList = await GetEmailsForOrganization(message.IsVolunteer, message.IsAthlete);
            if (message.IsTesting)
            {
                var testEmails = new List<string>
                {
                    "van@nuttin-but.net", "lvanlowe@comcast.net", "webmaster@pwsova.org"
                };
                emailList = testEmails;
            }
            emailList.Add(message.From);
            emailList = emailList.ConvertAll(d => d.ToLower());
            emailList = emailList.Distinct().ToList();
            await SendEmailsAsync(emailList, message.From, message.Subject, message.PlainTextContent, message.HtmlContent);
            return emailList.Count;

        }

        public async Task<List<string>> GetEmailsForOrganization(bool isVolunteer, bool isAthlete)
        {
            var emails = await _organizationRepository.GetEmails(isVolunteer, isAthlete);
            return emails;
        }
    }
}
