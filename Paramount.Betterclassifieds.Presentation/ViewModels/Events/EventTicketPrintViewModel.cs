using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventTicketPrintViewModel
    {
        public EventTicketPrintViewModel()
        { }

        public EventTicketPrintViewModel(AdSearchResult adDetails, EventModel eventDetails, EventBookingTicket ticket)
        {
            this.TicketName = ticket.TicketName;
            this.TicketNumber = ticket.EventTicketId.ToString();
            this.ImageId = adDetails.PrimaryImage;
            this.EventName = adDetails.Heading;
            this.Location = eventDetails.Location;
            this.StartDateTime = eventDetails.EventStartDate.GetValueOrDefault().ToString("dd-MMM-yyyy");
            this.Price = ticket.Price.GetValueOrDefault();
            this.ContactNumber = adDetails.ContactPhone;
            this.BarcodeData = string.Format("{0} {1} {2}", eventDetails.EventId, ticket.EventTicketId, ticket.EventBookingTicketId);
        }

        public static IEnumerable<EventTicketPrintViewModel> Create(AdSearchResult adDetails, EventModel eventDetails, EventBooking eventBooking)
        {
            return eventBooking.EventBookingTickets
                .Select(eventBookingTicket => new EventTicketPrintViewModel(adDetails, eventDetails, eventBookingTicket))
                .ToList();
        }

        public string TicketNumber { get; set; }
        public string EventName { get; set; }
        public string ImageId { get; set; }
        public string Location { get; set; }
        public string StartDateTime { get; set; }
        public string TicketName { get; set; }
        public decimal Price { get; set; }
        public string ContactNumber { get; set; }
        public string BarcodeData { get; set; }
    }
}