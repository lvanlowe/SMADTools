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
            var teams = _context.Teams.ToListAsync();
            _context.RemoveRange(teams.Result);
            var categogies = _context.SportTypes.ToListAsync();
            _context.RemoveRange(categogies.Result);
            var locations = _context.Programs.ToListAsync();
            _context.RemoveRange(locations.Result);
            var sports = _context.Sports.ToListAsync();
            _context.RemoveRange(sports.Result);
            var places = _context.Location.ToListAsync();
            _context.RemoveRange(places.Result);
            var times = _context.CalendarTimes.ToListAsync();
            _context.RemoveRange(times.Result);
            _context.SaveChanges();
        }

        private void LoadSports()
        {
            _context.Sports.Add(new Sports() { Id = 1, Name = "Basketball", IsTeamSport = true});
            _context.Sports.Add(new Sports() { Id = 2, Name = "Soccer", IsTeamSport = true});
            _context.Sports.Add(new Sports() { Id = 3, Name = "Track", IsTeamSport = false});
            _context.Programs.Add(new Programs() { Id = 1, Sport = 1, Name = "Woodbridge" });
            _context.Programs.Add(new Programs() { Id = 2, Sport = 1, Name = "Gainesville" });
            _context.Programs.Add(new Programs() { Id = 3, Sport = 2, });
            _context.Programs.Add(new Programs() { Id = 4, Sport = 3, Name = "Dale City" });
            _context.Programs.Add(new Programs() { Id = 5, Sport = 3, Name = "Montclair" });
            _context.Programs.Add(new Programs() { Id = 6, Sport = 3, Name = "Lake Ridge" });
            _context.SportTypes.Add(new SportTypes() { Id = 1, SportId = 1, Name = "Full Court" });
            _context.SportTypes.Add(new SportTypes() { Id = 2, SportId = 1, Name = "Half Court" });
            _context.SportTypes.Add(new SportTypes() { Id = 3, SportId = 1, Name = "Skills" });
            _context.SportTypes.Add(new SportTypes() { Id = 4, SportId = 2, Name = "Skills" });
            _context.SportTypes.Add(new SportTypes() { Id = 5, SportId = 2, Name = "Non Skills" });
            _context.Teams.Add(new Teams() { Id = 1, ProgramId = 1, Name = "Gladiators", SportType = 1 });
            _context.Teams.Add(new Teams() { Id = 2, ProgramId = 1, Name = "Bulls", SportType = 1 });
            _context.Teams.Add(new Teams() { Id = 3, ProgramId = 1, Name = "Vikings", SportType = 2 });
            _context.Teams.Add(new Teams() { Id = 4, ProgramId = 2, Name = "Dominators", SportType = 1 });
            _context.Teams.Add(new Teams() { Id = 5, ProgramId = 2, Name = "Liberty", SportType = 2 });
            _context.Teams.Add(new Teams() { Id = 6, ProgramId = 3, Name = "Ravens", SportType = 5 });
            _context.Location.Add(new Location()
            {
                id = 1, Name = "Potomac High School", Street = "123 Main Street", City = "Woodbridge", State = "VA",
                Zip = "22193"
            });
            _context.Location.Add(new Location()
            {
                id = 2,
                Name = "Potomac Middle School",
                Street = "129 Main Street",
                City = "Woodbridge",
                State = "VA",
                Zip = "22193"
            });
            _context.Location.Add(new Location()
            {
                id = 3,
                Name = "Saunders Middle School",
                Street = "129 Dale Bvld",
                City = "Dale City",
                State = "VA",
                Zip = "22192"
            });
            _context.CalendarTimes.Add(new CalendarTimes {Id = 1, TimeHour = "12 Noon"});
            _context.CalendarTimes.Add(new CalendarTimes {Id = 2, TimeHour = "6:30 AM"});
            _context.CalendarTimes.Add(new CalendarTimes {Id = 3, TimeHour = "1:00 PM"});
            _context.SaveChanges();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        [InlineData(10)]
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


        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 3)]
        public void GetLocationBySport_When_executed_create_list_of_Programs_bySport(int sportId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeSports();
            LoadSports();

            var repository = new ReferenceRepository(_context);
            var actual = repository.GetLocationBySport(sportId);

            Assert.Equal(expected, actual.Result.Count);

        }



        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 2)]
        [InlineData(3, 0)]
        public void GetCategoryBySport_When_executed_create_list_of_Categories_bySport(int sportId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeSports();
            LoadSports();

            var repository = new ReferenceRepository(_context);
            var actual = repository.GetCategoryBySport(sportId);

            Assert.Equal(expected, actual.Result.Count);

        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 1)]
        [InlineData(3, 0)]
        public void GetTeamBySport_When_executed_create_list_of_Teams_bySport(int sportId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeSports();
            LoadSports();

            var repository = new ReferenceRepository(_context);
            var actual = repository.GetTeamBySport(sportId);

            Assert.Equal(expected, actual.Result.Count);

        }

        [Theory]
        [InlineData()]
        public void GetSports_When_executed_create_list_of_Sport_with_Children()

        {
            // Insert seed data into the database using one instance of the context
            InitializeSports();
            LoadSports();

            var repository = new ReferenceRepository(_context);
            var actual = repository.GetSports();

            Assert.Equal(3, actual.Result.Count);
            Assert.Equal(2, actual.Result[0].Programs.Count);

        }

        [Theory]
        [InlineData(1, "Woodbridge", "Basketball")]
        [InlineData(2, null, "Soccer")]
        [InlineData(3, "Montclair", "Track")]
        public void GetLocationByProgramId_When_executed_return_Programs_for_id(int programId, string locationName, string sportName)

        {
            // Insert seed data into the database using one instance of the context
            InitializeSports();
            LoadSports();

            var repository = new ReferenceRepository(_context);
            var actual = repository.GetLocationByProgramId(programId);

            Assert.Equal(locationName, actual.Result.Name);
            Assert.Equal(sportName, actual.Result.SportNavigation.Name);

        }

        [Theory]
        [InlineData(1, "Potomac High School", "123 Main Street")]
        [InlineData(2, "Potomac Middle School", "129 Main Street")]
        [InlineData(3, "Saunders Middle School", "129 Dale Bvld")]
        public void GetLocationByLocationId_When_executed_return_Programs_for_id(int locationId, string locationName, string address)

        {
            // Insert seed data into the database using one instance of the context
            InitializeSports();
            LoadSports();

            var repository = new ReferenceRepository(_context);
            var actual = repository.GetLocationByLocationId(locationId);
            
            Assert.Equal(locationName, actual.Result.Name);
            Assert.Equal(address, actual.Result.Street);

        }

        [Theory]
        [InlineData(1, "12 Noon")]
        [InlineData(2, "6:30 AM")]
        [InlineData(3, "1:00 PM")]
        public void GetTimeByTimeId_When_executed_return_Programs_for_id(int timeId, string timeData)
        {
            // Insert seed data into the database using one instance of the context
            InitializeSports();
            LoadSports();

            var repository = new ReferenceRepository(_context);
            var actual = repository.GetTimeByTimeId(timeId);

            Assert.Equal(timeData, actual.Result.TimeHour);

        }

    }
}
