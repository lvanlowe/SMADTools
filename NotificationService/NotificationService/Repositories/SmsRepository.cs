using System;
using System.Collections.Generic;
using System.Text;
using NotificationService.Interfaces;

namespace NotificationService.Repositories
{
    public class SmsRepository : ISmsRepository
    {

        public SmsRepository(string accountSid, string authToken, string fromPhone)
        {

        }

        public void SendSms(string toPhone, string body)
        {
            throw new NotImplementedException();
        }
    }
}
