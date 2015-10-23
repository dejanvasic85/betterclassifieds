namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class ExpirationReminder : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }
        public EmailAttachment[] Attachments { get; private set; }

        [Placeholder("AdReference")]
        public string AdReference { get; set; }

        [Placeholder("LinkForExtension")]
        public string LinkForExtension { get; set; }
    }
}