using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.DataModels;

namespace InformationService.Interfaces
{
    public interface ITrainingRepository
    {
        Task<List<SportEmails>> GetEmailsBySport(int sportId);
    }
}
