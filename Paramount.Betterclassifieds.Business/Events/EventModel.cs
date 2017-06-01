using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventModel : ICategoryAd
    {
        public EventModel()
        {
            Tickets = new List<EventTicket>();
            EventBookings = new List<EventBooking>();
            Address = new Address();
            IncludeTransactionFee = true;
        }

        public int? EventId { get; set; }
        public int OnlineAdId { get; set; }
        public string Location { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }
        public long? TimeZoneDaylightSavingsOffsetSeconds { get; set; }
        public long? TimeZoneUtcOffsetSeconds { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventStartDateUtc { get; set; }
        public DateTime? EventEndDate { get; set; }
        public DateTime? EventEndDateUtc { get; set; }
        public IList<EventTicket> Tickets { get; set; }
        public IList<EventBooking> EventBookings { get; set; }
        public IList<EventOrganiser> EventOrganisers { get; set; }

        public string LocationFloorPlanDocumentId { get; set; }
        public string LocationFloorPlanFilename { get; set; }

        public Address Address { get; set; }
        public long? AddressId { get; set; }

        /// <summary>
        /// If true then the customer will absorb the fee payment
        /// </summary>
        public bool? IncludeTransactionFee { get; set; }
        public string VenueName { get; set; }
        public bool? GroupsRequired { get; set; }

        public string VenueNameAndLocation => VenueName.HasValue()
            ? $"{VenueName} - {LocationWithoutAustralia}"
            : LocationWithoutAustralia;

        public string LocationWithoutAustralia => Location.Replace(", Australia", string.Empty);


        // Ticket Availability Dates
        public DateTime? ClosingDate { get; set; }
        public DateTime? ClosingDateUtc { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? OpeningDateUtc { get; set; }
        public bool IsClosed => this.ClosingDateUtc.HasValue && this.ClosingDateUtc <= DateTime.UtcNow;

        public bool DisplayGuests { get; set; }
        public bool? IsSeatedEvent { get; set; }
    }
}
