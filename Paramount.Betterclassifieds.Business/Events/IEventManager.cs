namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventManager
    {
        EventModel GetEventDetails(int onlineAdId);
    }

    public class EventManager : IEventManager
    {
        private readonly IEventRepository _eventRepository;

        public EventManager(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public EventTicketReservationResult ReserveTickets(EventTicketReservationRequest request)
        {
            return new EventTicketReservationResult
            {
                EventTicket = request.EventTicket
            };
        }

        public EventModel GetEventDetails(int onlineAdId)
        {
            return _eventRepository.GetEventDetails(onlineAdId);
        }
    }
}