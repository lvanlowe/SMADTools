using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InformationService.Models;

namespace InformationService.Interfaces
{
    public interface IRefRepository
    {
        Task<NotificationEntity> GetEventByName(string eventName);
    }
}
