using System;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    /// <summary>
    /// Generic template for emails so we need at least subject, body and sender
    /// </summary>
    public class EmailTemplate
    {
        public EmailTemplate()
            : this(typeof (SquareBracketParser).Name)
        {
        }

        public EmailTemplate(string parserName)
        {
            ParserName = parserName;
            ModifiedDate = DateTime.Now;
            ModifiedDateUtc = DateTime.UtcNow;
            ModifiedBy = "system";
        }
        
        public int EmailTemplateId { get; set; }
        public string Description { get; set; }
        public string BodyTemplate { get; set; }
        public string SubjectTemplate { get; set; }
        public string From { get; set; }
        public bool IsBodyHtml { get; set; }
        public string DocType { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; private set; }
        public DateTime? ModifiedDateUtc { get; private set; }

        // Type of parser that is required
        public string ParserName { get; private set; }
        
    }
}