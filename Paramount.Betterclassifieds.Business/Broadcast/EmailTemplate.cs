﻿namespace Paramount.Betterclassifieds.Business
{
    /// <summary>
    /// Generic template for emails so we need at least subject, body and sender
    /// </summary>
    public class EmailTemplate
    {
        public int? EmailTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string BodyTemplate { get; set; }
        public string SubjectTemplate { get; set; }
        public string FromAddress { get; set; }
        public bool IsBodyHtml { get; set; }

        // Type of parser that is required
        public string ParserName { get; set; }
        
    }
}