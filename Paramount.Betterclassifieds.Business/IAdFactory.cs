namespace Paramount.Betterclassifieds.Business
{
    using Events;

    public interface IAdFactory
    {
        EventModel CreateEvent();
    }

    public class AdFactory : IAdFactory
    {
        private readonly IDateService _dateService;

        public AdFactory(IDateService dateService)
        {
            _dateService = dateService;
        }

        public EventModel CreateEvent()
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