﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    //public class EventGuestNotificationFactory
    //{
    //    public EventGuestNotification Create(HttpContextBase httpContext, IClientConfig config, EventModel eventModel, EventBookingTicket eventBookingTicket, AdSearchResult ad, IEnumerable<EventGroup> eventGroups, string eventUrl, string purchaserName, byte[] ticketAttachment)
    //    {
    //        var urlHelper = new UrlHelper(httpContext.Request.RequestContext);

    //        var notification = new EventGuestNotification
    //        {
    //            EventName = ad.Heading,
    //            EventUrl = eventUrl,
    //            PurchaserName = purchaserName,
    //            ClientName = config.ClientName,
    //            Location = eventModel.Location,
    //            EventStartDate = $"{eventModel.EventStartDate:dd-MMM-yyyy h:mm tt} {eventModel.TimeZoneName}",
    //            EventEndDate = $"{eventModel.EventEndDate:dd-MMM-yyyy h:mm tt} {eventModel.TimeZoneName}",
    //            BarcodeImgUrl = urlHelper.Image(eventBookingTicket.BarcodeImageDocumentId.GetValueOrDefault().ToString()).WithFullUrl(),
    //            TicketType = eventBookingTicket.TicketName,
    //            GuestEmail = eventBookingTicket.GuestEmail,
    //            GuestFullName = eventBookingTicket.GuestFullName,
    //            GroupName = eventGroups?.FirstOrDefault(g => g.EventGroupId == eventBookingTicket.EventGroupId)?.GroupName.Default("None")
    //        };

    //        var calendarAttachmentContent = new AttachmentFactory().CreateCalendarInvite(config.ClientName,
    //            eventModel.EventId.GetValueOrDefault(),
    //            ad.Heading,
    //            ad.Description,
    //            eventModel.EventStartDate.GetValueOrDefault(),
    //            eventModel.EventEndDate.GetValueOrDefault(),
    //            eventBookingTicket.GuestEmail,
    //            eventModel.Location,
    //            eventModel.LocationLatitude.GetValueOrDefault(),
    //            eventModel.LocationLongitude.GetValueOrDefault(),
    //            eventUrl,
    //            eventModel.TimeZoneId);

    //        notification.WithCalendarInvite(calendarAttachmentContent).WithTicket(ticketAttachment);

    //        return notification;
    //    }

    //    public EventGuestResendNotification CreateGuestResendNotification(HttpContextBase httpContext, IClientConfig config, EventModel eventModel, EventBookingTicket eventBookingTicket, AdSearchResult ad, IEnumerable<EventGroup> eventGroups, string eventUrl, string purchaserName, byte[] ticketAttachment)
    //    {
    //        var urlHelper = new UrlHelper(httpContext.Request.RequestContext);
            
    //        var notification = new EventGuestResendNotification
    //        {
    //            EventName = ad.Heading,
    //            EventUrl = eventUrl,
    //            PurchaserName = purchaserName,
    //            ClientName = config.ClientName,
    //            Location = eventModel.Location,
    //            EventStartDate = $"{eventModel.EventStartDate:dd-MMM-yyyy h:mm tt} {eventModel.TimeZoneName}",
    //            EventEndDate = $"{eventModel.EventEndDate:dd-MMM-yyyy h:mm tt} {eventModel.TimeZoneName}",
    //            BarcodeImgUrl = urlHelper.Image(eventBookingTicket.BarcodeImageDocumentId.GetValueOrDefault().ToString()).WithFullUrl(),
    //            TicketType = eventBookingTicket.TicketName,
    //            GuestEmail = eventBookingTicket.GuestEmail,
    //            GuestFullName = eventBookingTicket.GuestFullName,
    //            GroupName = eventGroups?.FirstOrDefault(g => g.EventGroupId == eventBookingTicket.EventGroupId)?.GroupName.Default("None")
    //        };

    //        var calendarAttachmentContent = new AttachmentFactory().CreateCalendarInvite(config.ClientName,
    //            eventModel.EventId.GetValueOrDefault(),
    //            ad.Heading,
    //            ad.Description,
    //            eventModel.EventStartDate.GetValueOrDefault(),
    //            eventModel.EventEndDate.GetValueOrDefault(),
    //            eventBookingTicket.GuestEmail,
    //            eventModel.Location,
    //            eventModel.LocationLatitude.GetValueOrDefault(),
    //            eventModel.LocationLongitude.GetValueOrDefault(),
    //            eventUrl,
    //            eventModel.TimeZoneId);

    //        notification.WithCalendarInvite(calendarAttachmentContent).WithTicket(ticketAttachment);

    //        return notification;
    //    }

    //    public EventGuestRemovedNotification CreateGuestRemovedNotification(UrlHelper url, AdSearchResult ad, EventModel eventModel, EventBookingTicket eventBookingticket)
    //    {
    //        return new EventGuestRemovedNotification
    //        {
    //            EventBookingTicketId = eventBookingticket.EventBookingTicketId,
    //            EventUrl = url.AdUrl(ad.HeadingSlug, ad.AdId, ad.CategoryAdType).WithFullUrl().ToString(),
    //            EventName = ad.Heading,
    //            EventDate = eventModel.EventStartDate.GetValueOrDefault().ToString("dd-MMM-yyy hh:mm tt")
    //        };
    //    }
    //}
}