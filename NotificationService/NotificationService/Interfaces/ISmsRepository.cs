using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Interfaces
{
    public interface ISmsRepository
    {
        void SendSms(string toPhone, string body);
    }
}
