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
            var startDate = _dateService.NowToNextHour;
            var endDate = startDate.AddHours(4);

            // Initialise with start date and hours to be close to the current time
            return new EventModel
            {
                EventStartDate = startDate,
                EventEndDate = endDate
            };
        }
    }
}