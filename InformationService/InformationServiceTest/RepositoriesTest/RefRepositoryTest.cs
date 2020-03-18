using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Interfaces;
using InformationService.Repositories;
using Microsoft.Extensions.Configuration;

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
            _repository = new RefRepository();

        }
    }
}
