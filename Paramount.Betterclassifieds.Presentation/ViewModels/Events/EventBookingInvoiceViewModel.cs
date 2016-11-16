using System;
using System.Linq;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventBookingInvoiceViewModel
    {
        public EventBookingInvoiceViewModel()
        { }

        public EventBookingInvoiceViewModel(IClientConfig clientConfig, EventBooking eventBooking,
            ApplicationUser applicationUser, string eventName)
        {
            BusinessName = clientConfig.ClientName;
            BusinessAddress = clientConfig.ClientAddress.ToString();
            BusinessPhone = clientConfig.ClientPhoneNumber;
            CreatedDate = DateTime.Now;

            RecipientName = applicationUser.FullName;
            RecipientAddress = applicationUser.FullAddress;
            RecipientPhoneNumber = applicationUser.Phone;

            Fees = eventBooking.TransactionFee;
            Total = eventBooking.TotalCost;
            PaymentReference = eventBooking.PaymentReference;

            LineItems = eventBooking.EventBookingTickets.Select(b => new InvoiceItemViewModel
            {
                Name = string.Format("Event Ticket : {0} - {1}", eventName, b.TicketName),
                Price = b.Price.GetValueOrDefault(),
                Quantity = 1,
                ItemTotal = b.Price.GetValueOrDefault()
            }).ToArray();
        }
        
        public string BusinessName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPhone { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public InvoiceItemViewModel[] LineItems { get; set; }
        public decimal Total { get; set; }
        public decimal Fees { get; set; }
        public string PaymentReference { get; set; }
    }
}