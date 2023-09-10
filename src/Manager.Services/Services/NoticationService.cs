using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Manager.Services.Interface;
using MimeKit;
using MimeKit.Text;

namespace Manager.Services.Services
{
    public class NoticationService : INotificationService
    {
        public bool EnviarEmail(string destinatario, string assunto, string corpo)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("kenya.cruickshank42@ethereal.email"));
                email.To.Add(MailboxAddress.Parse(destinatario));
                email.Subject = assunto;
                email.Body = new TextPart(TextFormat.Html) { Text = corpo };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("kenya.cruickshank42@ethereal.email", "4J2Y8v7XaaKZYPJffZ");
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    
}