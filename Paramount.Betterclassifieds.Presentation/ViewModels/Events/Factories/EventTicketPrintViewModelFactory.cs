using System.Collections.Generic;
using System.Linq;
using System.Monads;
using System.Threading.Tasks;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events.Factories
{
    public class EventTicketPrintViewModelFactory
    {
        // Todo - Service location no good. Need to think of a better pattern :(
        private readonly IEventManager _eventManager;

        public EventTicketPrintViewModelFactory()
        {
            _eventManager = DependencyResolver.Current.GetService<IEventManager>();
        }

        public IEnumerable<EventTicketPrintViewModel> FromEventBooking(UrlHelper urlHelper, IEventBarcodeManager barcodeManager, AdSearchResult adDetails, EventModel eventDetails, EventBooking eventBooking)
        {
            return eventBooking.EventBookingTickets
                .Select(eventBookingTicket => FromEventBookingTicket(urlHelper, barcodeManager, adDetails, eventDetails, eventBookingTicket))
                .ToList();
        }

        public EventTicketPrintViewModel FromEventBookingTicket(UrlHelper urlHelper, IEventBarcodeManager barcodeManager, AdSearchResult adDetails, EventModel eventDetails, EventBookingTicket ticket)
        {
            EventGroup group = null;
            if (ticket.EventGroupId.HasValue)
            {
                group = Task.Run(() => _eventManager.GetEventGroup(ticket.EventGroupId.Value)).Result;
            }
            
            return new EventTicketPrintViewModel
            {
                TicketName = ticket.TicketName,
                TicketNumber = ticket.EventTicketId.ToString(),
                EventPhoto = adDetails.PrimaryImage,
                EventName = adDetails.Heading,
                Location = eventDetails.VenueNameAndLocation,
                StartDateTime = eventDetails.EventStartDate.GetValueOrDefault().ToString("dd-MMM-yyyy HH:mm"),
                Price = ticket.TotalPrice,
                ContactNumber = adDetails.ContactPhone,
                BarcodeData = urlHelper.ValidateBarcode(barcodeManager.GenerateBarcodeData(eventDetails, ticket)).WithFullUrl(),
                GroupName = group.With(g => g.GroupName),
                GuestFullName = ticket.GuestFullName,
            };
        }
    }
}