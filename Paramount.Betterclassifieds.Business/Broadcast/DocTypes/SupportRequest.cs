namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class SupportRequest : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }

        [Placeholder("content")]
        public string RequestDetails { get; set; }

        [Placeholder("name")]
        public string Name { get; set; }

        [Placeholder("email")]
        public string Email { get; set; }

        [Placeholder("phone")]
        public string Phone { get; set; }
    }
}