using FindACoach.Core.Configuration;
using FindACoach.Core.ServiceContracts;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace FindACoach.Core.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailSettings _emailOptions;

        public EmailSenderService(IOptions<EmailSettings> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                client.Host = _emailOptions.Host;
                client.Port = _emailOptions.Port;
                client.EnableSsl = _emailOptions.EnableSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailOptions.UserName, _emailOptions.Password);

                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_emailOptions.SenderEmail, _emailOptions.SenderName);
                    mailMessage.To.Add(email);
                    mailMessage.Subject = subject;
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = true;

                    await client.SendMailAsync(mailMessage);
                }
            }
        }
    }
}