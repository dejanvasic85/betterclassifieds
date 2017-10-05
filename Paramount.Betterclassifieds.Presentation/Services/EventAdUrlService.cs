using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class EventAdUrlService : ICategoryAdUrlService
    {
        private readonly IUrl _url;

        public EventAdUrlService(IUrl url)
        {
            _url = url;
        }

        public string EditUrl(int adId)
        {
            return _url.EventDashboardUrl(adId);
        }
    }
}