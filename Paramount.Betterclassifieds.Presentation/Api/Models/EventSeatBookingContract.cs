using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class EventSeatBookingContract
    {
        public string Id { get; set; }
        public string ColourCode { get; set; }
        public string SeatingCategoryName { get; set; }
        public bool NotAvailableToPublic { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime BookedDateUtc { get; set; }
        public int? EventTicketId { get; set; }
        public EventTicketContract EventTicket { get; set; }
    }

    public class EventSeatContractFactory : IMappingBehaviour
    {
        public EventSeatBookingContract FromModel(EventSeatBooking seatBooking)
        {
            return this.Map<EventSeatBooking, EventSeatBookingContract>(seatBooking);
        }

        public IEnumerable<EventSeatBookingContract> FromModels(IEnumerable<EventSeatBooking> seats)
        {
            return seats.Select(FromModel);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventSeatBooking, EventSeatBookingContract>();
            configuration.CreateMap<EventTicket, EventTicketContract>();
        }
    }
}