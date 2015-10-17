using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Payment;
using PayPal.Api.Payments;

namespace Paramount.Betterclassifieds.Payments.pp
{
    public class ChargeableItemsToPaypalConverter
    {
        public ItemList Convert(IPaymentRequest request)
        {

            var bookingOrder = ((AdBookingPaymentRequest)request).BookingOrderResult;
            
            // Use the same reference for all sku's for paypal
            var reference = bookingOrder.BookingReference;

            var list = new ItemList() { items = new List<Item>() };
            list.items.AddRange(bookingOrder.OnlineBookingAdRate.GetItems().Select(li => new Item
            {
                name = li.Name,
                price = li.Price.ToString("N"),
                currency = li.Currency,
                quantity = li.Quantity.ToString(),
                sku = reference
            }));

            if (!bookingOrder.IsPrintIncluded)
                return list;

            // Publications will be line items
            list.items.AddRange(bookingOrder.PrintRates.Select(p => new Item
            {
                name = p.Name,
                price = p.OrderTotal.ToString("N"),
                currency = "AUD",
                quantity = "1",
                sku = reference
            }));

            return list;

        }


    }
}