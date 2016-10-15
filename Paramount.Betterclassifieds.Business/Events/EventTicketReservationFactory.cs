using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventTicketReservationFactory
    {
        IEnumerable<EventTicketReservation> CreateReservations(int eventTicketId, int quantity, string sessionId, int? eventGroupId);
        EventTicketReservation CreateReservation(string sessionId, int? eventGroupId, EventTicket eventTicket);
        EventTicketReservation CreateFreeReservation(string sessionId, int? eventGroupId, EventTicket eventTicket);
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

        public IEnumerable<EventTicketReservation> CreateReservations(int eventTicketId, int quantity, string sessionId, int? eventGroupId)
        {
            var eventTicket = _eventRepository.GetEventTicketDetails(eventTicketId, includeReservations: true);

            for (int i = 0; i < quantity; i++)
            {
                var reservation = CreateReservation(sessionId, eventGroupId, eventTicket);

                yield return reservation;
            }
        }

        public EventTicketReservation CreateReservation( string sessionId, int? eventGroupId, EventTicket eventTicket)
        {
            var calculator = new TicketFeeCalculator(_clientConfig);
            var ticketPrice = calculator.GetTotalTicketPrice(eventTicket);
            var eventDetails = _eventRepository.GetEventDetails(eventTicket.EventId.GetValueOrDefault());

            var reservation = Create(sessionId, eventGroupId, eventTicket);
            reservation.Price = ticketPrice.OriginalPrice;
            reservation.TransactionFee = eventDetails.IncludeTransactionFee.GetValueOrDefault() ? ticketPrice.Fee : 0;
            
            return reservation;
        }

        public EventTicketReservation CreateFreeReservation(string sessionId, int? eventGroupId, EventTicket eventTicket)
        {
            return Create(sessionId, eventGroupId, eventTicket);
        }

        private EventTicketReservation Create(string sessionId, int? eventGroupId, EventTicket eventTicket)
        {
            var reservation = new EventTicketReservation
            {
                CreatedDate = _dateService.Now,
                CreatedDateUtc = _dateService.UtcNow,
                ExpiryDate = _dateService.Now.AddMinutes(_clientConfig.EventTicketReservationExpiryMinutes),
                ExpiryDateUtc = _dateService.UtcNow.AddMinutes(_clientConfig.EventTicketReservationExpiryMinutes),
                SessionId = sessionId,
                Quantity = 1,
                EventGroupId = eventGroupId,
                EventTicketId = eventTicket?.EventTicketId,
                Status = new SufficientTicketsRule()
                    .IsSatisfiedBy(new RemainingTicketsWithRequestInfo(1, _eventManager.GetRemainingTicketCount(eventTicket)))
                    .Result
            };
            return reservation;
        }
    }
}