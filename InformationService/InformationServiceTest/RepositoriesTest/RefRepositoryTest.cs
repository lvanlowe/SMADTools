using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Interfaces;
using InformationService.Repositories;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace InformationServiceTest.RepositoriesTest
{
    public class RefRepositoryTest
    {

        IRefRepository _repository;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        public RefRepositoryTest()
        {
            var config = InitConfiguration();
            var connectionString = config["ConnectionString"];
            _repository = new RefRepository(connectionString);

        }

        [Fact]
        public void GetEventByNameTest_When_id_found_Then_notificationEntity()
        {
            const string eventName = "Track";

            var actual =_repository.GetEventByName(eventName);

            Assert.Equal("You have sigh up for notifications for the Hylton Track Meet", actual.Result.Message);
        }
    }
}
