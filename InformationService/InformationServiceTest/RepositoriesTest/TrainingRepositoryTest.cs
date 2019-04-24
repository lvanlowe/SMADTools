using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Models;
using InformationService.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InformationServiceTest.RepositoriesTest
{
    public class TrainingRepositoryTest
    {
        private PwsodbContext _context;

        public TrainingRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PwsodbContext>().
                UseInMemoryDatabase(databaseName: "TrainingRepository")
                .Options;
            _context = new PwsodbContext(options);
        }

        private void InitializeRegistrants()
        {
            var emails = _context.RegistrantEmail.ToListAsync();
            _context.RemoveRange(emails.Result);
            var phones = _context.RegistrantPhone.ToListAsync();
            _context.RemoveRange(phones.Result);
            var athletes = _context.RegisteredAthlete.ToListAsync();
            _context.RemoveRange(athletes.Result);
            var registrants = _context.Registrant.ToListAsync();
            _context.RemoveRange(registrants.Result);
            _context.SaveChanges();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        [InlineData(10)]
        public void GetEmailsBySports_When_executed_create_list_of_Sports(int last)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();

            for (int i = 1; i < last + 1; i++)
            {
                _context.Sports.Add(new Sports() { Id = i, Name = "Baseball" + i, Email = i + "Jones@pwsova.org" });
            }

            _context.SaveChanges();

            var repository = new ReferenceRepository(_context);
            var actual = repository.GetAllSports();

            Assert.Equal(last, actual.Result.Count);

        }
    }
}
