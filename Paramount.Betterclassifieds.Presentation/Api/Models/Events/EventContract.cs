using System;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

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
        public DateTime? EventStartDateUtc { get; set; }
        public DateTime? EventEndDate { get; set; }
        public DateTime? EventEndDateUtc { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime? ClosingDateUtc { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? OpeningDateUtc { get; set; }

        public string LocationFloorPlanDocumentId { get; set; }
        public string LocationFloorPlanFilename { get; set; }
        public bool IsClosed { get; set; }
        public bool? IncludeTransactionFee { get; set; }
        public bool? GroupsRequired { get; set; }

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
        public string VenueName { get; set; }
    }

    public class EventContractFactory : IMappingBehaviour
    {
        public EventContract FromModel(EventSearchResult eventSearchResult)
        {
            Guard.NotNullIn(eventSearchResult, eventSearchResult.EventDetails,
                eventSearchResult.Address,
                eventSearchResult.AdSearchResult);

            var contract = this.Map<EventModel, EventContract>(eventSearchResult.EventDetails);
            this.Map(eventSearchResult.AdSearchResult, contract);
            this.Map(eventSearchResult.Address, contract.Address);

            return contract;
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile(this.GetType().Name);
            configuration.CreateMap<EventModel, EventContract>();
            configuration.CreateMap<AdSearchResult, EventContract>();
            configuration.CreateMap<Address, AddressContract>();
        }
    }
}