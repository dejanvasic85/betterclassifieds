using System;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EmailDelivery
    {
        public EmailDelivery(Guid broadcastId, string subject, string body, bool isHtml, string templateName, string from, params string[] to)
        {
            BroadcastId = broadcastId;
            Subject = subject;
            Body = body;
            To = string.Join(",", to);
            From = from;
            IsHtml = isHtml;
            CreatedDate = DateTime.Now;
            CreatedDateUtc = DateTime.UtcNow;
            TemplateName = templateName;
        }

        public int EmailDeliveryId { get; set; }
        public Guid BroadcastId { get; private set; }
        public string To { get; private set; }
        public string Cc { get; private set; }
        public string Bcc { get; private set; }
        public string From { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public bool IsHtml { get; private set; }
        public string TemplateName { get; private set; }
        public int Attempts { get; private set; }
        public DateTime? SentDate { get; private set; }
        public DateTime? SentDateUtc { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public DateTime? CreatedDateUtc { get; private set; }
        public string LastExceptionMessage { get; private set; }
        public string LastExceptionStackTrace { get; private set; }
        
        public void IncrementAttempts()
        {
            Attempts++;
        }

        public void LogException(Exception ex)
        {
            LogException(ex.Message, ex.StackTrace);
        }

        public void LogException(string message, string stacktrace)
        {
            LastExceptionMessage = message;
            LastExceptionStackTrace = stacktrace;
        }

        public void MarkAsSent()
        {
            SentDate = DateTime.Now;
            SentDateUtc = DateTime.UtcNow;
        }
    }
}