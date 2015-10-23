namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class NewRegistration : IDocType
    {
        public string DocumentType
        {
            get { return GetType().Name; }
        }

        public EmailAttachment[] Attachments { get; private set; }

        [Placeholder("FirstName")]
        public string FirstName { get; set; }

        [Placeholder("LastName")]
        public string LastName { get; set; }

        [Placeholder("ConfirmationCode")]
        public string ConfirmationCode { get; set; }
    }
}