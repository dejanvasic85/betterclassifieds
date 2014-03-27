using System;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EmailDelivery
    {
        public EmailDelivery(Guid broadcastId, string subject, string body, bool isHtml, string docType, string from, params string[] to)
        {
            BroadcastId = broadcastId;
            Subject = subject;
            Body = body;
            To = string.Join(",", to);
            From = from;
            IsHtml = isHtml;
            CreatedDate = DateTime.Now;
            CreatedDateUtc = DateTime.UtcNow;
            DocType = docType;
        }

        public static EmailDelivery BuildWithTemplate<T>(T docType, EmailTemplate emailTemplate, Guid broadcastId, params string[] to) where T : IDocType
        {
            Guard.NotNull(emailTemplate);
            Guard.NotDefaultValue(broadcastId);
            Guard.NotNullOrEmpty(to);

            // Fetch the parser required for this template ( by name )
            var parser = ParserFactory.ResolveParser(emailTemplate.ParserName);

            if(parser == null)
                throw new NullReferenceException(string.Format("Parser {0} does cannot be resolved.", emailTemplate.ParserName));

            // Construct the body and subject by performing the parsing on each
            var subject = parser.ParseToString(emailTemplate.SubjectTemplate, docType.ToPlaceholderDictionary());
            var body = parser.ParseToString(emailTemplate.BodyTemplate, docType.ToPlaceholderDictionary());

            return new EmailDelivery(broadcastId, subject, body, emailTemplate.IsBodyHtml, emailTemplate.DocType, emailTemplate.FromAddress, to);
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
        public string DocType { get; private set; }
        public int Attempts { get; private set; }
        public DateTime? LastAttemptDate { get; set; }
        public DateTime? LastAttemptDateUtc { get; set; }
        public DateTime? SentDate { get; private set; }
        public DateTime? SentDateUtc { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public DateTime? CreatedDateUtc { get; private set; }
        public string LastExceptionMessage { get; private set; }
        public string LastExceptionStackTrace { get; private set; }

        public void IncrementAttempts()
        {
            Attempts++;
            LastAttemptDate = DateTime.Now;
            LastAttemptDateUtc = DateTime.UtcNow;
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