using System;

namespace Paramount.Betterclassifieds.Domain.Notifications
{
    public class Email
    {
        public int Id { get; set; }
        public string EmailRecipient { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public bool IsBodyHtml { get; set; }
        public DateTime? LastRetryDate { get; set; }
        public DateTime? SentDateTime { get; set; }
        public int Attempts { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string Sender { get; set; }
    }
}
