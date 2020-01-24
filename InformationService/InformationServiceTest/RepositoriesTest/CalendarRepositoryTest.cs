using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Models;
using InformationService.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InformationServiceTest.RepositoriesTest
{
    public class CalendarRepositoryTest
    {
        private PwsodbContext _context;

        public CalendarRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PwsodbContext>().
                UseInMemoryDatabase(databaseName: "CalendarRepository")
                .Options;
            _context = new PwsodbContext(options);

        }

        [Theory]
        [InlineData(3)]
        //[InlineData(7)]
        //[InlineData(10)]
        public void GetPracticesForLocation_When_executed_create_list_of_Practice_Events(int locationId)

        {
            // Insert seed data into the database using one instance of the context
            //InitializeSports();

            for (int i = 1; i < locationId + 1; i++)
            {
                _context.Sports.Add(new Sports() { Id = i, Name = "Baseball" + i, Email = i + "Jones@pwsova.org" });
            }

            _context.SaveChanges();
            long programId = 1;
            DateTime startDate = DateTime.Now;
            var repository = new CalendarRepository(_context);
            var actual = repository.GetPracticesForLocation(programId, startDate);

            Assert.Equal(locationId, actual.Result.Count);

        }

    }


}
