using System;
using System.Net;
using System.Net.Mail;

namespace BankOfSuccess.Data
{
    public class EmailNotification : INotification
    {
        public void Update(Transaction transaction)
        {


            //mail.From = new MailAddress("bankofsuccess2022@gmail.com");
            //mail.To.Add(notification.Email);
            //mail.Subject = "Bank Account related mail";
            //mail.Body = body;
            //mail.IsBodyHtml = true;
            //mail.Attachments.Add(new Attachment("C:\\file.zip"));


            //using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 2525))
            //{
            //    smtp.EnableSsl = true;
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = new NetworkCredential("banksucesss123@gmail.com", "rylonbqxihhbyxro");
            //    smtp.Send("banksucesss123@gmail.com", "vishalsajjan0@gmail.com", "Sucess", "Transfer");
            //}

            MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "banksucesss123@gmail.com";
            string password = "rylonbqxihhbyxro";
            string toEmail = "vishalsajjan0@gmail.com";
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = "Sucessfull Transaction";
            message.Body = "Your Transaction is Sucessfull";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, password);




                smtpClient.Send(message.From.ToString(), message.To.ToString(), message.Subject, message.Body);
                System.Net.Mail.Attachment attachment;
                attachment = new Attachment("C:/Users/Admin/Desktop/BankOfSuccess-log-notify/BankOfSuccess.Data/abcd.txt");
                message.Attachments.Add(attachment);
            }



        }
    }
}

