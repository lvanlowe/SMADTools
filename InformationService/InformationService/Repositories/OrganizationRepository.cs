using System;
using System.Collections.Generic;
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
    }
}
