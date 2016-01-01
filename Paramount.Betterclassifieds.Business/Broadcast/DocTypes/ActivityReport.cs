using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class ActivityReport : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }
        public IList<EmailAttachment> Attachments { get; set; }

        [Placeholder("ReportDate")]
        public string ReportDate { get; set; }

        [Placeholder("Environment")]
        public string Environment { get; set; }

        [Placeholder("ClassifiedsTable")]
        public string ClassifiedsTable { get; set; }

        [Placeholder("LogTable")]
        public string LogTable { get; set; }
    }
}