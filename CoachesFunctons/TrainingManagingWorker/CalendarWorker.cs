using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Models;
using InterfaceModels;

namespace TrainingManagingWorker
{
    public class CalendarWorker
    {
        public string BuildCancelEmailSubject(SportLocationDto sport, CancelEventDto cancelEvent, PracticeCalendarItems practice)
        {
            string message = sport.SportName + " " + sport.ProgramName;
            message += " " + practice.CalendarItem.ItemDate.ToShortDateString();
            message += " practice canceled " + cancelEvent.CancelReason;
            return message;
        }
    }
}
