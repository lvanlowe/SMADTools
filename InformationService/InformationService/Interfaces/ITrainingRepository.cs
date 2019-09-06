﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.DataModels;
using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface ITrainingRepository
    {
        Task<List<SportEmails>> GetEmailsBySport(int sportId);
        Task<List<SportEmails>> GetPhonesBySport(int sportId);
        Task<List<Registrant>> GetRegistrantsBySport(int sportId);
        Task AddRegisteredAthlete(int registrantId, int athleteId);
    }
}
