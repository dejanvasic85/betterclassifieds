using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    /// <summary>
    /// Used for viewing the event ad
    /// </summary>
    public class EventViewDetailsModel
    {
        public EventViewDetailsModel()
        {

        }

        public EventViewDetailsModel(HttpContextBase httpContext, UrlHelper urlHelper, AdSearchResult searchResult, EventModel eventModel, IClientConfig clientConfig)
        {
            this.AdId = searchResult.AdId;
            this.Title = searchResult.Heading;
            this.TitleSlug = searchResult.HeadingSlug;
            this.EventUrl = urlHelper.AdUrl(searchResult.HeadingSlug, searchResult.AdId, true, searchResult.CategoryAdType);
            this.HtmlText = searchResult.HtmlText;
            this.Description = searchResult.Description;
            this.EventPhoto = searchResult.PrimaryImage;
            this.EventPhotoUrl = urlHelper.ImageOriginal(searchResult.PrimaryImage).WithFullUrl();
            this.OrganiserName = searchResult.ContactName;
            this.OrganiserPhone = searchResult.ContactPhone;
            this.Views = searchResult.NumOfViews;
            this.SocialShareText = "This looks good '" + httpContext.Server.HtmlEncode(searchResult.Heading) + "'";


            this.EventId = eventModel.EventId.GetValueOrDefault();
            this.Location = eventModel.Location;
            this.LocationFriendlyName = eventModel.Address.ToString();
            this.LocationLatitude = eventModel.LocationLatitude;
            this.LocationLongitude = eventModel.LocationLongitude;
            this.EventStartDate = eventModel.EventStartDate.GetValueOrDefault();
            this.EventEndDate = eventModel.EventEndDate.GetValueOrDefault();
            this.EventStartDateDisplay = eventModel.EventStartDate.GetValueOrDefault().ToLongDateString();
            this.EventStartTime = eventModel.EventStartDate.GetValueOrDefault().ToString("hh:mm tt");
            this.EventEndDateDisplay = eventModel.EventStartDate.GetValueOrDefault().ToLongDateString();
            this.EventEndTime = eventModel.EventEndDate.GetValueOrDefault().ToString("hh:mm tt");

            this.FacebookAppId = clientConfig.FacebookAppId;
            this.MaxTicketsPerBooking = clientConfig.MaxOnlineImages.GetValueOrDefault();

            this.Tickets = eventModel.Tickets.Select(t => new EventTicketViewModel
            {
                EventId = t.EventId,
                AvailableQuantity = t.AvailableQuantity,
                EventTicketId = t.EventTicketId,
                Price = eventModel.IncludeTransactionFee.GetValueOrDefault()
                    ? t.Price + (t.Price * clientConfig.EventTicketFeeDecimal)
                    : t.Price,

                RemainingQuantity = t.RemainingQuantity,
                TicketName = t.TicketName
            }).ToArray();
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
        public EventTicketViewModel[] Tickets { get; set; }
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

        public bool TicketingEnabled
        {
            get { return Tickets != null && Tickets.Length > 0; }
        }

    }
}