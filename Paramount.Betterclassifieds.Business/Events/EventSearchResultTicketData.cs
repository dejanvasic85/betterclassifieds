using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventSearchResultTicketData
    {
        public decimal? CheapestTicket { get; set; }
        public decimal? MostExpensiveTicket { get; set; }

        public bool HasTickets => CheapestTicket.HasValue || MostExpensiveTicket.HasValue;

        public bool AreAllTicketsFree => CheapestTicket.GetValueOrDefault() == 0
                                         && MostExpensiveTicket.GetValueOrDefault() == 0;
    }

    public class EventSearchResultTicketDataFactory
    {
        public EventSearchResultTicketData CreateFromEventModel(EventModel eventModel)
        {
            if (eventModel?.Tickets == null || !eventModel.Tickets.Any())
            {
                return new EventSearchResultTicketData();
            }

            var cheapest = eventModel.Tickets.OrderBy(t => t.Price).First();
            var expensive = eventModel.Tickets.OrderByDescending(t => t.Price).First();

            return new EventSearchResultTicketData
            {
                CheapestTicket = cheapest.Price,
                MostExpensiveTicket = expensive.Price
            };
        }
    }

}