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
            APIContext apiContext = ApiContextFactory.CreateApiContext();

            //var paypalItems = new ItemList
            //{
            //    items = request.BookingProducts.GetItems().Select(li => new Item
            //    {
            //        name = li.Name,
            //        price = li.Price.ToString("N"),
            //        currency = li.Currency,
            //        quantity = li.Quantity.ToString(),
            //        sku = request.PayReference
            //    }).ToList()
            //};
            var converter = new ChargeableItemsToPaypalConverter();
            var paypalItems = converter.Convert(request.BookingProducts);


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
            var sum = request.BookingProducts.Sum(b => b.BookingTotal());

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
        public ItemList Convert(List<BookingProduct> products)
        {
            ItemList list = new ItemList();

            // Use the same reference for all sku's for paypal
            var reference = products.Select(m => m.Reference).Distinct().First();

            // Online items will be listed separately
            var onlineItems = products.SelectMany(b => b.GetItems().OfType<AdChargeItem>());

            list.items.AddRange(onlineItems.Select(li => new Item
            {
                name = li.Name,
                price = li.Price.ToString("N"),
                currency = li.Currency,
                quantity = li.Quantity.ToString(),
                sku = reference
            }));

            // Publications will be grouped


            return list;

        }


    }
}