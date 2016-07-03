using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Api.Models;
using Paramount.Betterclassifieds.Presentation.Api.Models.Events;

namespace Paramount.Betterclassifieds.Presentation.Api.Factories
{
    public class EventContractFactory : IMappingBehaviour
    {
        public EventContract FromResult(EventSearchResult eventSearchResult)
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