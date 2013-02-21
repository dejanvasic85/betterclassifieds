using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.DataTransferObjects.Billing.Messages;
using Paramount.Common.DataTransferObjects.Common;
using Paramount.Common.ServiceContracts;

namespace Paramount.Billing.UI
{
    public class DummyProvider : IBillingService
    {
        #region Implementation of IBillingService

        public CreateShoppingCartResonse CreateShoppingCart(CreateShoppingCartRequest request)
        {
            return new CreateShoppingCartResonse();
        }

        public AddShoppingCartItemResponse AddShoppingCartItem(AddShoppingCartItemRequest request)
        {
            return new AddShoppingCartItemResponse();
        }

        public GetShoppingCartDetailResponse GetShoppingCartDetail(GetShoppingCartDetailRequest request)
        {
            return new GetShoppingCartDetailResponse() { Items = new List<ShoppingCartItemEntity>(), ShoppingCart = new ShoppingCartEntity() { } };
        }

        public ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest request)
        {
            return new ProcessPaymentResponse();
        }

        public PaymentSuccessfulResponse PaymentSuccessful(PaymentSuccessfulRequest request)
        {
            return new PaymentSuccessfulResponse();
        }

        public GetAllInvoicesResponse GetAllInvoices(GetAllInvoicesRequest request)
        {
            return new GetAllInvoicesResponse();
        }

        public GenerateInvoiceResponse GenerateInvoice(GenerateInvoiceRequest request)
        {
            return new GenerateInvoiceResponse();
        }

        public GetSettingsResponse GetSettings(GetSettingsRequest request)
        {
            return new GetSettingsResponse() { BillingSettings = new BillingSettingsEntity()
                                                                     {
                                                                         ClientCode = "AUS"
                                                                     } 
            };
        }

        public SaveSettingsResponse SaveSettings(SaveSettingsRequest request)
        {
            return new SaveSettingsResponse();
        }

        public UpdateInvoiceAddressDetailsResponse UpdateInvoiceAddressDetails(UpdateInvoiceAddressDetailsRequest request)
        {
            return new UpdateInvoiceAddressDetailsResponse();
        }

        public InvoicePaidResponse InvoicePaid(InvoicePaidRequest request)
        {
            return new InvoicePaidResponse();
        }

        public GetInvoiceDetailResponse GetInvoiceDetail(GetInvoiceDetailRequest request)
        {
            return new GetInvoiceDetailResponse()
                       {
                           Invoice = new InvoiceEntity()
                                         {
                                             BillingAddress = GetAddress(), 
                                             ClientCode = "ABC", 
                                             DateTimeCreated = DateTime.Now.Subtract(new TimeSpan(1, 1, 1, 1)), 
                                             DateTimeUpdated = DateTime.Now, 
                                             DeliveryAddress = GetAddress(), InvoiceId = Guid.NewGuid(), SessionId = HttpContext.Current.Session.SessionID, Status = "Generated", UserId = Guid.NewGuid().ToString()
                                         }
                       };
        }

        private static AddressDetails GetAddress()
        {
            return new AddressDetails()
                       {
                           Address1 = "Address1",
                           Address2 = "Address2",
                           Country = "Australia",
                           Name = "Name",
                           Postcode = "3000",
                           State = "Victoria"
                       };
        }

        public GetInvoiceItemsResponse GetInvoiceItems(GetInvoiceItemsRequest request)
        {
            return new GetInvoiceItemsResponse() { InvoiceItems = new List<InvoiceItemEntity>() { GetInvoiceItem() } };
        }

        public ConfirmInvoiceResponse ConfirmInvoice(ConfirmInvoiceRequest request)
        {
            throw new NotImplementedException();
        }

        public GetCurrencyListResponse GetCurrencyList(GetCurrencyListRequest request)
        {
            return new GetCurrencyListResponse() { CurrencyList = new List<CurrencyEntity> { new CurrencyEntity() { CurrencyCode = "AUS", CurrencyId = Guid.NewGuid(), CurrencyName = "Australia" }, new CurrencyEntity() { CurrencyCode = "INR", CurrencyId = Guid.NewGuid(), CurrencyName = "Indian Ruppe" } } };
        }

        public GetBankListResponse GetBankList(GetBankListRequest request)
        {
            return new GetBankListResponse(){BankList = new List<BillingBankEntity>(){new BillingBankEntity(){AllowOverride = true, BankId = Guid.NewGuid(), BankName = "NAB", Description = "NAB Bank", GatewayUrl = "www.paramountit.com.au/banktest", GSTRate = 11, RefundPolicyUrl = "www.paramountit.com.au/refundUrl", ReturnLinkText = "Return",ReturnLinkUrl = "www.paramountit.com.au/invoice"}}};
        }

        public UpdateInvoiceStatusResponse UpdateInvoiceStatus(UpdateInvoiceStatusRequest request)
        {
            throw new NotImplementedException();
        }

        private static InvoiceItemEntity GetInvoiceItem()
        {
            return new InvoiceItemEntity() { InvoiceId = Guid.NewGuid(), InvoiceItemId = Guid.NewGuid(), Price = 12, Quantity = 11, Summary = "summary.....", Title = "title" };
        }

        #endregion
    }
}
