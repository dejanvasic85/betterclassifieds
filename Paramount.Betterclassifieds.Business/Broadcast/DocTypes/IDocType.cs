using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface IDocType
    {
        string DocumentType { get; } // e.g. NewRegistration, AccountConfirmation
        IList<EmailAttachment> Attachments { get; }
    }
}