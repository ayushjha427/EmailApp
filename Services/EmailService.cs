using System.Net;
using System.Net.Mail;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using EmailCommunicator.Models;
using Microsoft.Extensions.Options;

namespace EmailCommunicator.Services
{
    public class EmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        // -------- SEND EMAIL (SMTP) --------
        public void Send(string to, string subject, string body)
        {
            var smtp = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
            {
                Credentials = new NetworkCredential(
                    _settings.Email,
                    _settings.Password
                ),
                EnableSsl = true
            };

            var mail = new MailMessage(
                _settings.Email,
                to,
                subject,
                body
            );

            smtp.Send(mail);
        }

        // -------- RECEIVE EMAIL (IMAP) --------
        public List<MimeMessage> GetInbox(int count = 10)
        {
            using var client = new ImapClient();

            client.Connect(_settings.ImapServer,
                _settings.ImapPort,
                true
            );

            client.Authenticate(
                _settings.Email,
                _settings.Password
            );

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            return inbox.Take(count).ToList();
        }
    }
}