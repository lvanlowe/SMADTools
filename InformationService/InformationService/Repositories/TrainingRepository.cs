using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationService.DataModels;
using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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

            var phones = await _context.Registrant
                .Join(_context.RegistrantEmail,
                    r => r.Id,
                    e => e.RegistrantId,
                    (r, e) => new { r, e }).Where(c => c.r.SportId == sportId).Select(c => new SportEmails
                {
                    FirstName = c.r.FirstName,
                    LastName = c.r.LastName,
                    NickName = c.r.NickName,
                    ProgramId = c.r.ProgramId,
                    SportTypeId = c.r.SportTypeId,
                    TeamId = c.r.TeamId,
                    Email = c.e.Email,
                    Selected = c.r.Selected,
                    IsVolunteer = c.r.IsVolunteer
                }).ToListAsync();
            return emails;
        }

        public async Task<List<SportPhones>> GetPhonesBySport(int sportId)
        {

            var phones = await _context.Registrant
                .Join(_context.RegistrantPhone,
                    r => r.Id,
                    p => p.RegistrantId,
                    (r, p) => new {r, p}).Where(c => c.r.SportId == sportId && c.p.CanText).Select(c => new SportPhones
                {
                    FirstName = c.r.FirstName,
                    LastName = c.r.LastName,
                    NickName = c.r.NickName,
                    ProgramId = c.r.ProgramId,
                    SportTypeId = c.r.SportTypeId,
                    TeamId = c.r.TeamId,
                    Phone = c.p.Phone,
                    Selected = c.r.Selected,
                    IsVolunteer = c.r.IsVolunteer
                }).ToListAsync();

            return phones;
        }
    }
}
