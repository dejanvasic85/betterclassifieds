using System;
using System.Security.Policy;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventBookedViewModel
    {
        public EventBookedViewModel()
        { }

        public EventBookedViewModel(AdSearchResult adDetails, EventModel eventDetails, EventBooking eventBooking, UrlHelper urlHelper)
        {
            EventName = adDetails.Heading;
            CustomerEmailAddress = eventBooking.Email;
            CustomerFirstName = eventBooking.FirstName;
            CustomerLastName = eventBooking.LastName;
            OrganiserName = adDetails.ContactName;
            OrganiserEmail = adDetails.ContactPhone;
            EventUrl = urlHelper.AdUrl(adDetails.HeadingSlug, adDetails.AdId, includeSchemeAndProtocol: true, routeName: "Event");
            Address = eventDetails.Location;
            LocationLatitude = eventDetails.LocationLatitude;
            LocationLongitude = eventDetails.LocationLongitude;
            StartDateTime = eventDetails.EventStartDate.GetValueOrDefault();
            EndDateTime = eventDetails.EventEndDate.GetValueOrDefault();
        }

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
    }
}