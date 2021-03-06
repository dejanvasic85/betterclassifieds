﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class GuestContract
    {
        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public IList<EventBookingTicketField> DynamicFields { get; set; }
        public string BarcodeData { get; set; }
        public string TicketName { get; set; }
        public int TicketNumber { get; set; }
        public decimal TotalTicketPrice { get; set; }
        public DateTime DateOfBooking { get; set; }
        public DateTime DateOfBookingUtc { get; set; }
        public int TicketId { get; set; }
        public string GroupName { get; set; }
        public bool IsPublic { get; set; }
    }

    public class GuestViewContract
    {
        public string GuestName { get; set; }
        public string GroupName { get; set; }
    }

    public class GuestContractFactory : IMappingBehaviour
    {
        public GuestContract FromEventGuestDetails(EventGuestDetails eventGuestDetails)
        {
            return this.Map<EventGuestDetails, GuestContract>(eventGuestDetails);
        }
        public GuestViewContract FromEventPublicGuestDetails(EventGuestPublicView eventGuestDetails)
        {
            return this.Map<EventGuestPublicView, GuestViewContract>(eventGuestDetails);
        }


        public IEnumerable<GuestContract> FromEventGuestDetails(IEnumerable<EventGuestDetails> eventGuests)
        {
            return eventGuests.Select(FromEventGuestDetails);
        }

        public IEnumerable<GuestViewContract> FromEventPublicGuestDetails(IEnumerable<EventGuestPublicView> eventGuests)
        {
            return eventGuests.Select(FromEventPublicGuestDetails);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventGuestDetails, GuestContract>();
            configuration.CreateMap<EventGuestPublicView, GuestViewContract>();
        }
    }

}