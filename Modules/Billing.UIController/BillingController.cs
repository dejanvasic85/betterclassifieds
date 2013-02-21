using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Paramount.Common.DataTransferObjects.Billing.Messages;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.DataTransferObjects.Enums;
using Paramount.Common.ServiceContracts;
using Paramount.Services.Proxy;
using Paramount.Common.DataTransferObjects.Common;

namespace Paramount.Billing.UIController
{
    public class BillingController
    {
        protected IBillingService billingService;

        public BillingController()
        {
            this.billingService = WebServiceHostManager.BillingServiceClient;
        }

        public BillingController(IBillingService billingService)
        {
            this.billingService = billingService;
        }

        #region Shopping Cart

        public Guid CreateShoppingCart(string title)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new CreateShoppingCartRequest()
                              {
                                  SessionId = HttpContext.Current.Session.SessionID,
                                  Title = title

                              };
            request.SetBaseRequest(groupId);
            var response = this.billingService.CreateShoppingCart(request);
            return response.ShoppingCartId;
        }

        public Guid AddItemToCart(Guid shoppingCartId, decimal price, string referenceId, string productType, decimal quantity, string summary, string title)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new AddShoppingCartItemRequest()
            {
                ShoppingCartItem = new ShoppingCartItemEntity()
                                       {
                                           Price = price,
                                           ReferenceId = referenceId,
                                           ProductType = productType,
                                           Quantity = quantity,
                                           Summary = summary,
                                           Title = title,
                                           ShoppingCartId = shoppingCartId
                                       }

            };
            request.SetBaseRequest(groupId);
            var response = this.billingService.AddShoppingCartItem(request);
            return response.ShoppingCartItemId;
        }

        #endregion

        #region Billing

        public BillingSettingsEntity GetBillingSettings()
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new GetSettingsRequest();
            request.SetBaseRequest(groupId);

            var response = billingService.GetSettings(request);
            return response.BillingSettings;
        }

        public bool SaveBillingSettings(BillingSettingsEntity billingSettingsEntity)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new SaveSettingsRequest() { BillingSettings = billingSettingsEntity };
            request.SetBaseRequest(groupId);
            var response = billingService.SaveSettings(request);
            return response.Success;
        }

        #endregion

        #region Invoice

        public Guid CreateInvoice(Guid shoppingCartId)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new GenerateInvoiceRequest() { ShoppingCartId = shoppingCartId };
            request.SetBaseRequest(groupId);
            var response = this.billingService.GenerateInvoice(request);
            return response.InvoiceId;
        }

        public bool UpdateInvoiceAddress(Guid invoiceId, AddressDetails billingAddress, AddressDetails deliveryAddress)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new UpdateInvoiceAddressDetailsRequest() { InvoiceId = invoiceId, DeliveryAddress = deliveryAddress, BillingAddress = billingAddress };
            request.SetBaseRequest(groupId);
            var response = this.billingService.UpdateInvoiceAddressDetails(request);
            return response.Success;
        }

        public bool MarkInvoiceAsPaid(Guid invoiceId,string sessionId, decimal totalAmount)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new InvoicePaidRequest() { InvoiceId = invoiceId, SessionId = sessionId, TotalAmount = totalAmount};
            request.SetBaseRequest(groupId);
            var response = this.billingService.InvoicePaid(request);
            return response.Success;
        }

        public InvoiceEntity GetInvoiceDetails(Guid invoiceId)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new GetInvoiceDetailRequest() { InvoiceId = invoiceId };
            request.SetBaseRequest(groupId);
            var response = this.billingService.GetInvoiceDetail(request);
            return response.Invoice;
        }

        public List<InvoiceItemEntity> GetInvoiceItems(Guid invoiceId)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new GetInvoiceItemsRequest() { InvoiceId = invoiceId };
            request.SetBaseRequest(groupId);
            var response = this.billingService.GetInvoiceItems(request);
            return response.InvoiceItems;
        }
        #endregion

        public bool ConfirmInvoice(Guid invoiceId, PaymentType optionId)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new ConfirmInvoiceRequest() { InvoiceId = invoiceId, PaymentType = optionId };
            request.SetBaseRequest(groupId);
            var response = this.billingService.ConfirmInvoice(request);
            return response.Success;
        }

     

        public List<CurrencyEntity> GetCurrencyList()
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new GetCurrencyListRequest();
            request.SetBaseRequest(groupId);
            var response = this.billingService.GetCurrencyList(request);
            return response.CurrencyList;
        }


        public List<BillingBankEntity> GetBankList()
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new GetBankListRequest();
            request.SetBaseRequest(groupId);
            var response = this.billingService.GetBankList(request);
            return response.BankList;
        }

        public Guid CheckOut(Guid cartId)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new GenerateInvoiceRequest(){ShoppingCartId = cartId};
            request.SetBaseRequest(groupId);
            var response = this.billingService.GenerateInvoice(request);
            return response.InvoiceId;
        }

        public void InvoiceFailed(Guid invoiceId)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new UpdateInvoiceStatusRequest() { InvoiceId = invoiceId, InvoiceStatus = InvoiceStatus.Failed };
            request.SetBaseRequest(groupId);
            var response = this.billingService.UpdateInvoiceStatus(request);
            return;
        }

        public void InvoiceCancelled(Guid invoiceId)
        {
            var groupId = Guid.NewGuid().ToString();
            var request = new UpdateInvoiceStatusRequest() { InvoiceId = invoiceId, InvoiceStatus = InvoiceStatus.Cancelled };
            request.SetBaseRequest(groupId);
            var response = this.billingService.UpdateInvoiceStatus(request);
            return;
        }
    }
}
