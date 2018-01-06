using System.Collections.Generic;
using System.Linq;
using System.Monads;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventTicketPrintViewModelFactory
    {
        private readonly IUrl _url;
        private readonly IClientConfig _clientConfig;

        public EventTicketPrintViewModelFactory(IUrl url, IClientConfig clientConfig)
        {
            _url = url;
            _clientConfig = clientConfig;
        }

        public EventTicketPrintViewModel Create(
            AdSearchResult adDetails,
            EventModel eventDetails,
            EventBookingTicket ticket,
            string brandName,
            string brandUrl,
            IEnumerable<EventGroup> groupsForEvent)
        {
            var group = groupsForEvent?.SingleOrDefault(g => g.EventGroupId == ticket.EventGroupId);

            var image = _url.DefaultTicketAdImage();
            if (ticket.TicketImage.HasValue())
            {
                image = _url.WithAbsoluteUrl().Image(ticket.TicketImage, _clientConfig.TicketImageDimensions);
            }
            
            return new EventTicketPrintViewModel
            {
                TicketName = ticket.TicketName,
                TicketNumber = ticket.EventBookingTicketId.ToString(),
                EventPhoto = adDetails.PrimaryImage,
                EventName = adDetails.Heading.TruncateOnWordBoundary(40),
                Location = eventDetails.VenueNameAndLocation,
                StartDateTime = eventDetails.EventStartDate.GetValueOrDefault().ToString("dddd d MMM h:mmtt"),
                Price = ticket.TotalPrice,
                ContactNumber = adDetails.ContactPhone,
                GroupName = group.With(g => g.GroupName),
                GuestFullName = ticket.GuestFullName,
                GuestEmail = ticket.GuestEmail,
                BarcodeImageDocumentId = ticket.BarcodeImageDocumentId.GetValueOrDefault().ToString(),
                BrandName = brandName,
                BrandUrl = brandUrl,
                SeatNumber = ticket.SeatNumber,
                OrganiserName = adDetails.ContactName,
                TicketImage = image
            };
        }
    }
}