using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MimeKit;

namespace WebUI.Identity
{
    public class EmailService : IIdentityMessageService
    {
        public async  Task SendAsync(IdentityMessage message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "email_from_config"));
            emailMessage.To.Add(new MailboxAddress("", message.Destination));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Body
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {

                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                await client.AuthenticateAsync("email_from_config", "password_from_config");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}