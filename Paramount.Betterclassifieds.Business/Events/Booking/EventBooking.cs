using System;
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
        public string PromoCode { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? CreatedDateTimeUtc { get; set; }
        public string HowYouHeardAboutEvent { get; set; } // How the user found out about the event

        // Only because of entity framework
        // Only because of entity framework
        public string StatusAsString
        {
            get { return Status.ToString(); }
            set
            {
                EventBookingStatus status;
                if (Enum.TryParse(value, out status))
                {
                    Status = status;
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
                    PaymentMethod = status;
                }
            }
        }

        public decimal Cost { get; set; }
        public decimal TransactionFee { get; set; }
       
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public int TotalCostInCents()
        {
            // We are taking an assumption here that we only deal with currencies with 2 decimal places :(
            return (int)(TotalCost*100);
        }

        public decimal CostAfterDiscount => Cost - DiscountAmount.GetValueOrDefault();


        /// <summary>
        /// The original percentage fee used at the time of the booking. Important to track when prices change.
        /// </summary>
        public decimal FeePercentage { get; set; }

        /// <summary>
        /// The original cents fee used at the time of the booking. Important to track when prices change.
        /// </summary>
        public decimal FeeCents { get; set; }
    }
}