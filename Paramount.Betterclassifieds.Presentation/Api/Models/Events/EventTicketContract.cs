using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Api.Models.Events
{
    public class EventTicketContract
    {
        public int? EventTicketId { get; set; }
        public int? EventId { get; set; }
        public string TicketName { get; set; }
        public int AvailableQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }

    public class EventTicketContractFactory : IMappingBehaviour
    {
        public EventTicketContract FromModel(EventTicket eventTicket)
        {
            return this.Map<EventTicket, EventTicketContract>(eventTicket);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile(this.GetType().Name);
            configuration.CreateMap<EventTicket, EventTicketContract>();
        }
    }
}