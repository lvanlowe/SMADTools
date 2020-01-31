using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InformationService.Models;
using InformationService.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InformationServiceTest.RepositoriesTest
{
    public class CalendarRepositoryTest
    {
        private PwsodbContext _context;

        public CalendarRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PwsodbContext>().
                UseInMemoryDatabase(databaseName: "CalendarRepository")
                .Options;
            _context = new PwsodbContext(options);

        }

        private void InitializeCalendar()
        {
            var practice = _context.PracticeCalendarItems.ToListAsync();
            _context.RemoveRange(practice.Result);
            var calendar = _context.CalendarItems.ToListAsync();
            _context.RemoveRange(calendar.Result);
            var athletes = _context.RegisteredAthlete.ToListAsync();
            _context.SaveChanges();
        }

        private void LoadCalendar()
        {
            var calendar1 = new CalendarItems() { Id = 1, ItemDate = DateTime.Now.AddDays(-7), };
            var calendar2 = new CalendarItems() { Id = 2, ItemDate = DateTime.Now.AddDays(-3), };
            var calendar3 = new CalendarItems() { Id = 3, ItemDate = DateTime.Now.AddDays(-1), };
            var calendar4 = new CalendarItems() { Id = 4, ItemDate = DateTime.Now.AddDays(0), };
            var calendar5 = new CalendarItems() { Id = 5, ItemDate = DateTime.Now.AddDays(4), };
            var calendar6 = new CalendarItems() { Id = 6, ItemDate = DateTime.Now.AddDays(6), };
            var calendar7 = new CalendarItems() { Id = 7, ItemDate = DateTime.Now.AddDays(7), };
            var calendar8 = new CalendarItems() { Id = 8, ItemDate = DateTime.Now.AddDays(11), };
            var calendar9 = new CalendarItems() { Id = 9, ItemDate = DateTime.Now.AddDays(13), };
            var calendar10 = new CalendarItems() { Id = 10, ItemDate = DateTime.Now.AddDays(14), };

            calendar1.PracticeCalendarItems.Add(new PracticeCalendarItems { Id = 1, CalendarItemId = 1, ProgramId = 1 });
            calendar2.PracticeCalendarItems.Add(new PracticeCalendarItems { Id = 2, CalendarItemId = 2, ProgramId = 2 });
            calendar4.PracticeCalendarItems.Add(new PracticeCalendarItems { Id = 3, CalendarItemId = 4, ProgramId = 1 });
            calendar5.PracticeCalendarItems.Add(new PracticeCalendarItems { Id = 4, CalendarItemId = 5, ProgramId = 2 });
            calendar7.PracticeCalendarItems.Add(new PracticeCalendarItems { Id = 5, CalendarItemId = 7, ProgramId = 1 });
            calendar8.PracticeCalendarItems.Add(new PracticeCalendarItems { Id = 6, CalendarItemId = 8, ProgramId = 2 });
            calendar10.PracticeCalendarItems.Add(new PracticeCalendarItems { Id = 7, CalendarItemId = 10, ProgramId = 1 });

            _context.CalendarItems.Add(calendar1);
            _context.CalendarItems.Add(calendar2);
            _context.CalendarItems.Add(calendar3);
            _context.CalendarItems.Add(calendar4);
            _context.CalendarItems.Add(calendar5);
            _context.CalendarItems.Add(calendar6);
            _context.CalendarItems.Add(calendar7);
            _context.CalendarItems.Add(calendar8);
            _context.CalendarItems.Add(calendar9);
            _context.CalendarItems.Add(calendar10);

            _context.SaveChanges();

        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 2)]
        public void GetPracticesForLocation_When_executed_create_list_of_Practice_Events(int locationId, int expected)

        {
            // Insert seed data into the database using one instance of the context
            InitializeCalendar();
            LoadCalendar();

            DateTime startDate = DateTime.Now;
            var repository = new CalendarRepository(_context);
            var actual = repository.GetPracticesForLocation(locationId, startDate);

            Assert.Equal(expected, actual.Result.Count);

        }

        [Fact]
        public void CancelEvent_When_executed_set_cancel_reason()

        {
            // Insert seed data into the database using one instance of the context
            InitializeCalendar();
            LoadCalendar();

            var reason = "snow";
            var calendarId = 1;
            var note = "The schools are closed";
            var repository = new CalendarRepository(_context);
            repository.CancelEvent(calendarId, reason, note);

            var calendar = _context.CalendarItems.Where(c => c.Id == calendarId).FirstOrDefaultAsync();

            Assert.Equal(reason, calendar.Result.CancelReason);
            Assert.Equal(note, calendar.Result.Comments);

        }

        [Theory]
        [InlineData(5, 1, 7, 7)]
        [InlineData(4, 2, 5, 4)]
        public void GetPracticeEvent_When_executed_return_Practice_Event(int practiceId, int locationId, int calendarId, int days)

        {
            // Insert seed data into the database using one instance of the context
            InitializeCalendar();
            LoadCalendar();

            DateTime startDate = DateTime.Now;
            var repository = new CalendarRepository(_context);
            var actual = repository.GetPracticeEvent(practiceId);

            Assert.Equal(locationId, actual.Result.ProgramId);
            Assert.Equal(calendarId, actual.Result.CalendarItemId);
            Assert.Equal(startDate.AddDays(days).ToShortDateString(), actual.Result.CalendarItem.ItemDate.ToShortDateString());

        }
    }


}
