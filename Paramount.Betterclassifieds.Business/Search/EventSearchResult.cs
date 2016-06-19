using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Business.Search
{
    public class EventSearchResult
    {
        public AdSearchResult AdSearchResult { get; set; }
        public EventModel EventDetails { get; set; }
        public Address Address { get; set; }
    }
}