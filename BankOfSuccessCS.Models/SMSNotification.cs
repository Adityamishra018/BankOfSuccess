using System;
using System.Configuration;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BankOfSuccessCS.Models
{
    public class SMSNotification : INotification
    {
        public void Update(string message)
        {
            throw new NotImplementedException();

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

            Console.WriteLine("Sending Mail....");
        }
    }
}
