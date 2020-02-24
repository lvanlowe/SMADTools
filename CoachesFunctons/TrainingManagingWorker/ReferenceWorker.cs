using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Interfaces;
using InformationService.Models;
using InterfaceModels;

namespace TrainingManagingWorker
{
    public class ReferenceWorker
    {

        private IReferenceRepository _referenceRepository_;

        public ReferenceWorker(IReferenceRepository referenceRepository)
        {
            _referenceRepository_ = referenceRepository;
        }

        public SportLocationDto PrepareSportsDataForClient(Programs location)
        {
            SportLocationDto dto = new SportLocationDto
            {
                Email = location.SportNavigation.Email,
                SportName = location.SportNavigation.Name,
                SportId = location.SportNavigation.Id,
                ProgramId = location.Id,
                ProgramName = location.Name
            };
            return dto;
        }

        public string PrepareChampionshipText(string teamName, string locationName, string arrivalTime)
        {
            string message = "{{teamName}} completion will be at {{locationName}}, everyone should arrive at {{arrivalTime}} ";
            message = message
                .Replace("{{teamName}}", teamName)
                .Replace("{{locationName}}", locationName)
                .Replace("{{arrivalTime}}", arrivalTime);

            return message;
        }

        public class TournamentDetails
        {
            public string TeamName { get; set; }
            public string StartTime { get; set; }
            public string FirstGameTime { get; set; }
            public string SecondGameTime { get; set; }
            public string OrGameTime { get; set; }
            public string LocationName { get; set; }
            public string LocationAddress { get; set; }
            public string LocationCity { get; set; }
            public string LocationState { get; set; }
            public string LocationZip { get; set; }
        }

        public string PrepareChampionshipEmail(TournamentDetails details)
        {
            throw new NotImplementedException();
        }
    }
}
