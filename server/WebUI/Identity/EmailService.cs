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

            emailMessage.From.Add(new MailboxAddress("������������� �����", "totsamiynixon@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", message.Destination));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Body
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {

                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                await client.AuthenticateAsync("totsamiynixon@gmail.com", "ff7forever");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}