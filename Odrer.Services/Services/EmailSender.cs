using Odrer.Core;
using Orders.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Odrer.Services.Services
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string email, string subject, Status status)
        {
            SmtpClient client= new SmtpClient("smtp.ethereal.email",587);
            client.EnableSsl = true;
            client.UseDefaultCredentials= false;
            client.Credentials = new NetworkCredential("aboelnasrl.ob@gmail.com", "Lolo__123456");
            MailMessage mailMessage= new MailMessage();
            mailMessage.From = new MailAddress("aboelnasrl.ob@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat($"<h1>Your Order Status is changed to be {status}</h1>");
            mailBody.AppendFormat("<br  />");
            mailBody.AppendFormat("<p>Thank You");
            mailMessage.Body = mailBody.ToString();
            client.Send(mailMessage);
            
        }
    }
}
