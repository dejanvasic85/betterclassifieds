﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicket : ITicketPriceInfo
    {
        public EventBookingTicket()
        {
            IsActive = true;
            TicketFieldValues = new List<EventBookingTicketField>();
        }
        public int EventBookingTicketId { get; set; }
        public int EventBookingId { get; set; }
        public EventBooking EventBooking { get; set; }
        public int EventTicketId { get; set; }
        public EventTicket EventTicket { get; set; }
        public string TicketName { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? CreatedDateTimeUtc { get; set; }
        public List<EventBookingTicketField> TicketFieldValues { get; set; }
        public decimal? TransactionFee { get; set; }
        public decimal TotalPrice { get; set; }
        public int? EventGroupId { get; set; }
        public EventGroup EventGroup { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastModifiedDateUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public Guid? BarcodeImageDocumentId { get; set; }
        public Guid? TicketDocumentId { get; set; }
        public bool IsPublic { get; set; }
        public string SeatNumber { get; set; }
        public string TicketImage { get; set; }

        public override string ToString()
        {
            var builder =  new StringBuilder(this.GuestFullName);

            builder.AppendFormat(" - {0}", GuestEmail);
            builder.AppendFormat(" - {0}", TicketName);

            if (SeatNumber.HasValue())
            {
                builder.AppendFormat(" - {0}", SeatNumber);
            }


            return builder.ToString();
        }
    }
}