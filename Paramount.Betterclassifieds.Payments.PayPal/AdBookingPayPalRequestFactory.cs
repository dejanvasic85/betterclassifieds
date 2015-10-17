using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Payment
{
    public class AdBookingPayPalRequestFactory : IPayPalRequestFactory<BookingOrderResult>
    {
        public PayPalPaymentRequest CreatePaymentRequest(BookingOrderResult model, string payReference, string returnUrl, string cancelUrl)
        {
            var reference = model.BookingReference;
            var items = AddOnlineRates(model, reference);
            items.AddRange(AddPrintRates(model, reference));

            return new PayPalPaymentRequest
            {
                PayReference = payReference,
                ReturnUrl = returnUrl,
                CancelUrl = cancelUrl,
                ChargeableItems = items,
                Total = model.Total
            };
        }

        private List<PayPalChargeableItem> AddOnlineRates(BookingOrderResult bookingOrder, string sku)
        {
            var list = new List<PayPalChargeableItem>();
            
            list.AddRange(bookingOrder.OnlineBookingAdRate.GetItems().Select(li => new PayPalChargeableItem(li.Name, li.Price, li.Currency, li.Quantity, sku)));

            return list;
        }

        private List<PayPalChargeableItem> AddPrintRates(BookingOrderResult bookingOrder, string sku)
        {
            var list = new List<PayPalChargeableItem>();
            if (!bookingOrder.IsPrintIncluded)
                return list;

            // Publications will be line items
            list.AddRange(bookingOrder.PrintRates.Select(p => new PayPalChargeableItem(p.Name, p.OrderTotal, "AUD", quantity:1 , sku: sku)));

            return list;
        }

    }
}