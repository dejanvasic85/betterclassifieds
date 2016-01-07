using System.Linq;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Business.Payment
{
    public class EventBookingPayPalRequestFactory : IPayPalRequestFactory<EventBooking>
    {
        public PaymentRequest CreatePaymentRequest(EventBooking model, string payReference, string returnUrl, string cancelUrl)
        {
            var lineItems = model.EventBookingTickets
                .Select(t => new ChargeableItem(t.TicketName, t.Price.GetValueOrDefault(), "AUD", 1, t.EventTicketId.ToString()))
                .ToList();

            return new PaymentRequest
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