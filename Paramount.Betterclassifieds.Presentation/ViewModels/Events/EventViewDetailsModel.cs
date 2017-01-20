using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    /// <summary>
    /// Used for viewing the event ad
    /// </summary>
    public class EventViewDetailsModel : IMappingBehaviour
    {
        public EventViewDetailsModel()
        {

        }

        public EventViewDetailsModel(HttpContextBase httpContext, UrlHelper urlHelper, AdSearchResult searchResult, EventModel eventModel, IClientConfig clientConfig, IEnumerable<EventGuestDetails> guestList)
        {
            AdId = searchResult.AdId;
            Title = searchResult.Heading;
            TitleSlug = searchResult.HeadingSlug;
            EventUrl = urlHelper.AdUrl(searchResult.HeadingSlug, searchResult.AdId, searchResult.CategoryAdType).WithFullUrl();
            HtmlText = searchResult.HtmlText;
            Description = searchResult.Description;
            EventPhoto = searchResult.PrimaryImage;
            EventPhotoUrl = urlHelper.ImageOriginal(searchResult.PrimaryImage).WithFullUrl();
            OrganiserName = searchResult.ContactName;
            OrganiserPhone = searchResult.ContactPhone;
            Views = searchResult.NumOfViews;
            SocialShareText = "This looks good '" + httpContext.Server.HtmlEncode(searchResult.Heading) + "'";
            IsClosed = eventModel.IsClosed;

            EventId = eventModel.EventId.GetValueOrDefault();
            Location = eventModel.Location;
            LocationFriendlyName = eventModel.VenueNameAndLocation;
            LocationLatitude = eventModel.LocationLatitude;
            LocationLongitude = eventModel.LocationLongitude;
            EventStartDate = eventModel.EventStartDate.GetValueOrDefault();
            EventEndDate = eventModel.EventEndDate.GetValueOrDefault();
            EventStartDateDisplay = eventModel.EventStartDate.GetValueOrDefault().ToLongDateString();
            EventStartTime = eventModel.EventStartDate.GetValueOrDefault().ToString("hh:mm tt");
            EventEndDateDisplay = eventModel.EventStartDate.GetValueOrDefault().ToLongDateString();
            EventEndTime = eventModel.EventEndDate.GetValueOrDefault().ToString("hh:mm tt");

            FacebookAppId = clientConfig.FacebookAppId;
            MaxTicketsPerBooking = clientConfig.EventMaxTicketsPerBooking;

            LocationFloorPlanFilename = eventModel.LocationFloorPlanFilename;
            LocationFloorPlanDocumentId = eventModel.LocationFloorPlanDocumentId;
            TicketingEnabled = eventModel.Tickets != null && eventModel.Tickets.Any();

            // Build the guest list
            Guests = this.MapList<EventGuestDetails, EventGuestListViewModel>(guestList.ToList());
        }
        
        public DateTime EventEndDate { get; set; }

        public DateTime EventStartDate { get; set; }

        public int AdId { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
        public string HtmlText { get; set; }
        public string Location { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        public string EventPhoto { get; set; }
        public string EventStartDateDisplay { get; set; }
        public string EventStartTime { get; set; }
        public string EventEndDateDisplay { get; set; }
        public string EventEndTime { get; set; }
        public string OrganiserName { get; set; }
        public string OrganiserPhone { get; set; }
        public int Views { get; set; }
        public string Posted { get; set; }
        public bool IsClosed { get; set; }
        public string LocationFloorPlanDocumentId { get; set; }
        public string LocationFloorPlanFilename { get; set; }
        public int MaxTicketsPerBooking { get; set; }
        public string Description { get; set; }
        public string TitleSlug { get; set; }
        public string CategoryEventType { get; set; }
        public string FacebookAppId { get; set; }
        public string EventPhotoUrl { get; set; }
        public string EventUrl { get; set; }
        public string SocialShareText { get; set; }
        public string LocationFriendlyName { get; set; }

        public bool TicketingEnabled { get; set; }

        public List<EventGuestListViewModel> Guests { get; set; }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventGuestDetails, EventGuestListViewModel>();
        }
    }
}