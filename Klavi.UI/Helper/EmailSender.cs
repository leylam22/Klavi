using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Klavi.UI.Helper;

public class EmailSender : IEmailSender
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword) 
    {
        _smtpPort = smtpPort;
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
        _smtpServer = smtpServer;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using (var client = new SmtpClient(_smtpServer, _smtpPort))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUsername),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

           await client.SendMailAsync(mailMessage);
        }
    }
}
