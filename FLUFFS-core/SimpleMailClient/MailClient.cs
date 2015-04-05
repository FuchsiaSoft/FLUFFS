using SimpleMailClient.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMailClient
{
    public class MailClient : IMailClient
    {
        public void SendEmail(IEnumerable<string> recipients, 
            string subject, string body, string sender)
        {
            SmtpClient client = new SmtpClient();
            string host = Settings.Default.SmtpHost;
            int port = Settings.Default.SmtpPort;
            MailMessage message = new MailMessage()
            {
                From = new MailAddress(sender),
                Sender = new MailAddress(sender),
                Subject = subject,
                Body = body
            };
            foreach (string recipient in recipients)
            {
                message.To.Add(new MailAddress(recipient));
            }
            client.Send(message);
        }
    }
}
