﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.Interfaces;
using InformationService.Models;
using InterfaceModels;

namespace TrainingManagingWorker
{
    public class CalendarWorker
    {
        private ICalendarRepository _calendarRepository;
        private IReferenceRepository _referenceRepository;
        public CalendarWorker(ICalendarRepository calendarRepository, IReferenceRepository referenceRepository)
        {
            _calendarRepository = calendarRepository;
            _referenceRepository = referenceRepository;
        }

        public string BuildCancelEmailSubject(SportLocationDto sport, CancelEventDto cancelEvent, PracticeCalendarItems practice)
        {
            string message = sport.SportName + " " + sport.ProgramName;
            message += " " + practice.CalendarItem.ItemDate.ToShortDateString();
            message += " practice canceled " + cancelEvent.CancelReason;
            return message;
        }

        public async Task<CoachEmailDto> ProcessEventCancelation(CancelEventDto cancelEvent)
        {
            var practice = await _calendarRepository.GetPracticeEvent(cancelEvent.PracticeId);
            var location = await _referenceRepository.GetLocationByProgramId(practice.ProgramId);
            _calendarRepository.CancelEvent(practice.CalendarItem.Id, cancelEvent.CancelReason,
                cancelEvent.CancelNote);
            SportLocationDto sport = new SportLocationDto
            {
                SportName = location.SportNavigation.Name,
                ProgramName = location.Name,
            };
            var dto = new CoachEmailDto
            {
                From = location.SportNavigation.Email,
                SportId = location.SportNavigation.Id,
                ProgramId = location.Id,
                HtmlContent = cancelEvent.CancelNote,
                Subject = BuildCancelEmailSubject(sport, cancelEvent, practice),
            };
            return dto;
        }
    }
}
