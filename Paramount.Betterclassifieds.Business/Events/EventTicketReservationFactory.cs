using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventTicketReservationFactory
    {
        IEnumerable<EventTicketReservation> CreateReservations(int eventTicketId, int quantity, string sessionId);
        EventTicketReservation CreateReservation(int eventTicketId, string sessionId, EventTicket eventTicket);
    }

    public class EventTicketReservationFactory : IEventTicketReservationFactory
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;
        private readonly IClientConfig _clientConfig;
        private readonly IEventManager _eventManager;

        public EventTicketReservationFactory(IEventRepository eventRepository, IDateService dateService, IClientConfig clientConfig, IEventManager eventManager)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
            _clientConfig = clientConfig;
            _eventManager = eventManager;
        }

        public IEnumerable<EventTicketReservation> CreateReservations(int eventTicketId, int quantity, string sessionId)
        {
            var eventTicket = _eventRepository.GetEventTicketDetails(eventTicketId, includeReservations: true);

            for (int i = 0; i < quantity; i++)
            {
                var reservation = CreateReservation(eventTicketId, sessionId, eventTicket);

                yield return reservation;
            }
        }

        public EventTicketReservation CreateReservation(int eventTicketId, string sessionId, EventTicket eventTicket)
        {
            var reservation = new EventTicketReservation
            {
                CreatedDate = _dateService.Now,
                CreatedDateUtc = _dateService.UtcNow,
                ExpiryDate = _dateService.Now.AddMinutes(_clientConfig.EventTicketReservationExpiryMinutes),
                ExpiryDateUtc = _dateService.UtcNow.AddMinutes(_clientConfig.EventTicketReservationExpiryMinutes),
                SessionId = sessionId,
                Quantity = 1,
                EventTicketId = eventTicketId,
                Price = eventTicket.Price,
                Status = new SufficientTicketsRule()
                    .IsSatisfiedBy(new RemainingTicketsWithRequestInfo(1, _eventManager.GetRemainingTicketCount(eventTicket)))
                    .Result
            };
            return reservation;
        }
    }
}