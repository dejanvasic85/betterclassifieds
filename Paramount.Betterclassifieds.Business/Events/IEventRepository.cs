namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventRepository
    {
        EventModel GetEventDetails(int onlineAdId);
    }
}