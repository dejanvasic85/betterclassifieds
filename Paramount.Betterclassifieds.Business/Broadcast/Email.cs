using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class Email
    {
        protected Email()
        {
            // Parameterless constructor for EntityFramework ( hope it works >.. )
        }

        public Email(Guid broadcastId, string subject, string body, bool isBodyHtml, string docType, string from, params string[] to)
        {
            BroadcastId = broadcastId;
            Subject = subject;
            Body = body;
            To = string.Join(",", to);
            From = from;
            IsBodyHtml = isBodyHtml;
            ModifiedDate = DateTime.Now;
            ModifiedDateUtc = DateTime.UtcNow;
            DocType = docType;
            EmailAttachments = new EmailAttachment[0];
        }

        public static Email BuildWithTemplate<T>(T docType, EmailTemplate emailTemplate, Guid broadcastId, params string[] to) where T : IDocType
        {
            Guard.NotNull(emailTemplate);
            Guard.NotDefaultValue(broadcastId);
            Guard.NotNullOrEmpty(to);

            // Fetch the parser required for this template ( by name )
            var parser = ParserFactory.ResolveParser(emailTemplate.ParserName);

            if (parser == null)
                throw new NullReferenceException(string.Format("Parser {0} does cannot be resolved.", emailTemplate.ParserName));

            // Construct the body and subject by performing the parsing on each
            var subject = parser.ParseToString(emailTemplate.SubjectTemplate, docType.ToPlaceholderDictionary());
            var body = parser.ParseToString(emailTemplate.BodyTemplate, docType.ToPlaceholderDictionary());

            var email = new Email(broadcastId, subject, body, emailTemplate.IsBodyHtml, emailTemplate.DocType, emailTemplate.From, to);

            if (docType.Attachments != null)
            {
                email.EmailAttachments = docType.Attachments.ToArray();
            }
            return email;
        }

        public long? EmailDeliveryId { get; set; }
        public Guid BroadcastId { get; private set; }
        public string To { get; private set; }
        public string Cc { get; private set; }
        public string Bcc { get; private set; }
        public string From { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public bool IsBodyHtml { get; private set; }
        public string DocType { get; private set; }
        public int Attempts { get; private set; }
        public DateTime? LastAttemptDate { get; set; }
        public DateTime? LastAttemptDateUtc { get; set; }
        public DateTime? SentDate { get; private set; }
        public DateTime? SentDateUtc { get; private set; }
        public DateTime? ModifiedDate { get; private set; }
        public DateTime? ModifiedDateUtc { get; private set; }
        public string LastErrorMessage { get; private set; }
        public string LastErrorStackTrace { get; private set; }

        public IList<EmailAttachment> EmailAttachments { get; private set; }

        private void IncrementAttempts()
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
            LastErrorMessage = message;
            LastErrorStackTrace = stacktrace;
        }

        public void MarkAsSent()
        {
            SentDate = DateTime.Now;
            SentDateUtc = DateTime.UtcNow;
        }

        public void Send(ISmtpMailer smtp)
        {
            IncrementAttempts();

            try
            {
                smtp.SendEmail(Subject, Body, From, EmailAttachments.ToArray(), To);

                MarkAsSent();
            }
            catch (Exception ex)
            {
                LogException(ex.InnerMostException().Message, ex.StackTrace);
            }
        }

        public bool ReachedMaxAttempts
        {
            get { return Attempts >= 3; }
        }

        public bool IsComplete
        {
            get { return SentDate.HasValue || ReachedMaxAttempts; }
        }
    }
}