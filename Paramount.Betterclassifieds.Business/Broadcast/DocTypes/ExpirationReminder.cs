namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class ExpirationReminder : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }

        [Placeholder("AdReference")]
        public string AdReference { get; set; }

        [Placeholder("LinkForExtension")]
        public string LinkForExtension { get; set; }
    }
}