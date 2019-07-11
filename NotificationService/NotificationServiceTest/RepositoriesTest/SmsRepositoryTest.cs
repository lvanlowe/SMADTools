using System.Configuration;
using Microsoft.Extensions.Configuration;
using NotificationService.Interfaces;
using NotificationService.Repositories;
using Xunit;

namespace NotificationServiceTest.RepositoriesTest
{
    public class SmsRepositoryTest
    {
        private ISmsRepository _repository;
        private string _toPhone;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
        public SmsRepositoryTest()
        {
            var config = InitConfiguration();
            var accountSid = config["AccountSid"];
            var authToken = config["AuthToken"];
            var fromPhone = config["FromPhone"];
            _toPhone = config["ToPhone"];

            _repository = new SmsRepository(accountSid, authToken, fromPhone);
        }

        [Fact]
        public void TestSendSms()
        {
            string body = "Join Earth's mightiest heroes. Like Kevin Bacon.";

            _repository.SendSms(_toPhone, body);
        }

    }
}
