using System.Linq;
using Paramount.Betterclassifieds.DataService;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.Common.DataTransferObjects.Billing.Messages;
using Paramount.Common.DataTransferObjects.Enums;
using Paramount.Common.ServiceContracts;
using System;

namespace Paramount.Services
{
    public class BillingService : BaseService, IBillingService
    {
        #region Implementation of IBillingService

        public CreateShoppingCartResonse CreateShoppingCart(CreateShoppingCartRequest request)
        {
            var shoppingCartEntity = new ShoppingCartEntity
                                         {
                                             Title = request.Title,
                                             SessionId = request.SessionId,
                                             DateTimeCreated = DateTime.Now,
                                             DateTimeModified = DateTime.Now,
                                             ClientCode = request.ClientCode,
                                             UserId = request.AuditData.Username,
                                             Status =
                                                 Enum.GetName(typeof(ShoppingCartStatus), ShoppingCartStatus.Created)
                                         };
            var dataService = new ShoppingCartDataProvider(request.ClientCode);
            var id = dataService.Create(shoppingCartEntity);
            return new CreateShoppingCartResonse { ShoppingCartId = id };

        }

        public AddShoppingCartItemResponse AddShoppingCartItem(AddShoppingCartItemRequest request)
        {
            var item = request.ShoppingCartItem;
            var dataService = new ShoppingCartDataProvider(request.ClientCode);
            var id = dataService.AddItem(item);
            return new AddShoppingCartItemResponse() { ShoppingCartItemId = id };
        }

        public GetShoppingCartDetailResponse GetShoppingCartDetail(GetShoppingCartDetailRequest request)
        {
            var dataService = new ShoppingCartDataProvider(request.ClientCode);
            var getShoppingCartDetailResponse = new GetShoppingCartDetailResponse
                                  {
                                      ShoppingCart =
                                          dataService.AsQueryable().FirstOrDefault(
                                              cart => cart.ShoppingCartId == request.ShoppingCartId),
                                      Items = dataService.ItemsAsQueryable(request.ShoppingCartId)
                                  };

            return getShoppingCartDetailResponse;
        }

        public ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest request)
        {
            throw new NotImplementedException();
        }

        public PaymentSuccessfulResponse PaymentSuccessful(PaymentSuccessfulRequest request)
        {
            throw new NotImplementedException();
        }

        public GetAllInvoicesResponse GetAllInvoices(GetAllInvoicesRequest request)
        {
            throw new NotImplementedException();
        }

        public GenerateInvoiceResponse GenerateInvoice(GenerateInvoiceRequest request)
        {
            var dataService = new InvoiceDataProvider(request.ClientCode);
            var generateInvoice = dataService.GenerateInvoice(request.ShoppingCartId, Enum.GetName(typeof(InvoiceStatus), InvoiceStatus.Created));
            dataService.Commit();
            return new GenerateInvoiceResponse() { InvoiceId = generateInvoice };
        }

        GetSettingsResponse IBillingService.GetSettings(GetSettingsRequest request)
        {
            var dataService = new BillingSettingsDataProvider(request.ClientCode);
            return new GetSettingsResponse() { BillingSettings = dataService.GetBillingSettings() };
        }

        SaveSettingsResponse IBillingService.SaveSettings(SaveSettingsRequest request)
        {
            var dataService = new BillingSettingsDataProvider(request.ClientCode);
            var success = dataService.SaveSettings(request.BillingSettings);
            dataService.Commit();
            return new SaveSettingsResponse() { Success = success };
        }

        public UpdateInvoiceAddressDetailsResponse UpdateInvoiceAddressDetails(UpdateInvoiceAddressDetailsRequest request)
        {
            var dataService = new InvoiceDataProvider(request.ClientCode);
            var updateInvoiceAddressDetails = dataService.UpdateInvoiceAddressDetails(request.InvoiceId, request.BillingAddress,
                                                                                      request.DeliveryAddress);
            dataService.Commit();
            return new UpdateInvoiceAddressDetailsResponse()
                       {
                           Success =
                               updateInvoiceAddressDetails
                       };
        }

        public InvoicePaidResponse InvoicePaid(InvoicePaidRequest request)
        {
            var dataService = new InvoiceDataProvider(request.ClientCode);
            var invoicePaid = dataService.UpdateInvoiceStatus(request.InvoiceId, Enum.GetName(typeof(InvoiceStatus), InvoiceStatus.Paid), request.SessionId,request.TotalAmount);
            dataService.Commit();
            return new InvoicePaidResponse()
            {
                Success = invoicePaid
            };
        }

        public GetBankListResponse GetBankList(GetBankListRequest request)
        {
            var dataService = new InvoiceDataProvider(request.ClientCode);
            return new GetBankListResponse()
            {
                BankList =
                    dataService.GetBankList()
            };
        }

        public GetInvoiceDetailResponse GetInvoiceDetail(GetInvoiceDetailRequest request)
        {
            var dataService = new InvoiceDataProvider(request.ClientCode);
            return new GetInvoiceDetailResponse() { Invoice = dataService.GetInvoice(request.InvoiceId) };
        }

        public GetInvoiceItemsResponse GetInvoiceItems(GetInvoiceItemsRequest request)
        {
            var dataService = new InvoiceDataProvider(request.ClientCode);
            return new GetInvoiceItemsResponse() { InvoiceItems = dataService.GetItems(request.InvoiceId) };

        }

        public ConfirmInvoiceResponse ConfirmInvoice(ConfirmInvoiceRequest request)
        {
            var dataService = new InvoiceDataProvider(request.ClientCode);
            dataService.UpdateInvoiceStatus(request.InvoiceId,
                                            Enum.GetName(typeof(InvoiceStatus), InvoiceStatus.SubmitForPayment));
            dataService.UpdatePaymentType(request.InvoiceId, Enum.GetName(typeof(PaymentType), request.PaymentType));
            dataService.Commit();
            return new ConfirmInvoiceResponse(){Success = true};
        }

        public UpdateInvoiceStatusResponse UpdateInvoiceStatus(UpdateInvoiceStatusRequest request)
        {
            var dataService = new InvoiceDataProvider(request.ClientCode);
            dataService.UpdateInvoiceStatus(request.InvoiceId, Enum.GetName(typeof(InvoiceStatus), request.InvoiceStatus));
            dataService.Commit();
            return new UpdateInvoiceStatusResponse() { Success = true };
        }

        public GetCurrencyListResponse GetCurrencyList(GetCurrencyListRequest request)
        {
            var dataService = new CommonDataProvider(request.ClientCode);
            return new GetCurrencyListResponse()
            {
                CurrencyList = dataService.GetCurrencyList()
            };
        }

        #endregion
    }
}
