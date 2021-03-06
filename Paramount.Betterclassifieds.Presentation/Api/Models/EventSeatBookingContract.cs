﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class EventSeatBookingContract
    {
        public string EventSeatId { get; set; }
        public string SeatNumber { get; set; }
        public bool Available => !IsBooked;
        public bool IsBooked { get; set; }
        public int? EventTicketId { get; set; }
        public EventTicketContract EventTicket { get; set; }
    }

    public class EventSeatBookingContractFactory : IMappingBehaviour
    {
        public EventSeatBookingContract FromModel(EventSeat seat)
        {
            return this.Map<EventSeat, EventSeatBookingContract>(seat);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventSeat, EventSeatBookingContract>()
                .ForMember(m => m.EventSeatId, options => options.MapFrom(src => src.EventSeatId));

            configuration.CreateMap<EventTicket, EventTicketContract>();
        }
    }
}