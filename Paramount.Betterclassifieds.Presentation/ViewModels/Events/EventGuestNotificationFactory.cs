using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventGuestNotificationFactory
    {
        public EventGuestNotification Create(IClientConfig config,
            EventModel eventModel, AdSearchResult ad, string eventUrl, string purchaserName, string guestEmail)
        {
            var notification = new EventGuestNotification
            {
                EventName = ad.Heading,
                EventUrl = eventUrl,
                PurchaserName = purchaserName,
                ClientName = config.ClientName,
                Location = eventModel.Location,
                EventStartDate = $"{eventModel.EventStartDate:dd-MMM-yyyy h:mm tt} {eventModel.TimeZoneName}",
                EventEndDate = $"{eventModel.EventEndDate:dd-MMM-yyyy h:mm tt} {eventModel.TimeZoneName}"
            };

            var calendarAttachmentContent = new AttachmentFactory().CreateCalendarInvite(config.ClientName,
                eventModel.EventId.GetValueOrDefault(),
                ad.Heading,
                ad.Description,
                eventModel.EventStartDate.GetValueOrDefault(),
                eventModel.EventEndDate.GetValueOrDefault(),
                guestEmail,
                eventModel.Location,
                eventModel.LocationLatitude.GetValueOrDefault(),
                eventModel.LocationLongitude.GetValueOrDefault(),
                eventUrl,
                eventModel.TimeZoneId);

            notification.WithCalendarInvite(calendarAttachmentContent);

            return notification;
        }

        public EventGuestRemovedNotification CreateGuestRemovedNotification(UrlHelper url, AdSearchResult ad, EventModel eventModel, EventBookingTicket eventBookingticket)
        {
            return new EventGuestRemovedNotification
            {
                EventBookingTicketId = eventBookingticket.EventBookingTicketId,
                EventUrl = url.AdUrl(ad.HeadingSlug, ad.AdId, ad.CategoryAdType).WithFullUrl().ToString(),
                EventName = ad.Heading,
                EventDate = eventModel.EventStartDate.GetValueOrDefault().ToString("dd-MMM-yyy hh:mm tt")
            };
        }
    }
}