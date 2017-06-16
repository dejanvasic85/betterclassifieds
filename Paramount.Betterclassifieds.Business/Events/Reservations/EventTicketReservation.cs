﻿using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketReservation : ITicketPriceInfo
    {
        public EventTicketReservation()
        {
            TicketFields = new List<EventBookingTicketField>();
        }
        public int? EventTicketReservationId { get; set; }
        public int? EventTicketId { get; set; }
        public EventTicket EventTicket { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string SessionId { get; set; }
        public EventTicketReservationStatus Status { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }

        // Only because of entity framework
        public virtual string StatusAsString
        {
            get => Status.ToString();
            set
            {
                EventTicketReservationStatus status;
                if (Enum.TryParse(value, out status))
                {
                    this.Status = status;
                }
            }
        }

        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public List<EventBookingTicketField> TicketFields { get; set; }
        public decimal? TransactionFee { get; set; }
        public decimal TotalPriceWithTxnFee => Price.GetValueOrDefault() + TransactionFee.GetValueOrDefault();
        public int? EventGroupId { get; set; }
        public bool IsPublic { get; set; }
        public string SeatNumber { get; set; }
        
    }
}