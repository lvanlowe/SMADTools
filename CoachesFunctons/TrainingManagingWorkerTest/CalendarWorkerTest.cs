﻿using System;
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
        private Mock<ICalendarRepository> _mockCalendarRepository;
        private Mock<IReferenceRepository> _mockReferenceRepository;


        public CalendarWorkerTest()
        {
            _mockCalendarRepository = new Mock<ICalendarRepository>();
            _mockReferenceRepository = new Mock<IReferenceRepository>();

            _worker = new CalendarWorker(_mockCalendarRepository.Object, _mockReferenceRepository.Object);
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

        [Fact]
        public void ProcessEventCancelation_When_executed_verify_event()

        {

            CancelEventDto cancelEvent = new CancelEventDto
            {
                CancelNote = "practice is cancel due to snow",
                CancelReason = "weather",
                PracticeId = 67,
            };

            CalendarItems calendarItem = new CalendarItems
            {
                Id = 89,
                ItemDate = DateTime.Now.AddDays(7),
            };

            PracticeCalendarItems practiceCalendarItem = new PracticeCalendarItems
            {
                Id = 67,
                CalendarItem = calendarItem,
                ProgramId = 10,
            };

            SportLocationDto sport = new SportLocationDto
            {
                ProgramId = 10,
                SportId = 4,
                Email = "superman@dc.com",
                SportName = "Basketball",
                ProgramName = "Woodbridge",
            };

            PracticeCalendarItems practice = new PracticeCalendarItems
            {
                Id = 67,
                CalendarItem = new CalendarItems { Id = 89, ItemDate = DateTime.Now.AddDays(7) }
            };

            _mockCalendarRepository.Setup(repository => repository.GetPracticeEvent(cancelEvent.PracticeId)).ReturnsAsync(practiceCalendarItem);

            string expected = sport.SportName + " " + sport.ProgramName + " " +
                              DateTime.Now.AddDays(7).ToShortDateString() + " practice canceled " +
                              cancelEvent.CancelReason;

            var actual = _worker.ProcessEventCancelation(cancelEvent);
            _mockCalendarRepository.Verify(mock => mock.GetPracticeEvent(It.IsAny<long>()), Times.Once);

        }


        [Fact]
        public void ProcessEventCancelation_When_executed_verify_sport()

        {

            CancelEventDto cancelEvent = new CancelEventDto
            {
                CancelNote = "practice is cancel due to snow",
                CancelReason = "weather",
                PracticeId = 67,
            };

            CalendarItems calendarItem = new CalendarItems
            {
                Id = 89,
                ItemDate = DateTime.Now.AddDays(7),
            };

            PracticeCalendarItems practiceCalendarItem = new PracticeCalendarItems
            {
                Id = 67,
                CalendarItem = calendarItem,
                ProgramId = 10,
            };

            Sports sport = new Sports
            {
                Id = 4,
                Name = "Basketball",
                Email = "superman@dc.com"
            };

            Programs program = new Programs
            {
                Id = 10,
                Name = "Woodbridge"
            };

            sport.Programs.Add(program);

            //SportLocationDto sport = new SportLocationDto
            //{
            //    ProgramId = 10,
            //    SportId = 4,
            //    Email = "superman@dc.com",
            //    SportName = "Basketball",
            //    ProgramName = "Woodbridge",
            //};

            PracticeCalendarItems practice = new PracticeCalendarItems
            {
                Id = 67,
                CalendarItem = new CalendarItems { Id = 89, ItemDate = DateTime.Now.AddDays(7) }
            };

            _mockCalendarRepository.Setup(repository => repository.GetPracticeEvent(cancelEvent.PracticeId)).ReturnsAsync(practiceCalendarItem);
            _mockReferenceRepository.Setup(repository => repository.GetLocationByProgramId(practiceCalendarItem.ProgramId)).ReturnsAsync(program);

            //string expected = sport.SportName + " " + sport.ProgramName + " " +
            //                  DateTime.Now.AddDays(7).ToShortDateString() + " practice canceled " +
            //                  cancelEvent.CancelReason;

            var actual = _worker.ProcessEventCancelation(cancelEvent);
            _mockReferenceRepository.Verify(mock => mock.GetLocationByProgramId(It.IsAny<long>()), Times.Once);

        }

        [Fact]
        public void ProcessEventCancelation_When_executed_verify_event_canceled()

        {

            CancelEventDto cancelEvent = new CancelEventDto
            {
                CancelNote = "practice is cancel due to snow",
                CancelReason = "weather",
                PracticeId = 67,
            };

            CalendarItems calendarItem = new CalendarItems
            {
                Id = 89,
                ItemDate = DateTime.Now.AddDays(7),
            };

            PracticeCalendarItems practiceCalendarItem = new PracticeCalendarItems
            {
                Id = 67,
                CalendarItem = calendarItem,
                ProgramId = 10,
            };

            Sports sport = new Sports
            {
                Id = 4,
                Name = "Basketball",
                Email = "superman@dc.com"
            };

            Programs program = new Programs
            {
                Id = 10,
                Name = "Woodbridge"
            };

            sport.Programs.Add(program);

            //SportLocationDto sport = new SportLocationDto
            //{
            //    ProgramId = 10,
            //    SportId = 4,
            //    Email = "superman@dc.com",
            //    SportName = "Basketball",
            //    ProgramName = "Woodbridge",
            //};

            PracticeCalendarItems practice = new PracticeCalendarItems
            {
                Id = 67,
                CalendarItem = new CalendarItems { Id = 89, ItemDate = DateTime.Now.AddDays(7) }
            };

            _mockCalendarRepository.Setup(repository => repository.GetPracticeEvent(cancelEvent.PracticeId)).ReturnsAsync(practiceCalendarItem);
            _mockReferenceRepository.Setup(repository => repository.GetLocationByProgramId(practiceCalendarItem.ProgramId)).ReturnsAsync(program);

            //string expected = sport.SportName + " " + sport.ProgramName + " " +
            //                  DateTime.Now.AddDays(7).ToShortDateString() + " practice canceled " +
            //                  cancelEvent.CancelReason;

            var actual = _worker.ProcessEventCancelation(cancelEvent);
            _mockCalendarRepository.Verify(mock => mock.CancelEvent(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockCalendarRepository.Verify(mock => mock.CancelEvent(practice.CalendarItem.Id, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockCalendarRepository.Verify(mock => mock.CancelEvent(It.IsAny<long>(), cancelEvent.CancelReason, It.IsAny<string>()), Times.Once);
            _mockCalendarRepository.Verify(mock => mock.CancelEvent(It.IsAny<long>(), It.IsAny<string>(), cancelEvent.CancelNote), Times.Once);

        }

        [Fact]
        public void ProcessEventCancelation_When_executed_creat_email_dto()

        {

            CancelEventDto cancelEvent = new CancelEventDto
            {
                CancelNote = "practice is cancel due to snow",
                CancelReason = "weather",
                PracticeId = 67,
            };

            CalendarItems calendarItem = new CalendarItems
            {
                Id = 89,
                ItemDate = DateTime.Now.AddDays(7),
            };

            PracticeCalendarItems practiceCalendarItem = new PracticeCalendarItems
            {
                Id = 67,
                CalendarItem = calendarItem,
                ProgramId = 10,
            };

            Sports sport = new Sports
            {
                Id = 4,
                Name = "Basketball",
                Email = "superman@dc.com"
            };

            Programs program = new Programs
            {
                Id = 10,
                Name = "Woodbridge",
                SportNavigation = sport
            };

            sport.Programs.Add(program);

            //SportLocationDto sport = new SportLocationDto
            //{
            //    ProgramId = 10,
            //    SportId = 4,
            //    Email = "superman@dc.com",
            //    SportName = "Basketball",
            //    ProgramName = "Woodbridge",
            //};

            PracticeCalendarItems practice = new PracticeCalendarItems
            {
                Id = 67,
                CalendarItem = new CalendarItems { Id = 89, ItemDate = DateTime.Now.AddDays(7) }
            };

            _mockCalendarRepository.Setup(repository => repository.GetPracticeEvent(cancelEvent.PracticeId)).ReturnsAsync(practiceCalendarItem);
            _mockReferenceRepository.Setup(repository => repository.GetLocationByProgramId(practiceCalendarItem.ProgramId)).ReturnsAsync(program);

            string expected = sport.Name + " " + program.Name + " " +
                              DateTime.Now.AddDays(7).ToShortDateString() + " practice canceled " +
                              cancelEvent.CancelReason;

            var actual = _worker.ProcessEventCancelation(cancelEvent);
            Assert.Equal(sport.Email, actual.Result.From);
            Assert.Equal(sport.Id, actual.Result.SportId);
            Assert.Equal(program.Id, actual.Result.ProgramId);
            Assert.Equal(cancelEvent.CancelNote, actual.Result.HtmlContent);
            Assert.Equal(expected, actual.Result.Subject);


        }


        [Fact]
        public void ProcessEventCancelation_without_mock()

        {

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var trainingConnectionString = config["TrainingDatabase"];
            var trainingOptions = new DbContextOptionsBuilder<PwsodbContext>().UseSqlServer(trainingConnectionString).Options;

            PwsodbContext trainingContext = new PwsodbContext(trainingOptions);
            ICalendarRepository calendarRepository = new CalendarRepository(trainingContext);
            IReferenceRepository referenceRepository = new ReferenceRepository(trainingContext);
            CalendarWorker worker = new CalendarWorker(calendarRepository, referenceRepository);

            CancelEventDto cancelEvent = new CancelEventDto
            {
                CancelNote = "practice is cancel due to snow",
                CancelReason = "weather",
                PracticeId = 41598,
            };


            //var actual = worker.ProcessEventCancelation(cancelEvent);
            //Assert.Equal("AreaCoordinator@pwsova.org", actual.Result.From);

        }

    }
}
