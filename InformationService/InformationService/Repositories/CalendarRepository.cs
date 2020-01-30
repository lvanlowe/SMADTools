using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async void CancelEvent(long calendarId, string reason)
        {
            var calendar = await _context.CalendarItems.Where(c => c.Id == calendarId).FirstOrDefaultAsync();
            calendar.CancelReason = reason;
            await _context.SaveChangesAsync();
        }

        public async Task<PracticeCalendarItems> GetPracticeEvent(long practiceId)
        {
            var practice = await _context.PracticeCalendarItems
                .Include(p => p.CalendarItem)
                .Where(p => p.Id == practiceId)
                .FirstOrDefaultAsync();

            return practice;
        }
    }
}
