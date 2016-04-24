using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.DataService;
using PayPal.Api;

namespace Paramount.Betterclassifieds.Payments.pp
{
    public class PayPalPayPalService : IPayPalService, IMappingBehaviour
    {
        private readonly IDbContextFactory _contextFactory;
        private readonly IDateService _dateService;

        public PayPalPayPalService(IDbContextFactory contextFactory, IDateService dateService)
        {
            _contextFactory = contextFactory;
            _dateService = dateService;
        }

        public PayPalResponse SubmitPayment(PayPalRequest request)
        {
            var apiContext = ApiContextFactory.CreateApiContext();
            // var converter = new ChargeableItemsToPaypalConverter();
            var paypalItems = new ItemList() { items = new List<Item>()}; 
            paypalItems.items.AddRange(this.MapList<ChargeableItem, Item>(request.ChargeableItems.ToList()));

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
            var sum = request.Total;

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
                    description = "Paramount PayPal Transaction",
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

            if (myResponse?.links == null)
                return new PayPalResponse { Status = PaymentStatus.Failed };

            var approvalUrl = myResponse.links.FirstOrDefault(lnk => lnk.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase));
            if (approvalUrl == null)
                return new PayPalResponse { Status = PaymentStatus.Failed };

            return new PayPalResponse
            {
                Status = PaymentStatus.ApprovalRequired,
                ApprovalUrl = approvalUrl.href,
                PaymentId = myResponse.id
            };
        }

        public void CompletePayment(string payReference, string payerId)
        {
            var apiContext = ApiContextFactory.CreateApiContext();
            var payment = new Payment { id = payReference };
            var paymentExecution = new PaymentExecution { payer_id = payerId };

            payment.Execute(apiContext, paymentExecution);
        }

        public void CompletePayment(string payReference, string payerId, string userId, decimal amount, string title, string description)
        {
            CompletePayment(payReference, payerId);
            using (var context = _contextFactory.CreateClassifiedContext())
            {
                context.Transactions.InsertOnSubmit(new Paramount.Betterclassifieds.DataService.Classifieds.Transaction()
                {
                    Amount = amount,
                    Description = description,
                    Title = title,
                    TransactionDate = _dateService.Now,
                    PaymentMethod = PaymentType.PayPal.ToString(),
                    UserId = userId
                });
                context.SubmitChanges();
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("paypal converter");
            configuration.CreateMap<ChargeableItem, Item>();
        }
    }
}