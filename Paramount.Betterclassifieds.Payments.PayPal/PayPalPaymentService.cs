using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Payment;
using PayPal;
using PayPal.Api.Payments;

namespace Paramount.Betterclassifieds.Payments.pp
{
    public class PayPalPaymentService : IPaymentService
    {
        public PaymentResponse SubmitPayment(PaymentRequest request)
        {
            var apiContext = ApiContextFactory.CreateApiContext();
            var converter = new ChargeableItemsToPaypalConverter();
            var paypalItems = converter.Convert(request.BookingRateResult);


            // ###Payer
            // A resource representing a Payer that funds a payment
            // Payment Method
            // as `paypal`
            var payer = new Payer { payment_method = "paypal" };

            // # Redirect URLS
            var redirUrls = new RedirectUrls
            {
                cancel_url = request.CancelUrl,
                return_url = request.ReturnUrl
            };

            // ###Details
            // Let's you specify details of a payment amount.
            var sum = request.BookingRateResult.Total;

            var details = new Details
            {
                tax = "0",
                shipping = "0",
                subtotal = sum.ToString("N"),
            };

            // ###Amount
            // Let's you specify a payment amount.
            var amount = new Amount
            {
                currency = "AUD",
                total = sum.ToString("N"), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it. 
            var transactionList = new List<Transaction>
            {
                new Transaction
                {
                    description = "Testing out the paypal API Integration.",
                    amount = amount,
                    item_list = paypalItems
                }
            };

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List

            // ###Payment
            // A Payment Resource; create one using
            // the above types and intent as `sale` or `authorize`
            var payment = new Payment
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            var myResponse = payment.Create(apiContext);

            if (myResponse == null || myResponse.links == null)
                return new PaymentResponse { Status = PaymentStatus.Failed };

            var approvalUrl = myResponse.links.FirstOrDefault(lnk => lnk.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase));
            if (approvalUrl == null)
                return new PaymentResponse { Status = PaymentStatus.Failed };

            return new PaymentResponse
            {
                Status = PaymentStatus.ApprovalRequired,
                ApprovalUrl = approvalUrl.href,
                PaymentId = myResponse.id
            };
        }

        public void CompletePayment(PaymentRequest paymentRequest)
        {
            var apiContext = ApiContextFactory.CreateApiContext();
            var payment = new Payment { id = paymentRequest.PayReference };
            var paymentExecution = new PaymentExecution { payer_id = paymentRequest.PayerId };

            payment.Execute(apiContext, paymentExecution);
        }
    }

    public class ChargeableItemsToPaypalConverter
    {
        public ItemList Convert(BookingRateResult bookingRate)
        {
            ItemList list = new ItemList();

            // Use the same reference for all sku's for paypal
            var reference = bookingRate.BookingReference;

            // Online items will be listed separately
            list.items.AddRange(bookingRate.OnlineBookingAdRate.GetItems().Select(li => new Item
            {
                name = li.Name,
                price = li.Price.ToString("N"),
                currency = li.Currency,
                quantity = li.Quantity.ToString(),
                sku = reference
            }));

            if (bookingRate.PrintRates.Count == 0)
                return list;

            // Publications will be line items
            list.items.AddRange(bookingRate.PrintRates.Select(p => new Item
            {
                name = p.Name,
                price = p.Total.ToString("N"),
                currency = "AUD",
                quantity = "1",
                sku = reference
            }));

            return list;

        }


    }
}