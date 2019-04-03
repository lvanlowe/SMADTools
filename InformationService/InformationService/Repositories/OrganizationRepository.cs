using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.Interfaces;
using InformationService.Models;

namespace InformationService.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private PwsoContext _context;

        public OrganizationRepository(PwsoContext context)
        {
            _context = context;
        }

        public Task<List<Athletes>> GetAllAthletes()
        {
            throw new NotImplementedException();
        }
    }
}
