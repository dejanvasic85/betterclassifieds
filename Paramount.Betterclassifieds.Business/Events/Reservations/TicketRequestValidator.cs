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

        public TicketRequestValidator(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public bool IsSufficientTicketsAvailableForRequest(TicketReservationRequest[] requests)
        {
            Guard.NotNull(requests);

            var groupRule = new GroupQuantityRequestRule();
            var ticketRule = new TicketQuantityRequestRule(_eventManager);

            var groupRequests = CreateGroupRequests(requests).ToArray();
            var ticketRequests = CreateTicketRequests(requests).ToArray();

            return groupRequests.All(groupRule.IsSatisfiedBy) && ticketRequests.All(t => ticketRule.IsSatisfiedBy(t));
        }   

        private IEnumerable<GroupRequest> CreateGroupRequests(IEnumerable<TicketReservationRequest> requests)
        {
            var groups = requests.Where(g => g.EventGroupId.HasValue).GroupBy(r => r.EventGroupId);
            return groups.Select(gr => new GroupRequest(
                Task.Run(() => _eventManager.GetEventGroup(gr.Key.GetValueOrDefault())).Result,
                gr.Sum(g => g.SelectedQuantity)));
        }

        private IEnumerable<TicketQuantityRequest> CreateTicketRequests(IEnumerable<TicketReservationRequest> requests)
        {
            var ticketRequestGroups = requests.GroupBy(r => r.EventTicketId);
            return ticketRequestGroups.Select(tg => new TicketQuantityRequest(_eventManager.GetEventTicketAndReservations(tg.Key),
                tg.Sum(t => t.SelectedQuantity)));
        }
    }
}
