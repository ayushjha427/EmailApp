namespace EmailCommunicator.Models
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string ImapServer { get; set; }
        public int ImapPort { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}