using System;
using System.Collections.Generic;
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
    }
}
