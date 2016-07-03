using System;

namespace Paramount.Betterclassifieds.Presentation.Api.Models.Events
{
    public class EventContract
    {
        public int? EventId { get; set; }
        public string Location { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }
        public long? TimeZoneDaylightSavingsOffsetSeconds { get; set; }
        public long? TimeZoneUtcOffsetSeconds { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime? ClosingDateUtc { get; set; }
        public string LocationFloorPlanDocumentId { get; set; }
        public string LocationFloorPlanFilename { get; set; }
        public bool IsClosed { get; set; }
        public bool? IncludeTransactionFee { get; set; }

        // Maps to the online ad details
        public int AdId { get; set; }
        public string Heading { get; set; }
        public string HeadingSlug { get; set; }
        public string Description { get; set; }
        public DateTime? BookingDate { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string[] ImageUrls { get; set; }
        public string ParentCategoryName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Address object
        public AddressContract Address { get; set; }
    }
}