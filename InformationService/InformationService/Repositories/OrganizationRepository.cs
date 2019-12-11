using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationService.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly PwsoContext _context;

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
            var athlete = await _context.Athletes.Where(a => a.LastName == lastName && a.FirstName == firstName)
                .FirstOrDefaultAsync();
            return athlete;
        }

        public async Task<Athletes> FindAthleteById(int id)
        {
            var athlete = await _context.Athletes.Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            return athlete;
        }

        public Task<List<string>> GetEmails(bool isVolunteer, bool isAthlete)
        {
            throw new System.NotImplementedException();
        }
    }
}