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

        public OrganizationRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PwsoContext>().
                UseInMemoryDatabase(databaseName: "OrganizationRepository")
                .Options;
            _context = new PwsoContext(options);
        }


        [Theory]
        [InlineData(3)]
        [InlineData(13)]
        [InlineData(9)]

        public void GetAllAthletes_When_executed_create_list_of_Athletes(int last)

        {
            // Insert seed data into the database using one instance of the context
            var athletes = _context.Athletes.ToListAsync();
            _context.RemoveRange(athletes.Result);
            _context.SaveChanges();

            for (int i = 1; i < last + 1; i++)
            {
                _context.Athletes.Add(new Athletes {Id = i, FirstName = "Tom" + i, LastName = "Jones" + i});
            }

            _context.SaveChanges();

            var repository = new OrganizationRepository(_context);
            var actual = repository.GetAllAthletes();

            Assert.Equal(last, actual.Result.Count);

        }
    }
}
