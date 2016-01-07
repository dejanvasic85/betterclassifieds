using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Payment
{
    public class AdBookingPayPalRequestFactory : IPayPalRequestFactory<BookingOrderResult>
    {
        public PaymentRequest CreatePaymentRequest(BookingOrderResult model, string payReference, string returnUrl, string cancelUrl)
        {
            var reference = model.BookingReference;
            var items = AddOnlineRates(model, reference);
            items.AddRange(AddPrintRates(model, reference));

            return new PaymentRequest
            {
                PayReference = payReference,
                ReturnUrl = returnUrl,
                CancelUrl = cancelUrl,
                ChargeableItems = items,
                Total = model.Total
            };
        }

        private List<ChargeableItem> AddOnlineRates(BookingOrderResult bookingOrder, string sku)
        {
            var list = new List<ChargeableItem>();
            
            list.AddRange(bookingOrder.OnlineBookingAdRate.GetItems().Select(li => new ChargeableItem(li.Name, li.Price, li.Currency, li.Quantity, sku)));

            return list;
        }

        private List<ChargeableItem> AddPrintRates(BookingOrderResult bookingOrder, string sku)
        {
            var list = new List<ChargeableItem>();
            if (!bookingOrder.IsPrintIncluded)
                return list;

            // Publications will be line items
            list.AddRange(bookingOrder.PrintRates.Select(p => new ChargeableItem(p.Name, p.OrderTotal, "AUD", quantity:1 , sku: sku)));

            return list;
        }

    }
}