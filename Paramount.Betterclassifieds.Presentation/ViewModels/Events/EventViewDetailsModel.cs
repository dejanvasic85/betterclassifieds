namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    /// <summary>
    /// Used for viewing the event ad
    /// </summary>
    public class EventViewDetailsModel
    {
        public int AdId { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
        public string HtmlText { get; set; }
        public string Location { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        public string EventPhoto { get; set; }
        public string EventStartDate { get; set; }
        public string EventStartTime { get; set; }
        public string EventEndDate { get; set; }
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

        public bool TicketingEnabled
        {
            get { return Tickets != null && Tickets.Length > 0; }
        }
       
    }
}