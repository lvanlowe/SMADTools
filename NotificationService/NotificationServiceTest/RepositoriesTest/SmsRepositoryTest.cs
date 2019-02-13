using System;
using System.Configuration;
using System.Text;
using NotificationService.Interfaces;
using NotificationService.Repositories;

namespace NotificationServiceTest.RepositoriesTest
{
    public class SmsRepositoryTest
    {
        private ISmsRepository _repository;

        public SmsRepositoryTest()
        {
            var accountSid = ConfigurationManager.AppSettings["AccountSid"];
            var authToken = ConfigurationManager.AppSettings["AuthToken"];
            var fromPhone = ConfigurationManager.AppSettings["FromPhone"];
            _repository = new SmsRepository(accountSid, authToken, fromPhone);
        }
    }
}
