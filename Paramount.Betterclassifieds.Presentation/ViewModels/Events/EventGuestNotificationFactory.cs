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
                EventStartDate = eventModel.EventStartDate.GetValueOrDefault().ToString("dd-MMM-yyyy h:mm tt"),
                EventEndDate = eventModel.EventEndDate.GetValueOrDefault().ToString("dd-MMM-yyyy h:mm tt")
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
                eventUrl);

            notification.WithCalendarInvite(calendarAttachmentContent);

            return notification;
        }
    }
}