using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.Interfaces;
using InformationService.Models;

namespace InformationService.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private PwsodbContext _context;

        public CalendarRepository(PwsodbContext context)
        {
            _context = context;
        }

        public Task<List<PracticeCalendarItems>> GetPracticesForLocation(long programId, in DateTime startDate)
        {
            throw new NotImplementedException();
        }
    }
}
