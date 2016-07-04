using System;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Api.Models.Events
{
    public class EventGroupContract
    {
        public int? EventGroupId { get; set; }
        public int? EventId { get; set; }
        public string GroupName { get; set; }
        public int? MaxGuests { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? CreatedDateTimeUtc { get; set; }
        public int GuestCount { get; set; }
        public bool AvailableToAllTickets { get; set; }
    }

    public class EventGroupContractFactory : IMappingBehaviour
    {
        public EventGroupContract FromModel(EventGroup eventGroup)
        {
            Guard.NotNull(eventGroup);
            return this.Map<EventGroup, EventGroupContract>(eventGroup);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile(this.GetType().Name);
            configuration.CreateMap<EventGroup, EventGroupContract>();
        }
    }
}