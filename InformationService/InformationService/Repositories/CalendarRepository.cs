using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationService.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private PwsodbContext _context;

        public CalendarRepository(PwsodbContext context)
        {
            _context = context;
        }

        public async Task<List<PracticeCalendarItems>> GetPracticesForLocation(long programId, DateTime startDate)
        {
            var practice = await _context.PracticeCalendarItems
                .Include(p => p.CalendarItem)
                .Where(p => p.ProgramId == programId && p.CalendarItem.ItemDate.Date >= startDate.Date)
                .ToListAsync();
            return practice;
        }
    }
}
