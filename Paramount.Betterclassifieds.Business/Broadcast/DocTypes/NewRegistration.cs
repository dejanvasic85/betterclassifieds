using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class NewRegistration : IDocType
    {
        public string DocumentType
        {
            get { return GetType().Name; }
        }

        public IList<EmailAttachment> Attachments { get; set; }


        [Placeholder("FirstName")]
        public string FirstName { get; set; }

        [Placeholder("LastName")]
        public string LastName { get; set; }

        [Placeholder("ConfirmationCode")]
        public string ConfirmationCode { get; set; }
    }
}