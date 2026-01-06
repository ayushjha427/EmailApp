namespace EmailCommunicator.Models
{
    public class EmailSettings
    {
        public SendSettings Send { get; set; }
        public ReceiveSettings Receive { get; set; }
    }

    public class SendSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
    }

    public class ReceiveSettings
    {
        public string ImapServer { get; set; }
        public int ImapPort { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}