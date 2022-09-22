using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BankOfSuccessCS.Models
{
    public class SMSNotification : INotification
    {
        public void Update(string loc)
        {
            string accountSid = ConfigurationManager.AppSettings["AccountSid"];
            string authToken = ConfigurationManager.AppSettings["AuthToken"];
            TwilioClient.Init(accountSid, authToken);
            string ph = loc.Split(',')[1];
            string msgbody = File.ReadAllText(ConfigurationManager.AppSettings["StatementLogPath"]);

            Task.Run(() =>
            {
                var message = MessageResource.Create(
                     body: msgbody,
                     from: new Twilio.Types.PhoneNumber(ConfigurationManager.AppSettings["SenderNo"]),
                     to: new Twilio.Types.PhoneNumber(ph)
                 );;
            });
        }

        public void Update(Transaction t)
        {
            string accountSid = ConfigurationManager.AppSettings["AccountSid"];
            string authToken = ConfigurationManager.AppSettings["AuthToken"];
            TwilioClient.Init(accountSid, authToken);

            Task.Run(() =>
            {
                var message = MessageResource.Create(
                     body: t.ToString(),
                     from: new Twilio.Types.PhoneNumber(ConfigurationManager.AppSettings["SenderNo"]),
                     to: new Twilio.Types.PhoneNumber(t.Acc.PhoneNo)
                 ) ;
            });
        }
    }
}
