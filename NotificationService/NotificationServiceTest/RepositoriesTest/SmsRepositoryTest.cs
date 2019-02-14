using System.Configuration;
using NotificationService.Interfaces;
using NotificationService.Repositories;
using Xunit;

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

        [Fact]
        public void TestSendSms()
        {
            string body = "Join Earth's mightiest heroes. Like Kevin Bacon.";
            var toPhone = ConfigurationManager.AppSettings["ToPhone"];

            _repository.SendSms(toPhone, body);

        }

    }
}
