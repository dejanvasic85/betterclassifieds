using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public interface ITicketRequestValidator
    {
        /// <summary>
        /// Determines whether the request for tickets adheres to what is available at the moment
        /// </summary>
        bool IsSufficientTicketsAvailableForRequest(EventModel eventModel, TicketReservationRequest[] requests);
    }

    public class TicketRequestValidator : ITicketRequestValidator
    {
        private readonly IEventManager _eventManager;
        private readonly IEventSeatingService _eventSeatingService;
        private readonly IEventRepository _eventRepository;

        public TicketRequestValidator(IEventManager eventManager, IEventSeatingService eventSeatingService, IEventRepository eventRepository)
        {
            _eventManager = eventManager;
            _eventSeatingService = eventSeatingService;
            _eventRepository = eventRepository;
        }

        public bool IsSufficientTicketsAvailableForRequest(EventModel eventModel, TicketReservationRequest[] requests)
        {
            Guard.NotNull(requests);
            Guard.NotNull(eventModel);
            Guard.NotNull(eventModel.Tickets);

            var groupRule = new GroupQuantityRequestRule();
            var ticketRule = new TicketQuantityRequestRule(_eventManager);
            var seatAvailabilityRule = new SeatAvailabilityRule();

            var groupRequests = CreateGroupRequests(requests).ToArray();
            var ticketRequests = CreateTicketRequests(requests, eventModel.Tickets);
            var seatRequests = CreateSeatRequests(eventModel, requests);

            return groupRequests.All(groupRule.IsSatisfiedBy)
                && ticketRequests.All(t => ticketRule.IsSatisfiedBy(t))
                && seatRequests.All(s => seatAvailabilityRule.IsSatisfiedBy(s));
        }

        private IEnumerable<TicketQuantityRequest> CreateTicketRequests(IEnumerable<TicketReservationRequest> requests, IEnumerable<EventTicket> availableTickets)
        {
            return requests.Select(r => new TicketQuantityRequest(availableTickets.Single(t => t.EventTicketId == r.EventTicketId), r.SelectedQuantity));
        }

        private IEnumerable<SeatRequest> CreateSeatRequests(EventModel eventModel, IEnumerable<TicketReservationRequest> requests)
        {
            if (eventModel.IsSeatedEvent.HasValue && eventModel.IsSeatedEvent.Value)
            {
                var currentTicketBookings = _eventRepository.GetEventBookingTicketsForEvent(eventModel.EventId);
                var currentTicketReservations = _eventRepository.GetCurrentReservationsForEvent(eventModel.EventId.GetValueOrDefault());
                
                return requests.Select(r => new SeatRequest(r.OrderRequestId,
                    _eventSeatingService.GetEventSeat(eventModel.EventId.GetValueOrDefault(), r.SeatNumber),
                    currentTicketBookings,
                    currentTicketReservations));
            }
            return new List<SeatRequest>();
        }

        private IEnumerable<GroupRequest> CreateGroupRequests(IEnumerable<TicketReservationRequest> requests)
        {
            var groups = requests.Where(g => g.EventGroupId.HasValue).GroupBy(r => r.EventGroupId);
            return groups.Select(gr => new GroupRequest(
                Task.Run(() => _eventManager.GetEventGroup(gr.Key.GetValueOrDefault())).Result,
                gr.Sum(g => g.SelectedQuantity)));
        }
    }
}
