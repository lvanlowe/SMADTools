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
    }
}
