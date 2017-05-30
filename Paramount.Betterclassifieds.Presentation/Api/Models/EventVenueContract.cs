using AutoMapper;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class EventVenueContract
    {
        public int EventId { get; set; }
        public string VenueName { get; set; }

        // Todo - Location and other relevant properties

        public EventRowContract[] Rows { get; set; }

        public EventTicketContract[] Tickets { get; set; }
    }

    public class VenueContractFactory : IMappingBehaviour
    {
        public void OnRegisterMaps(IConfiguration configuration)
        {

        }
    }
}