using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventDashboardViewModel
    {
        public EventDashboardViewModel()
        { }

        public EventDashboardViewModel(int adId, int pageViews, EventModel eventModel, List<EventTicketViewModel> tickets)
        {
            this.AdId = adId;
            this.PageViews = pageViews;
            this.EventId = eventModel.EventId.GetValueOrDefault();
            this.Tickets = tickets;

            if (eventModel.EventBookings != null)
            {
                this.TotalRemainingQty = eventModel.Tickets.Sum(t => t.RemainingQuantity);

                var bookedTickets = eventModel.EventBookings.SelectMany(m => m.EventBookingTickets).ToList();
                this.TotalSoldQty = bookedTickets.Count;
                this.TotalSoldAmount = bookedTickets.Sum(t => t.Price);

                // Get the sold quantity for each ticket type
                foreach (var eventTicketType in tickets)
                {
                    eventTicketType.SoldQty = bookedTickets.Count(t => t.EventTicketId == eventTicketType.EventTicketId);
                }
            }
        }

        public int AdId { get; set; }
        public int PageViews { get; set; }
        public int EventId { get; set; }
        public List<EventTicketViewModel> Tickets { get; set; }
        public int TotalSoldQty { get; set; }
        public int TotalRemainingQty { get; set; }
        public decimal? TotalSoldAmount { get; set; }
    }
}