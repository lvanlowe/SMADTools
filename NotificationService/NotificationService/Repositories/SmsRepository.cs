using System;
using System.Collections.Generic;
using System.Text;
using NotificationService.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace NotificationService.Repositories
{
    public class SmsRepository : ISmsRepository
    {

        private string _accountSid;
        private string _authToken;
        private string _fromPhone;

        public SmsRepository(string accountSid, string authToken, string fromPhone)
        {
            TwilioClient.Init(accountSid, authToken);
            _fromPhone = fromPhone;
        }

        public void SendSms(string toPhone, string body)
        {
            var message = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber(_fromPhone),
                to: new Twilio.Types.PhoneNumber(toPhone)
            );
        }
    }
}
