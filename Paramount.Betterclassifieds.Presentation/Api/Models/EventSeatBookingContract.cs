using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class EventSeatBookingContract
    {
        public string EventSeatId { get; set; }
        public string SeatNumber { get; set; }
        public bool Available { get; set; }
        public int? EventTicketId { get; set; }
        public EventTicketContract EventTicket { get; set; }
    }

    public class EventSeatBookingContractFactory : IMappingBehaviour
    {
        public EventSeatBookingContract FromModel(EventSeatBooking seatBooking)
        {
            return this.Map<EventSeatBooking, EventSeatBookingContract>(seatBooking);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventSeatBooking, EventSeatBookingContract>()
                .ForMember(m => m.Available, options => options.MapFrom(src => src.IsAvailable()));

            configuration.CreateMap<EventTicket, EventTicketContract>();
        }
    }
}