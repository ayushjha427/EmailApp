using System.Net;
using System.Net.Mail;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using Microsoft.Extensions.Options;
using EmailCommunicator.Models;

namespace EmailCommunicator.Services
{
    public class EmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        // SEND (SMTP)
        public void Send(string to, string subject, string body)
        {
            var smtp = new SmtpClient(
                _settings.Send.SmtpServer,
                _settings.Send.SmtpPort)
            {
                Credentials = new NetworkCredential(
                    _settings.Send.Username,
                    _settings.Send.Password),
                EnableSsl = true
            };

            var mail = new MailMessage(
                _settings.Send.FromEmail,
                to,
                subject,
                body
            );

            smtp.Send(mail);
        }

        // RECEIVE (IMAP) – LATEST EMAILS FIRST ✅
        public List<MimeMessage> GetInbox(int count = 20)
        {
            using var client = new ImapClient();

            client.Connect(
                _settings.Receive.ImapServer,
                _settings.Receive.ImapPort,
                true
            );

            client.Authenticate(
                _settings.Receive.Email,
                _settings.Receive.Password
            );

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            var messages = new List<MimeMessage>();

            for (int i = inbox.Count - 1; i >= 0 && messages.Count < count; i--)
            {
                messages.Add(inbox.GetMessage(i));
            }

            client.Disconnect(true);

            return messages;
        }
    }
}