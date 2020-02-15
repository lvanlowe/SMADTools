using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IReferenceRepository
    {
        Task<List<Sports>> GetAllSports();
        Task<List<Programs>> GetLocationBySport(int sportId);
        Task<List<SportTypes>> GetCategoryBySport(int sportId);
        Task<List<Teams>> GetTeamBySport(int sportId);
        Task<List<Sports>> GetSports();
        Task<Programs> GetLocationByProgramId(long programId);
        Task<Location> GetLocationByLocationId(long locationId);
        Task<CalendarTimes> GetTimeByTimeId(long timeId);
    }
}
