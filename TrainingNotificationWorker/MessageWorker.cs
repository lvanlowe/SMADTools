using InformationService.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceModels;

namespace TrainingNotificationWorker
{
    public abstract class MessageWorker
    {
        public List<SportEmails> GetAddressesForLocation(int? locationId, List<SportEmails> addresses)
        {
            if (locationId == null || locationId == 0)
            {
                return addresses;
            }
            var locationAddresses = addresses.Where(e => e.ProgramId == locationId).ToList();
            return locationAddresses;
        }

        public List<SportEmails> GetAddressesForCategory(int? categoryId, List<SportEmails> addresses)
        {
            if (categoryId == null || categoryId == 0)
            {
                return addresses;
            }
            var categoryAddresses = addresses.Where(e => e.SportTypeId == categoryId).ToList();
            return categoryAddresses;
        }

        public List<SportEmails> GetAddressesForTeam(int? teamId, List<SportEmails> addresses)
        {
            if (teamId == null || teamId == 0)
            {
                return addresses;
            }
            var teamAddresses = addresses.Where(e => e.TeamId == teamId).ToList();
            return teamAddresses;
        }

        public List<SportEmails> GetAddressesForSelected(bool? isSelected, List<SportEmails> addresses)
        {
            if (isSelected.HasValue && isSelected.Value)
            {
                return addresses.Where(e => e.Selected == true || e.IsVolunteer == true).ToList();
            }
            return addresses;
        }

        public List<SportEmails> GetAddressesForVolunteers(bool? isVolunteer, List<SportEmails> addresses)
        {
            if (isVolunteer.HasValue && isVolunteer.Value)
            {
                return addresses.Where(e => e.IsVolunteer == true).ToList();
            }

            return addresses;
        }

        public List<SportEmails> GetAddresses(IMessageDto message, List<SportEmails> sportEmailList)
        {
            var locationEmailList = GetAddressesForLocation(Convert.ToInt32(message.ProgramId), sportEmailList);
            var categoryEmailList = GetAddressesForCategory(Convert.ToInt32(message.SportTypeId), locationEmailList);
            var teamEmailList = GetAddressesForTeam(Convert.ToInt32(message.TeamId), categoryEmailList);
            var selectedEmailList = GetAddressesForSelected(message.Selected, teamEmailList);
            var volEmailList = GetAddressesForVolunteers(message.IsVolunteer, selectedEmailList);
            return volEmailList;
        }
    }
}
