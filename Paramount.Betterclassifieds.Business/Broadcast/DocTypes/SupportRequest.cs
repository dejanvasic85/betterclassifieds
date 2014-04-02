namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class SupportRequest : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }

        [Placeholder("content")]
        public string RequestDetails { get; set; }

    }
}