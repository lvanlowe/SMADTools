using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface ICalendarRepository
    {
        Task<List<PracticeCalendarItems>> GetPracticesForLocation(long programId, DateTime startDate);

    }
}
