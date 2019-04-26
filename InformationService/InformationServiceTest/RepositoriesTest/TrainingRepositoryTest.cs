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
            RegistrantEmail email11 = new RegistrantEmail() { Id = 1, Email = "clark@superman.com", RegistrantId = 1 };
            RegistrantEmail email21 = new RegistrantEmail() { Id = 2, Email = "bruce@batman.com", RegistrantId = 2 };
            RegistrantEmail email22 = new RegistrantEmail() { Id = 3, Email = "alfred@batman.com", RegistrantId = 2 };
            RegistrantEmail email41 = new RegistrantEmail() { Id = 4, Email = "hal@greenlantern.org", RegistrantId = 4 };
            RegistrantEmail email51 = new RegistrantEmail() { Id = 5, Email = "diana@wonder.com", RegistrantId = 5 };
            RegistrantEmail email52 = new RegistrantEmail() { Id = 6, Email = "steve@wonder.com", RegistrantId = 5 };
            RegistrantEmail email53 = new RegistrantEmail() { Id = 7, Email = "fannie@wonder.com", RegistrantId = 5 };
            RegistrantEmail email61 = new RegistrantEmail() { Id = 8, Email = "oliver@arrow.net", RegistrantId = 6};
            RegistrantEmail email71 = new RegistrantEmail() { Id = 9, Email = "ray@atom.org", RegistrantId = 7 };
            RegistrantEmail email72 = new RegistrantEmail() { Id = 10, Email = "palmer@atom.org", RegistrantId = 7 };
            RegistrantEmail email81 = new RegistrantEmail() { Id = 11, Email = "jj@manhunter.com", RegistrantId = 8 };
            RegistrantEmail email82 = new RegistrantEmail() { Id = 12, Email = "jones@manhunter.com", RegistrantId = 8 };
            RegistrantEmail email83 = new RegistrantEmail() { Id = 13, Email = "john@manhunter.com", RegistrantId = 8 };
            RegistrantEmail email91 = new RegistrantEmail() { Id = 14, Email = "dick@batman.com", RegistrantId = 9 };
            RegistrantEmail email01 = new RegistrantEmail() { Id = 15, Email = "barbara@batman.com", RegistrantId = 9 };
            RegistrantEmail email02 = new RegistrantEmail() { Id = 16, Email = "iris@flash.net", RegistrantId = 10 };

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
                Id = 2,
                FirstName = "Bruce",
                LastName = "Wayne",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 1,
                TeamId = 1,
                Selected = true
            }; var registrant3 = new Registrant()
            {
                Id = 3,
                FirstName = "Barry",
                LastName = "Allen",
                ProgramId = 3,
                SportId = 2,
                SportTypeId = 1,
                TeamId = 2,
                IsVolunteer = true
            }; var registrant4 = new Registrant()
            {
                Id = 4,
                FirstName = "Hal",
                LastName = "Jordan",
                ProgramId = 3,
                SportId = 1,
                SportTypeId = 1,
                TeamId = 2,
                Selected = true
            }; var registrant5 = new Registrant()
            {
                Id = 5,
                FirstName = "Diana",
                LastName = "Prince",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 2,
                TeamId = 3
            }; var registrant6 = new Registrant()
            {
                Id = 6,
                FirstName = "Oliver",
                LastName = "Queen",
                ProgramId = 2,
                SportId = 1,
                SportTypeId = 2,
                TeamId = 3
            }; var registrant7 = new Registrant()
            {
                Id = 7,
                FirstName = "Ray",
                LastName = "Palmer",
                ProgramId = 3,
                SportId = 2,
                SportTypeId = 4,
                TeamId = 4
            }; var registrant8 = new Registrant()
            {
                Id = 8,
                FirstName = "John",
                LastName = "Jones",
                ProgramId = 3,
                SportId = 2,
                SportTypeId = 4,
                TeamId = 4
            }; var registrant9 = new Registrant()
            {
                Id = 9,
                FirstName = "Dick",
                LastName = "Grayson",
                ProgramId = 4,
                SportId = 3,
                IsVolunteer = true
            }; var registrant0  = new Registrant()
            {
                Id = 10,
                FirstName = "Iris",
                LastName = "West",
                ProgramId = 4,
                SportId = 3,
            };
            registrant0.RegistrantEmail.Add(email01);
            registrant0.RegistrantEmail.Add(email02);
            registrant1.RegistrantEmail.Add(email11);
            registrant2.RegistrantEmail.Add(email21);
            registrant2.RegistrantEmail.Add(email22);
            registrant4.RegistrantEmail.Add(email41);
            registrant5.RegistrantEmail.Add(email51);
            registrant5.RegistrantEmail.Add(email52);
            registrant5.RegistrantEmail.Add(email53);
            registrant6.RegistrantEmail.Add(email61);
            registrant7.RegistrantEmail.Add(email71);
            registrant7.RegistrantEmail.Add(email72);
            registrant8.RegistrantEmail.Add(email81);
            registrant8.RegistrantEmail.Add(email82);
            registrant8.RegistrantEmail.Add(email83);
            registrant9.RegistrantEmail.Add(email91);
            _context.Registrant.Add(registrant0);
            _context.Registrant.Add(registrant1);
            _context.Registrant.Add(registrant2);
            _context.Registrant.Add(registrant3);
            _context.Registrant.Add(registrant4);
            _context.Registrant.Add(registrant5);
            _context.Registrant.Add(registrant6);
            _context.Registrant.Add(registrant7);
            _context.Registrant.Add(registrant8);
            _context.Registrant.Add(registrant9);

            _context.SaveChanges();
        }

        [Theory]
        [InlineData(1, 2)]
        //[InlineData(7)]
        //[InlineData(10)]
        public void GetEmailsBySport_When_executed_create_list_of_SportEmails(int sportId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeRegistrants();
            LoadRegistrants();

            //for (int i = 1; i < last + 1; i++)
            //{
            //    _context.Sports.Add(new Sports() { Id = i, Name = "Baseball" + i, Email = i + "Jones@pwsova.org" });
            //}

            //_context.SaveChanges();

            var repository = new TrainingRepository(_context);
            var actual = repository.GetEmailsBySport(sportId);

            Assert.Equal(expected, actual.Result.Count);

        }
    }
}
