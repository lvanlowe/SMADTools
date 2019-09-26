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
            //_context.SaveChanges();

        }

        public async Task RemovePhone(List<RegistrantPhone> phoneList)
        {
            foreach (var phone in phoneList.Where(phone => phone != null))
            {
                _context.RegistrantPhone.Remove(phone);
            }

            //_context.SaveChanges();
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

            //_context.SaveChanges();
        }

        public async Task ModifyPhone(List<RegistrantPhone> phoneList)
        {
            var registrant = await _context.Registrant
                .Where(r => r.Id == phoneList[0].RegistrantId).FirstOrDefaultAsync();
            List<RegistrantPhone> deletedPhone = new List<RegistrantPhone>();
            var newPhoneList = phoneList.Where(p => p.Id == 0).ToList();
            var oldPhoneList = phoneList.Where(p => p.Id != 0).ToList();
            foreach (var phone in registrant.RegistrantPhone)
            {
                var exsistingPhone = phoneList.Where(p => p.Id == phone.Id).FirstOrDefault();
                if (exsistingPhone == null)
                {
                    deletedPhone.Add(phone);
                }
            }
            await UpdatePhone(oldPhoneList);
            await RemovePhone(deletedPhone);
            await AddPhone(newPhoneList);

        }

        public async Task AddEmail(List<RegistrantEmail> emailList)
        {
            var registrant = await _context.Registrant
                .Where(r => r.Id == emailList[0].RegistrantId).FirstOrDefaultAsync();
            foreach (var email in emailList)
            {
                registrant?.RegistrantEmail.Add(email);
            }
            //_context.SaveChanges();
        }

        public async Task RemoveEmail(List<RegistrantEmail> emailList)
        {
            foreach (var email in emailList.Where(email => email != null))
            {
                _context.RegistrantEmail.Remove(email);
            }

            //_context.SaveChanges();
        }

        public async Task UpdateEmail(List<RegistrantEmail> emailList)
        {
            foreach (var email in emailList.Where(email => email != null))
            {
                var originalEmail = _context.RegistrantEmail.FirstOrDefault(r => r.Id == email.Id);
                if (originalEmail == null) continue;
                originalEmail.Email = email.Email;
            }

            //_context.SaveChanges();
        }

        public async Task UpdateRegistrant(Registrant registrant)
        {
            var originalRegistrant = _context.Registrant.FirstOrDefaultAsync(r => r.Id == registrant.Id);
            if (originalRegistrant != null)
            {
                originalRegistrant.Result.Selected = registrant.Selected;
                originalRegistrant.Result.SportTypeId = registrant.SportTypeId;
                originalRegistrant.Result.Size = registrant.Size;
                originalRegistrant.Result.TeamId = registrant.TeamId;

                await ModifyPhone(registrant.RegistrantPhone.ToList());
                await ModifyEmail(registrant.RegistrantEmail.ToList());
                await _context.SaveChangesAsync();
            }
        }

        public async Task ModifyEmail(List<RegistrantEmail> emailList)
        {
            var registrant = await _context.Registrant
                .Where(r => r.Id == emailList[0].RegistrantId).FirstOrDefaultAsync();
            List<RegistrantEmail> deletedEmail = new List<RegistrantEmail>();
            var newEmailList = emailList.Where(p => p.Id == 0).ToList();
            var oldEmailList = emailList.Where(p => p.Id != 0).ToList();
            foreach (var phone in registrant.RegistrantEmail)
            {
                var exsistingEmail = emailList.Where(p => p.Id == phone.Id).FirstOrDefault();
                if (exsistingEmail == null)
                {
                    deletedEmail.Add(phone);
                }
            }
            await UpdateEmail(oldEmailList);
            await RemoveEmail(deletedEmail);
            await AddEmail(newEmailList);

        }
    }
}
