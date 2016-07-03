using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Business.Search
{
    public class EventSearchResult
    {
        public EventSearchResult()
        {
            
        }

        public EventSearchResult(AdSearchResult adSearchResult, EventModel eventModel,
            Address address)
        {
            AdSearchResult = adSearchResult;
            EventDetails = eventModel;
            Address = address;
        }

        public AdSearchResult AdSearchResult { get; set; }
        public EventModel EventDetails { get; set; }
        public Address Address { get; set; }
    }
}