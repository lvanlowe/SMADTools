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

        private void LoadRegistrants()
        {
            var registrant1 = new Registrant()
            {
                Id = 1,
                FirstName = "Clark",
                LastName = "Kent",
                ProgramId = 1,
                SportId = 1,
                SportTypeId = 1,
                TeamId = 1,
                Selected = true
            }; var registrant2 = new Registrant()
            {
                Id = 1,
                FirstName = "Bruce",
                LastName = "Wayne",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 1,
                TeamId = 1,
                Selected = true
            }; var registrant3 = new Registrant()
            {
                Id = 1,
                FirstName = "Barry",
                LastName = "Allen",
                ProgramId = 3,
                SportId = 2,
                SportTypeId = 1,
                TeamId = 2,
                IsVolunteer = true
            }; var registrant4 = new Registrant()
            {
                Id = 1,
                FirstName = "Hal",
                LastName = "Jordan",
                ProgramId = 3,
                SportId = 1,
                SportTypeId = 1,
                TeamId = 2,
                Selected = true
            }; var registrant5 = new Registrant()
            {
                Id = 1,
                FirstName = "Diana",
                LastName = "Prince",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 2,
                TeamId = 3
            }; var registrant6 = new Registrant()
            {
                Id = 1,
                FirstName = "Oliver",
                LastName = "Queen",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 2,
                TeamId = 3
            }; var registrant7 = new Registrant()
            {
                Id = 1,
                FirstName = "Ray",
                LastName = "Palmer",
                ProgramId = 3,
                SportId = 2,
                SportTypeId = 4,
                TeamId = 4
            }; var registrant8 = new Registrant()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Jones",
                ProgramId = 3,
                SportId = 2,
                SportTypeId = 4,
                TeamId = 4
            }; var registrant9 = new Registrant()
            {
                Id = 1,
                FirstName = "Dick",
                LastName = "Grayson",
                ProgramId = 4,
                SportId = 3,
                IsVolunteer = true
            }; var registrant0  = new Registrant()
            {
                Id = 1,
                FirstName = "Iris",
                LastName = "West",
                ProgramId = 4,
                SportId = 3,
            };
            _context.Registrant.Add(new Registrant() {Id = 1, FirstName = "Clark", LastName = "Kent", ProgramId = 1, SportId = 1, SportTypeId = 1, TeamId = 1});
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
