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
            var emails = await _context.Registrant
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

        public async Task<List<SportEmails>> GetPhonesBySport(int sportId)
        {

            var phones = await _context.Registrant
                .Join(_context.RegistrantPhone,
                    r => r.Id,
                    p => p.RegistrantId,
                    (r, p) => new {r, p}).Where(c => c.r.SportId == sportId && c.p.CanText).Select(c => new SportEmails
                    {
                    FirstName = c.r.FirstName,
                    LastName = c.r.LastName,
                    NickName = c.r.NickName,
                    ProgramId = c.r.ProgramId,
                    SportTypeId = c.r.SportTypeId,
                    TeamId = c.r.TeamId,
                    Email = c.p.Phone,
                    Selected = c.r.Selected,
                    IsVolunteer = c.r.IsVolunteer
                }).ToListAsync();

            return phones;
        }

        public async Task<List<Registrant>> GetRegistrantsBySport(int sportId)
        {
            var registrants = await _context.Registrant
                .Where(c => c.SportId == sportId && c.Year != "1999").ToListAsync();

            return registrants;
        }

        public async Task AddRegisteredAthlete(int registrantId, int athleteId)
        {
            var athlete = new RegisteredAthlete() { AthletesId = athleteId, RegistrantId = registrantId };
            var registrant = await _context.Registrant
                .Where(r => r.Id == registrantId).FirstOrDefaultAsync();
            registrant?.RegisteredAthlete.Add(athlete);
            _context.SaveChanges();

        }

        public async Task AddPhone(List<RegistrantPhone> phoneList)
        {
            var registrant = await _context.Registrant
                .Where(r => r.Id == phoneList[0].RegistrantId).FirstOrDefaultAsync();
            foreach (var phone in phoneList)
            {
                registrant?.RegistrantPhone.Add(phone);
            }
            _context.SaveChanges();

        }

        public async Task RemovePhone(List<RegistrantPhone> phoneList)
        {
            foreach (var phone in phoneList.Where(phone => phone != null))
            {
                _context.RegistrantPhone.Remove(phone);
            }

            _context.SaveChanges();
        }

        public async Task UpdatePhone(List<RegistrantPhone> phoneList)
        {
            foreach (var phone in phoneList.Where(phone => phone != null))
            {
                var originalPhone = _context.RegistrantPhone.FirstOrDefault(r => r.Id == phone.Id);
                if (originalPhone == null) continue;
                originalPhone.Phone = phone.Phone;
                originalPhone.PhoneType = phone.PhoneType;
                originalPhone.PhoneTypeId = phone.PhoneTypeId;
                originalPhone.CanText = phone.CanText;
            }

            _context.SaveChanges();
        }

        public async Task ModifyPhone(List<RegistrantPhone> phoneList)
        {
            throw new NotImplementedException();
        }
    }
}
