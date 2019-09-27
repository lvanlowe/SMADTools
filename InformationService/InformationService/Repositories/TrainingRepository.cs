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

        public void AddPhone(List<RegistrantPhone> phoneList, List<RegistrantPhone> originalPhoneList)
        {
            foreach (var phone in phoneList)
            {
                originalPhoneList.Add(phone);
            }
            //_context.SaveChanges();

        }

        public void RemovePhone(List<RegistrantPhone> phoneList, List<RegistrantPhone> originalPhoneList)
        {
            foreach (var phone in phoneList)
            {
                originalPhoneList.RemoveAll(p => p.Id == phone.Id);
            }

            //_context.SaveChanges();
        }

        public void UpdatePhone(List<RegistrantPhone> phoneList, List<RegistrantPhone> originalPhoneList)
        {
            foreach (var phone in phoneList.Where(phone => phone != null))
            {
                var originalPhone = originalPhoneList.FirstOrDefault(r => r.Id == phone.Id);
                if (originalPhone == null) continue;
                originalPhone.Phone = phone.Phone;
                originalPhone.PhoneType = phone.PhoneType;
                originalPhone.PhoneTypeId = phone.PhoneTypeId;
                originalPhone.CanText = phone.CanText;
            }

            //_context.SaveChanges();
        }

        public void ModifyPhone(List<RegistrantPhone> phoneList, List<RegistrantPhone> originalPhoneList)
        {
            var newPhoneList = phoneList.Where(p => p.Id == 0).ToList();
            var oldPhoneList = phoneList.Where(p => p.Id != 0).ToList();
            var deletedPhone = (from phone in originalPhoneList let existingPhone = phoneList.FirstOrDefault(p => p.Id == phone.Id) where existingPhone == null select phone).ToList();
            UpdatePhone(oldPhoneList, originalPhoneList);
            RemovePhone(deletedPhone, originalPhoneList);
            AddPhone(newPhoneList, originalPhoneList);
        }

        public void AddEmail(List<RegistrantEmail> emailList, List<RegistrantEmail> originalEmailList)
        {
            //var registrant = await _context.Registrant
            //    .Where(r => r.Id == emailList[0].RegistrantId).FirstOrDefaultAsync();
            foreach (var email in emailList)
            {
                originalEmailList.Add(email);
            }
            //_context.SaveChanges();
        }

        public void RemoveEmail(List<RegistrantEmail> emailList, List<RegistrantEmail> originalEmailList)
        {
            foreach (var email in emailList.Where(email => email != null))
            {
                originalEmailList.RemoveAll(e => e.Id == email.Id);

                _context.RegistrantEmail.Remove(email);
            }

            //_context.SaveChanges();
        }

        public void UpdateEmail(List<RegistrantEmail> emailList, List<RegistrantEmail> originalEmailList)
        {
            foreach (var email in emailList.Where(email => email != null))
            {
                var originalEmail = originalEmailList.FirstOrDefault(r => r.Id == email.Id);
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

                ModifyPhone(registrant.RegistrantPhone.ToList(), originalRegistrant.Result.RegistrantPhone.ToList());
                ModifyEmail(registrant.RegistrantEmail.ToList(), originalRegistrant.Result.RegistrantEmail.ToList());
                await _context.SaveChangesAsync();
            }
        }

        public void ModifyEmail(List<RegistrantEmail> emailList, List<RegistrantEmail> originalEmailList)
        {
            //var registrant = await _context.Registrant
            //    .Where(r => r.Id == emailList[0].RegistrantId).FirstOrDefaultAsync();
            List<RegistrantEmail> deletedEmail = new List<RegistrantEmail>();
            var newEmailList = emailList.Where(p => p.Id == 0).ToList();
            var oldEmailList = emailList.Where(p => p.Id != 0).ToList();
            foreach (var email in originalEmailList)
            {
                var exsistingEmail = emailList.Where(p => p.Id == email.Id).FirstOrDefault();
                if (exsistingEmail == null)
                {
                    deletedEmail.Add(email);
                }
            }
            UpdateEmail(oldEmailList, originalEmailList);
            RemoveEmail(deletedEmail, originalEmailList);
            AddEmail(newEmailList, originalEmailList);

        }
    }
}
