using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;
//using WebApi.Helpers;

namespace OnlineStore.Services
{
    public class SmtpHiddenInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public int SecureSocketOptions { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }


    public interface IEmailService
    {
        Task SendAsync(string from, string to, string subject, string html);
    }

    public class EmailService : IEmailService
    {
        public IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendAsync(string from, string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("exampleSender", from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            SmtpHiddenInfo smtpHiddenInfo = new SmtpHiddenInfo();
            _configuration.GetSection("SmtpHiddenInfo").Bind(smtpHiddenInfo);

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                smtpHiddenInfo.Host,
                smtpHiddenInfo.Port,
                (SecureSocketOptions)smtpHiddenInfo.SecureSocketOptions);

            await smtp.AuthenticateAsync(smtpHiddenInfo.User, smtpHiddenInfo.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
