using System.Linq;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Business.Payment
{
    public class EventBookingPayPalRequestFactory : IPayPalRequestFactory<EventBooking>
    {
        public PayPalPaymentRequest CreatePaymentRequest(EventBooking model, string payReference, string returnUrl, string cancelUrl)
        {
            var lineItems = model.EventBookingTickets
                .Select(t => new PayPalChargeableItem(t.TicketName, t.Price.GetValueOrDefault(), "AUD", t.Quantity, t.EventTicketId.ToString()))
                .ToList();

            return new PayPalPaymentRequest
            {
                PayReference = payReference,
                ReturnUrl = returnUrl,
                CancelUrl = cancelUrl,
                Total = model.TotalCost,
                ChargeableItems = lineItems
            };
        }
    }
}