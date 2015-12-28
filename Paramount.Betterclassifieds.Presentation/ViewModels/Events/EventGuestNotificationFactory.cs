using System.Linq;
using System.Web;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventGuestNotificationFactory
    {
        public EventGuestNotification Create(HttpContextBase httpContext, IClientConfig config, 
            EventModel eventModel, AdSearchResult ad, string eventUrl, string purchaser)
        {
            var notification = new EventGuestNotification
             {
                EventName = ad.Heading,
                EventUrl = eventUrl,
                PurchaserName = purchaser,
                ClientName = config.ClientName,
                Location = eventModel.Location,
                EventStartDate = eventModel.EventStartDate.GetValueOrDefault().ToString("dd-MMM-yyyy hh:mm tt"),
                EventEndDate = eventModel.EventStartDate.GetValueOrDefault().ToString("dd-MMM-yyyy hh:mm tt")
            };

            var calendarAttachmentContent = new AttachmentFactory().CreateCalendarInvite(config.BrandName,
                eventModel.EventId.GetValueOrDefault(),
                ad.Heading,
                ad.Description,
                eventModel.EventStartDate.GetValueOrDefault(),
                eventModel.EventEndDate.GetValueOrDefault(),
                config.SupportEmailList.First(),
                eventModel.Location,
                eventModel.LocationLatitude.GetValueOrDefault(),
                eventModel.LocationLongitude.GetValueOrDefault());

            notification.WithCalendarInvite(calendarAttachmentContent);

            return notification;
        }
    }
}