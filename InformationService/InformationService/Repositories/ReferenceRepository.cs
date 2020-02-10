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
    public class ReferenceRepository : IReferenceRepository
    {
        private PwsodbContext _context;

        public ReferenceRepository(PwsodbContext context)
        {
            _context = context;
        }

        public async Task<List<Sports>> GetAllSports()
        {
            var sports = await _context.Sports.ToListAsync();
            return sports;
        }

        public async Task<List<Programs>> GetLocationBySport(int sportId)
        {
            var locations = await _context.Programs.Where(p => p.Sport == sportId).ToListAsync();
            return locations;
        }

        public async Task<List<SportTypes>> GetCategoryBySport(int sportId)
        {
            var categories = await _context.SportTypes.Where(s => s.SportId == sportId).ToListAsync();
            return categories;
        }

        public async Task<List<Teams>> GetTeamBySport(int sportId)
        {
            var teams = await _context.Teams.Where(t => t.Program.Sport == sportId).ToListAsync();
            return teams;
        }

        public async Task<List<Sports>> GetSports()
        {
            var sports = await _context.Sports
                .Include(s => s.Programs)
                .ThenInclude(p => p.Teams).ToListAsync();
            return sports;
        }

        public async Task<Programs> GetLocationByProgramId(long programId)
        {
            var location = await _context.Programs.Where(p => p.Id == programId)
                .Include(p => p.SportNavigation)
                .FirstOrDefaultAsync();
            return location;
        }
    }
}
