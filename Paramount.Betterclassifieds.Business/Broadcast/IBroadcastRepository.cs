namespace Paramount.Betterclassifieds.Business
{
    public interface IBroadcastRepository
    {
        EmailTemplate GetTemplateByName(string templateName);
    }
}