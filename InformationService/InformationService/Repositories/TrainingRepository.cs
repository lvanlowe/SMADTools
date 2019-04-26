using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationService.DataModels;
using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationService.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private PwsodbContext _context;

        public TrainingRepository(PwsodbContext context)
        {
            _context = context;
        }

        public async Task<List<SportEmails>> GetEmailsBySport(int sportId)
        {
            var emails = await _context.Registrant.Where(r => r.SportId == sportId).Select(r => new SportEmails{FirstName = r.FirstName, LastName = r.LastName, NickName = r.NickName, ProgramId = r.ProgramId, SportTypeId = r.SportTypeId, TeamId = r.TeamId }).ToListAsync();

            return emails;
        }
    }
}
