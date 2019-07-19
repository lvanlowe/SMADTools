using System;
using System.Collections.Generic;
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
    }
}
