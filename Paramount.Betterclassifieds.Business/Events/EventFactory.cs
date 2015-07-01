using Paramount.Utility;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventFactory
    {
        EventModel Create();
    }

    public class EventFactory : IEventFactory
    {
        private readonly IDateService _dateService;

        public EventFactory(IDateService dateService)
        {
            _dateService = dateService;
        }

        public EventModel Create()
        {
            // Initialise with start date and hours to be close to the current time
            return new EventModel
            {
                EventStartDate = _dateService.Now,
                EventEndDate = _dateService.Now.AddHours(2)
            };
        }
    }
}
