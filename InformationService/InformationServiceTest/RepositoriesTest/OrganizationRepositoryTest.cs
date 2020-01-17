using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Models;
using InformationService.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InformationServiceTest.RepositoriesTest
{
    public class OrganizationRepositoryTest
    {
        private PwsoContext _context;
        private PwsodbContext _calendarContext;


        public OrganizationRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PwsoContext>().
                UseInMemoryDatabase(databaseName: "OrganizationRepository")
                .Options;
            _context = new PwsoContext(options);

            var calendarOptions = new DbContextOptionsBuilder<PwsodbContext>().
                UseInMemoryDatabase(databaseName: "CalendarRepository")
                .Options;
            _calendarContext = new PwsodbContext(calendarOptions);
        }

        private void InitializeAthletes()
        {
            var athletes = _context.Athletes.ToListAsync();
            _context.RemoveRange(athletes.Result);
            _context.SaveChanges();
        }

        private void InitializeEmails()
        {
            var emails = _context.Newsletter.ToListAsync();
            _context.RemoveRange(emails.Result);
            _context.SaveChanges();
        }

        private void InsertAthletes()
        {
            _context.Athletes.Add(new Athletes { Id = 1, FirstName = "Clark", LastName = "Kent" });
            _context.Athletes.Add(new Athletes { Id = 2, FirstName = "Bruce", LastName = "Wayne" });
            _context.Athletes.Add(new Athletes { Id = 3, FirstName = "Diana", LastName = "Prince" });
            _context.Athletes.Add(new Athletes { Id = 4, FirstName = "Barry", LastName = "Allen" });
            _context.Athletes.Add(new Athletes { Id = 5, FirstName = "Hal", LastName = "Jordon" });


            _context.SaveChanges();
        }

        private void InsertEmails()
        {
            _context.Newsletter.Add(new Newsletter { Id = 1, FirstName = "Clark", LastName = "Kent", Email = "superman@dc.com",IsAthlete = true, IsVolunteer = false});
            _context.Newsletter.Add(new Newsletter { Id = 2, FirstName = "Bruce", LastName = "Wayne", Email = "superman@dc.com", IsAthlete = true, IsVolunteer = true });
            _context.Newsletter.Add(new Newsletter { Id = 3, FirstName = "Diana", LastName = "Prince", Email = "superman@dc.com", IsAthlete = false, IsVolunteer = true });
            _context.Newsletter.Add(new Newsletter { Id = 4, FirstName = "Barry", LastName = "Allen", Email = "superman@dc.com", IsAthlete = true, IsVolunteer = false });
            _context.Newsletter.Add(new Newsletter { Id = 5, FirstName = "Hal", LastName = "Jordon", Email = "superman@dc.com", IsAthlete = true, IsVolunteer = true });

            _context.SaveChanges();
        }

        private void InsertCalendarEvents()
        {
            //_context.Athletes.Add(new Athletes { Id = 1, FirstName = "Clark", LastName = "Kent" });
            //_context.Athletes.Add(new Athletes { Id = 2, FirstName = "Bruce", LastName = "Wayne" });
            //_context.Athletes.Add(new Athletes { Id = 3, FirstName = "Diana", LastName = "Prince" });
            //_context.Athletes.Add(new Athletes { Id = 4, FirstName = "Barry", LastName = "Allen" });
            //_context.Athletes.Add(new Athletes { Id = 5, FirstName = "Hal", LastName = "Jordon" });


            //_context.SaveChanges();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(13)]
        [InlineData(9)]

        public void GetAllAthletes_When_executed_create_list_of_Athletes(int last)

        {
            // Insert seed data into the database using one instance of the context
            InitializeAthletes();

            for (int i = 1; i < last + 1; i++)
            {
                _context.Athletes.Add(new Athletes {Id = i, FirstName = "Tom" + i, LastName = "Jones" + i});
            }

            _context.SaveChanges();

            var repository = new OrganizationRepository(_context);
            var actual = repository.GetAllAthletes();

            Assert.Equal(last, actual.Result.Count);

        }


        [Theory]
        [InlineData(3, 'J', 3)]
        [InlineData(13, 'P', 13)]
        [InlineData(5, 'A', 0)]
        public void FindAthletesByFirstLetter_When_executed_create_list_of_Athletes_Start_Last_Given_Letter(int last, char letter, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeAthletes();

            for (int i = 1; i < last + 1; i++)
            {
                _context.Athletes.Add(new Athletes { Id = i, FirstName = "Tom" + i, LastName = "Jones" + i });
            }
            for (int i = 1; i < last + 1; i++)
            {
                _context.Athletes.Add(new Athletes { Id = i + last, FirstName = "Peter" + i, LastName = "Parker" + i });
            }

            _context.SaveChanges();

            var repository = new OrganizationRepository(_context);
            var actual = repository.FindAthletesByFirstLetter(letter);

            Assert.Equal(expected, actual.Result.Count);

        }


        [Theory]
        [InlineData("Clark", "Kent", 1)]
        [InlineData("Diana", "Prince", 3)]
        [InlineData("Hal", "Jordon", 5)]
        public void FindAthleteByName_When_name_Then_athlete_with_name(string firstName, string lastName, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeAthletes();
            InsertAthletes();

            var repository = new OrganizationRepository(_context);
            var actual = repository.FindAthleteByName(firstName, lastName);

            Assert.Equal(firstName, actual.Result.FirstName);
            Assert.Equal(lastName, actual.Result.LastName);
            Assert.Equal(expected, actual.Result.Id);

        }

        [Theory]
        [InlineData("Clark", "Kent", 1)]
        [InlineData("Diana", "Prince", 3)]
        [InlineData("Hal", "Jordon", 5)]
        public void FindAthleteById_When_Id_Then_athlete_with_name(string firstName, string lastName, int id)

        {
            // Insert seed data into the database using one instance of the context
            InitializeAthletes();
            InsertAthletes();

            var repository = new OrganizationRepository(_context);
            var actual = repository.FindAthleteById(id);

            Assert.Equal(firstName, actual.Result.FirstName);
            Assert.Equal(lastName, actual.Result.LastName);

        }

        [Theory]
        [InlineData(true, false, 3)]
        [InlineData(false, true, 4)]
        [InlineData(true, true, 5)]
        public void GetEmails_When_found_Then_emails(bool isAthlete, bool isVolunteer, int count)

        {
            // Insert seed data into the database using one instance of the context
            InitializeEmails();
            InsertEmails();

            var repository = new OrganizationRepository(_context);
            var actual = repository.GetEmails(isAthlete, isVolunteer);

            Assert.Equal(count, actual.Result.Count);

        }

    }
}
