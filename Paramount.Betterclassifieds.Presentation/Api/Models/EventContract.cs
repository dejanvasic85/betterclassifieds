using System;
using AutoMapper;
using Humanizer;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
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
        public string EventStartDateHumanized { get; set; }
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
        public string PrimaryImage { get; set; }
        public string[] ImageUrls { get; set; }
        public string ParentCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryFontIcon { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool DisplayGuests { get; set; }

        // Address object
        public AddressContract Address { get; set; }
        public string VenueName { get; set; }
        public string EventUrl { get; set; }

        // Ticketing information
        public decimal? CheapestTicket { get; set; }
        public decimal? MostExpensiveTicket { get; set; }
        public bool HasTickets { get; set; }
        public bool AreAllTicketsFree { get; set; }
        public string EventShortName { get; set; }
        public string EditAdUrl { get; internal set; }
    }

    public class EventContractFactory : IMappingBehaviour
    {
        private readonly IUrl _url;

        public EventContractFactory(IUrl url)
        {
            _url = url.WithAbsoluteUrl();
        }

        public EventContract FromModel(EventSearchResult eventSearchResult)
        {
            Guard.NotNullIn(eventSearchResult, eventSearchResult.EventDetails,
                eventSearchResult.Address, eventSearchResult.AdSearchResult);

            var contract = this.Map<EventModel, EventContract>(eventSearchResult.EventDetails);
            this.Map(eventSearchResult.AdSearchResult, contract);
            this.Map(eventSearchResult.TicketData, contract);
            this.Map(eventSearchResult.Address, contract.Address);

            contract.EventStartDateHumanized = eventSearchResult.EventDetails.EventStartDate.Humanize();
            
            contract.EventUrl = _url.EventUrl(eventSearchResult.AdSearchResult.HeadingSlug,
                eventSearchResult.AdSearchResult.AdId);

            contract.EditAdUrl = _url.EventDashboardUrl(eventSearchResult.AdSearchResult.AdId);

            return contract;
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile(this.GetType().Name);
            configuration.CreateMap<EventModel, EventContract>();
            configuration.CreateMap<EventSearchResultTicketData, EventContract>();
            configuration.CreateMap<AdSearchResult, EventContract>()
                .ForMember(m => m.EventShortName, options => options.MapFrom(s => s.Heading.TruncateOnWordBoundary(35)));
            configuration.CreateMap<Address, AddressContract>();

        }
    }
}