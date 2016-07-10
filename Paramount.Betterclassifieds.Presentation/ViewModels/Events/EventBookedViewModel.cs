using System;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventBookedViewModel
    {
        public EventBookedViewModel()
        { }

        public EventBookedViewModel(AdSearchResult adDetails, EventModel eventDetails, EventBooking eventBooking, UrlHelper urlHelper, IClientConfig clientConfig, HttpContextBase httpContext)
        {
            EventName = adDetails.Heading;
            CustomerEmailAddress = eventBooking.Email;
            CustomerFirstName = eventBooking.FirstName;
            CustomerLastName = eventBooking.LastName;
            OrganiserName = adDetails.ContactName;
            OrganiserEmail = adDetails.ContactPhone;
            Address = eventDetails.Location;
            LocationLatitude = eventDetails.LocationLatitude;
            LocationLongitude = eventDetails.LocationLongitude;
            StartDateTime = eventDetails.EventStartDate.GetValueOrDefault();
            EndDateTime = eventDetails.EventEndDate.GetValueOrDefault();
            EventPhoto = adDetails.PrimaryImage;

            EventUrl = urlHelper.AdUrl(adDetails.HeadingSlug, adDetails.AdId, adDetails.CategoryAdType).WithFullUrl();
            EventPhotoUrl = urlHelper.ImageOriginal(adDetails.PrimaryImage).WithFullUrl();
            Title = adDetails.Heading;
            Description = adDetails.Description;
            SocialShareText = "This looks good '" + httpContext.Server.HtmlEncode(adDetails.Heading) + "'";

            FacebookAppId = clientConfig.FacebookAppId;

            // Seating/Group management
            GroupSelectionViewModel = new GroupSelectionViewModel(eventBooking);
        }

        public string FacebookAppId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string SocialShareText { get; set; }
        public string EventPhotoUrl { get; set; }
        public string EventPhoto { get; set; }
        public string EventName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string OrganiserName { get; set; }
        public string OrganiserEmail { get; set; }
        public string Address { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string EventUrl { get; set; }
        public GroupSelectionViewModel GroupSelectionViewModel { get; set; }
    }
}