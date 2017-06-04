using System.Collections.Concurrent;
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
        bool IsSufficientTicketsAvailableForRequest(TicketReservationRequest[] requests);
    }

    public class TicketRequestValidator : ITicketRequestValidator
    {
        private readonly IEventManager _eventManager;
        private readonly IEventSeatingService _eventSeatingService;

        public TicketRequestValidator(IEventManager eventManager, IEventSeatingService eventSeatingService)
        {
            _eventManager = eventManager;
            _eventSeatingService = eventSeatingService;
        }

        public bool IsSufficientTicketsAvailableForRequest(TicketReservationRequest[] requests)
        {
            Guard.NotNull(requests);

            var groupRule = new GroupQuantityRequestRule();
            var ticketRule = new TicketQuantityRequestRule(_eventManager);
            var seatAvailabilityRule = new SeatAvailabilityRule();

            var groupRequests = CreateGroupRequests(requests).ToArray();

            // Fetch all the tickets and their reservations 
            ConcurrentBag<EventTicket> tickets = new ConcurrentBag<EventTicket>();
            ConcurrentBag<TicketQuantityRequest> ticketRequests =  new ConcurrentBag<TicketQuantityRequest>();
            ConcurrentBag<SeatRequest> seatRequests = new ConcurrentBag<SeatRequest>();

            Parallel.ForEach(requests, r =>
            {
                var ticket = _eventManager.GetEventTicketAndReservations(r.EventTicketId);
                tickets.Add(ticket);
                ticketRequests.Add(new TicketQuantityRequest(ticket, r.SelectedQuantity));
                if (r.SeatNumber.HasValue())
                {
                    seatRequests.Add(new SeatRequest(
                        r.SeatNumber, 
                        _eventSeatingService.GetSeatsForTicket(ticket, r.OrderRequestId)));
                }
            });
            
            return groupRequests.All(groupRule.IsSatisfiedBy) 
                && ticketRequests.All(t => ticketRule.IsSatisfiedBy(t))
                && seatRequests.All(s => seatAvailabilityRule.IsSatisfiedBy(s));
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
