using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventDashboardViewModel
    {
        public EventDashboardViewModel()
        { }

        public EventDashboardViewModel(int adId, int pageViews, EventModel eventModel, EventPaymentSummary paymentSummary, EventPaymentRequestStatus status, List<EventTicketViewModel> tickets, List<EventGuestListViewModel> guests)
        {
            this.AdId = adId;
            this.PageViews = pageViews;
            this.EventId = eventModel.EventId.GetValueOrDefault();
            this.Tickets = tickets;
            this.Guests = guests;
            this.EventPaymentRequestStatus = status.Humanize(LetterCasing.Title);
            this.IsClosed = eventModel.IsClosed;
            this.OrganiserAbsorbsTransactionFee = !eventModel.IncludeTransactionFee;

            if (eventModel.EventBookings != null)
            {
                this.TotalRemainingQty = eventModel.Tickets.Sum(t => t.RemainingQuantity);

                var bookedTickets = eventModel.EventBookings.SelectMany(m => m.EventBookingTickets).ToList();
                this.TotalSoldQty = bookedTickets.Count;
                this.TotalSoldAmount = paymentSummary.TotalTicketSalesAmount;
                this.EventOrganiserOwedAmount = paymentSummary.EventOrganiserOwedAmount;
                this.SystemTicketFeeLabel = string.Format("{0}%", paymentSummary.SystemTicketFee);


                // Get the sold quantity for each ticket type
                foreach (var eventTicketType in tickets)
                {
                    eventTicketType.SoldQty = bookedTickets.Count(t => t.EventTicketId == eventTicketType.EventTicketId);
                }
            }
        }

        public bool? OrganiserAbsorbsTransactionFee { get; set; }

        public string EventPaymentRequestStatus { get; set; }


        public int AdId { get; set; }
        public int PageViews { get; set; }
        public int EventId { get; set; }
        public List<EventTicketViewModel> Tickets { get; set; } // Ticket definitions
        public List<EventGuestListViewModel> Guests { get; set; } // Tickets purchased (guests)
        public int TotalSoldQty { get; set; }
        public int TotalRemainingQty { get; set; }
        public decimal? TotalSoldAmount { get; set; }
        public decimal? EventOrganiserOwedAmount { get; set; }
        public string SystemTicketFeeLabel { get; set; }
        public bool IsClosed { get; set; }
    }
}