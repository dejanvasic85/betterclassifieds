namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class NewBooking : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }

        [Placeholder("username")]
        public string Username { get; set; }

        [Placeholder("content")]
        public string Content { get; set; }
    }
}