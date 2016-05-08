﻿using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBooking
    {
        public EventBooking()
        {
            EventBookingTickets = new List<EventBookingTicket>();
        }
        public int EventBookingId { get; set; }
        public int EventId { get; set; }
        public EventModel Event { get; set; }
        public IList<EventBookingTicket> EventBookingTickets { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public decimal TotalCost { get; set; }
        public PaymentType PaymentMethod { get; set; }
        public string PaymentReference { get; set; }
        public EventBookingStatus Status { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? CreatedDateTimeUtc { get; set; }

        // Only because of entity framework
        public string StatusAsString
        {
            get { return Status.ToString(); }
            set
            {
                EventBookingStatus status;
                if (Enum.TryParse(value, out status))
                {
                    this.Status = status;
                }
            }
        }

        public string PaymentMethodAsString
        {
            get { return PaymentMethod.ToString(); }
            set
            {
                PaymentType status;
                if (Enum.TryParse(value, out status))
                {
                    this.PaymentMethod = status;
                }
            }
        }

        public Guid? TicketsDocumentId { get; set; }
        public DateTime? TicketsSentDate { get; set; }
        public DateTime? TicketsSentDateUtc { get; set; }
        public decimal Cost { get; set; }
        public decimal TransactionFee { get; set; }

        public string GetFullName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

        public int TotalCostInCents()
        {
            // We are taking an assumption here that we only deal with currencies with 2 decimal places :(
            return (int)(TotalCost*100);
        }
    }
}