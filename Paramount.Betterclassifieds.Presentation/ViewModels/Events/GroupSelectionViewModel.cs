using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class GroupSelectionViewModel
    {
        public GroupSelectionViewModel()
        {
            
        }

        public GroupSelectionViewModel(EventBooking eventBooking)
        {
            EventId = eventBooking.EventId;
            EventBookingId = eventBooking.EventBookingId;
            EventBookingTickets = eventBooking.EventBookingTickets
                .Select(ebt => new EventBookingTicketViewModel
                {
                    EventBookingId = ebt.EventBookingId,
                    EventBookingTicketId = ebt.EventBookingTicketId,
                    EventTicketId = ebt.EventTicketId,
                    TicketName = ebt.TicketName,
                    GuestFullName = ebt.GuestFullName,
                    GuestEmail = ebt.GuestEmail,
                    TotalPrice = ebt.TotalPrice
                })
                .ToList();
        }

        public int EventId { get; set; }
        public int EventBookingId { get; set; }
        public List<EventBookingTicketViewModel> EventBookingTickets { get; set; }
    }
}