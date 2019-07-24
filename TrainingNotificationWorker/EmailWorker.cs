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
using Microsoft.EntityFrameworkCore;

namespace TrainingNotificationWorker
{
    public class EmailWorker
    {

        private ITrainingRepository _repository;

        public EmailWorker(ITrainingRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<SportEmails>> GetEmailsForSport(int sportId)
        {
            var emails = await _repository.GetEmailsBySport(sportId);
            return emails;
        }

        public List<SportEmails> GetEmailsForLocation(int? locationId, List<SportEmails> emails)
        {
            if (locationId == null || locationId == 0)
            {
                return emails;
            }
            var locationEmails = emails.Where(e => e.ProgramId == locationId).ToList();
            return locationEmails;
        }

        public List<SportEmails> GetEmailsForCategory(int? categoryId, List<SportEmails> emails)
        {
            if (categoryId == null || categoryId == 0)
            {
                return emails;
            }
            var categoryEmail = emails.Where(e => e.SportTypeId == categoryId).ToList();
            return categoryEmail;
        }

        public List<SportEmails> GetEmailsForTeam(int? teamId, List<SportEmails> emails)
        {
            if (teamId == null || teamId == 0)
            {
                return emails;
            }
            var teamEmails = emails.Where(e => e.TeamId == teamId).ToList();
            return teamEmails;
        }

        public List<SportEmails> GetEmailsForSelected(bool? isSelected, List<SportEmails> emails)
        {
            if (isSelected.HasValue && isSelected.Value)
            {
                return emails.Where(e => e.Selected == true || e.IsVolunteer == true).ToList();
            }
            return emails;
        }

        public List<SportEmails> GetEmailsForVolunteers(bool? isVolunteer, List<SportEmails> emails)
        {
            if (isVolunteer.HasValue && isVolunteer.Value)
            {
                return emails.Where(e => e.IsVolunteer == true).ToList();
            }

            return emails;
        }

        public List<string> RemoveDuplicateEmails(List<SportEmails> emails)
        {
            var emailList = emails.Select(e => e.Email.ToLower()).Distinct().ToList();
            return emailList;
        }
    }
}
