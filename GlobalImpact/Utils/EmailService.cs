using System.ComponentModel.DataAnnotations;
using GlobalImpact.Interfaces;
using System.Net;
using MailKit.Net.Smtp;
//using System.Net.Mail;
using MimeKit;


namespace GlobalImpact.Utils
{
    /// <summary>
    /// Classe para envio de email de confirmação de conta.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var eml = new MimeMessage();
            eml.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            eml.To.Add(MailboxAddress.Parse(email));
            eml.Subject = subject;
            eml.Body = new TextPart("plain")
            {
                Text = message
            };

            using var smtp = new SmtpClient();

            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(eml);
            smtp.Disconnect(true);
        }
    }
}
