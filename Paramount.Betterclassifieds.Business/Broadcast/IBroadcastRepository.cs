namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface IBroadcastRepository
    {
        EmailTemplate GetTemplateByName(string templateName);
    }
}