using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationService.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private PwsoContext _context;

        public OrganizationRepository(PwsoContext context)
        {
            _context = context;
        }

        public async Task<List<Athletes>> GetAllAthletes()
        {
            var athletes = await _context.Athletes.ToListAsync();
            return athletes;
        }

        public async Task<List<Athletes>> FindAthletesByFirstLetter(char letter)
        {
            var athletes = await _context.Athletes.Where(a => a.LastName.StartsWith(letter)).ToListAsync();
            return athletes;
        }

        public async Task<Athletes> FindAthleteByName(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }
}
