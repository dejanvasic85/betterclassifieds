using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class ForgottenPassword : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }
        public IList<EmailAttachment> Attachments { get; set; }


        [Placeholder("Username")]
        public string Username { get; set; }

        [Placeholder("Password")]
        public string Password { get; set; }

        [Placeholder("Email")]
        public string Email { get; set; }
    }
}