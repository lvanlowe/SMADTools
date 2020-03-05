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

        private IReferenceRepository _referenceRepository;

        public ReferenceWorker(IReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;
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
            var message = "<p>Hi {{TeamName}} Athletes, Athletes family, Coaches and Volunteers:<br /></p><p style=\"margin-left:30px;\"><strong>Everyone should be there at {{StartTime}}.</strong><br /><br />The first game is at {{FirstGame}}.<br /><br />The second game is at {{SecondGame}}.<br /><br /><br />The competition will be at  {{LocationName}} ({{LocationAddress}}, {{LocationCity}}, {{LocationState}} {{LocationZip}} )<br /><br /><br /><p style=\"margin-left:30px;\">You must return your uniform to your coach RIGHT AFTER SATURDAY’S COMPETITION. Remember bring a change of clothes. Failure to return your uniform may reflect on your athlete being selected for future Basketball Championships. There will also be a charge of $30 for jersey and $20 for shorts for all uniforms not returned at that time.<br /><br /><em>Remember no food or drinks are allowed in the Gymnasiums, only water.</em><br /><br /><br />Let me know if you have any questions.<br /><br /><br /></p><p>Van&nbsp;<br /><br /><br />L. A. Van Lowe&nbsp;<br /><br />Basketball Coordinator&nbsp;<br /><br /><br />P. O. 1073&nbsp;<br /><br />Woodbridge, VA 22195-1073&nbsp;<br /><br />Fax: (866) 558-8780&nbsp;<br /></p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            string secondGame = details.SecondGameTime;
            if (details.OrGameTime != null)
            {
                secondGame +=  " or " + details.OrGameTime;
            }
            
            message = message.Replace("{{TeamName}}", details.TeamName);
            message = message.Replace("{{StartTime}}", details.StartTime);
            message = message.Replace("{{FirstGame}}", details.FirstGameTime);
            message = message.Replace("{{SecondGame}}", secondGame);
            message = message.Replace("{{LocationName}}", details.LocationName);
            message = message.Replace("{{LocationAddress}}", details.LocationAddress);
            message = message.Replace("{{LocationCity}}", details.LocationCity);
            message = message.Replace("{{LocationState}}", details.LocationState);
            message = message.Replace("{{LocationZip}}", details.LocationZip);
            return message;
        }

        public TournamentDetails PrepareChampionshipDetails(TournamentTeamDto dto)
        {
            var location = _referenceRepository.GetLocationByLocationId(dto.LocationId);
            var details = new TournamentDetails
            {
                FirstGameTime = _referenceRepository.GetTimeByTimeId(dto.Game1TimeId).Result.TimeHour,
                LocationAddress = location.Result.Street,
                LocationCity = location.Result.City,
                LocationName = location.Result.Name,
                LocationState = location.Result.State,
                LocationZip = location.Result.Zip,
                TeamName = _referenceRepository.GetTeamByTeamId(dto.TeamId).Result.Name,
                SecondGameTime = _referenceRepository.GetTimeByTimeId(dto.Game2TimeId).Result.TimeHour,
                StartTime = _referenceRepository.GetTimeByTimeId(dto.Game1TimeId - 1).Result.TimeHour,
            };
            if (dto.OrTimeId != 0)
            {
                details.OrGameTime = _referenceRepository.GetTimeByTimeId(dto.OrTimeId).Result.TimeHour;
            }

            return details;
        }

        public string PrepareChampionshipText(TournamentDetails details)
        {
            string message = "{{teamName}} competition will be at {{locationName}}, everyone should arrive at {{arrivalTime}} ";
            message = message
                .Replace("{{teamName}}", details.TeamName)
                .Replace("{{locationName}}", details.LocationName)
                .Replace("{{arrivalTime}}", details.StartTime);

            return message;
        }

        public CoachTextDto ChampionshipTextPreparation(TournamentTeamDto dto)
        {
            var details = PrepareChampionshipDetails(dto);

            CoachTextDto textdto = new CoachTextDto
            {
                SportId = 4,
                TeamId = dto.TeamId,
                Selected = true,
                Message = PrepareChampionshipText(details)
            };


            return textdto;
        }

        public CoachEmailDto ChampionshipEmailPreparation(TournamentTeamDto dto)
        {
            var details = PrepareChampionshipDetails(dto);

            CoachEmailDto emailDto = new CoachEmailDto
            {
                SportId = 4,
                TeamId = dto.TeamId,
                Selected = true,
                From = "basketballCoordinator@pwsova.org",
                Subject = "Basketball Tournament Information",
                HtmlContent = PrepareChampionshipEmail(details)
            };

            return emailDto;
        }
    }
}
