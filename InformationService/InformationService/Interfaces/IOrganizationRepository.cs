using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<List<Athletes>> GetAllAthletes();
        Task<List<Athletes>> FindAthletesByFirstLetter(char letter);
        Task<Athletes> FindAthleteByName(string firstName, string lastName);
    }
}
