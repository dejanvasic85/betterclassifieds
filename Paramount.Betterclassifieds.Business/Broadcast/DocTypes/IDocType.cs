namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface IDocType
    {
        string DocumentType { get; } // e.g. NewRegistration, AccountConfirmation
        EmailAttachment[] Attachments { get; }
    }
}