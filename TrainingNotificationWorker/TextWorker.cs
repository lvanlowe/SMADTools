using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.DataModels;
using InformationService.Interfaces;
using NotificationService.Interfaces;

namespace TrainingNotificationWorker
{
    public class TextWorker : MessageWorker
    {
        private ITrainingRepository _trainingRepository;
        private ISmsRepository _smsRepository;


        public TextWorker(ITrainingRepository trainingRepository, ISmsRepository smsRepository)
        {
            _trainingRepository = trainingRepository;
            _smsRepository = smsRepository;
        }

        public async Task<List<SportEmails>> GetPhonesForSport(int sportId)
        {
            var emails = await _trainingRepository.GetPhonesBySport(sportId);
            return emails;
        }
    }
}
