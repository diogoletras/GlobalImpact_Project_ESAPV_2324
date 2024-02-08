using GlobalImpact.Interfaces;
using System.Net;
using System.Net.Mail;


namespace GlobalImpact.Utils
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "diogos.game@gmail.com";
            var pw = "eijt gihk glju gysx";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(mail, pw),
                EnableSsl = true
            };

            await client.SendMailAsync(new MailMessage(mail, email, subject, message));
        }
    }
}
