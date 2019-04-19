using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Models;
using InformationService.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InformationServiceTest.RepositoriesTest
{
    public class ReferenceRepositoryTest
    {
        private PwsodbContext _context;

        public ReferenceRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PwsodbContext>().
                UseInMemoryDatabase(databaseName: "ReferenceRepository")
                .Options;
            _context = new PwsodbContext(options);
        }

        private void InitializeSports()
        {
            var sports = _context.Sports.ToListAsync();
            _context.RemoveRange(sports.Result);
            _context.SaveChanges();
        }

        [Theory]
        [InlineData(3)]
        public void GetAllSports_When_executed_create_list_of_Sports(int last)

        {
            // Insert seed data into the database using one instance of the context
            InitializeSports();

            for (int i = 1; i < last + 1; i++)
            {
                _context.Sports.Add(new Sports() { Id = i, Name = "Baseball" + i, Email = i + "Jones@pwsova.org"  });
            }

            _context.SaveChanges();

            var repository = new ReferenceRepository(_context);
            var actual = repository.GetAllSports();

            Assert.Equal(last, actual.Result.Count);

        }
    }
}
