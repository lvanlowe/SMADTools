using System;
using System.Collections.Generic;
using System.Linq;
using InformationService.Interfaces;
using InformationService.Models;
using InformationService.Repositories;
using InterfaceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using TrainingManagingWorker;
using Xunit;

namespace TrainingManagingWorkerTest
{
    public class CalendarWorkerTest
    {

        private CalendarWorker _worker;

        public CalendarWorkerTest()
        {
            _worker = new CalendarWorker();
        }

        [Fact]
        public void BuildCancelEmailSubject_When_executed_return_subject()

        {
            SportLocationDto sport = new SportLocationDto
            {
                ProgramId = 10,
                SportId = 4,
                Email = "superman@dc.com",
                SportName = "Basketball",
                ProgramName = "Woodbridge",
            };

            CancelEventDto cancelEvent = new CancelEventDto
            {
                CancelNote = "practice is cancel due to snow",
                CancelReason = "weather",
                PracticeId = 67,
            };

            PracticeCalendarItems practice = new PracticeCalendarItems
            {
                Id = 67,
                CalendarItem = new CalendarItems { Id = 89, ItemDate = DateTime.Now.AddDays(7)}
            };

            string expected = sport.SportName + " " + sport.ProgramName + " " +
                              DateTime.Now.AddDays(7).ToShortDateString() + " practice canceled " +
                              cancelEvent.CancelReason;

            string actual = _worker.BuildCancelEmailSubject(sport, cancelEvent, practice);
            Assert.Equal(expected, actual);

        }

    }
}
