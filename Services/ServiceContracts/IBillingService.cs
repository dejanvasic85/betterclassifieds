using System;
using System.ServiceModel;
using Paramount.Common.DataTransferObjects.Banner.Messages;
using Paramount.Common.DataTransferObjects.Billing.Messages;

namespace Paramount.Common.ServiceContracts
{
    [ServiceContract]
    public interface IBillingService
    {
        [OperationContract]
        CreateShoppingCartResonse CreateShoppingCart(CreateShoppingCartRequest request);

        [OperationContract]
        AddShoppingCartItemResponse AddShoppingCartItem(AddShoppingCartItemRequest request);

        [OperationContract]
        GetShoppingCartDetailResponse GetShoppingCartDetail(GetShoppingCartDetailRequest request);

        [OperationContract]
        ProcessPaymentResponse ProcessPayment(ProcessPaymentRequest request);

        [OperationContract]
        PaymentSuccessfulResponse PaymentSuccessful(PaymentSuccessfulRequest request);

        [OperationContract]
        GetAllInvoicesResponse GetAllInvoices(GetAllInvoicesRequest request);

        [OperationContract]
        GenerateInvoiceResponse GenerateInvoice(GenerateInvoiceRequest request);

        [OperationContract]
        GetSettingsResponse GetSettings(GetSettingsRequest request);

        [OperationContract]
        SaveSettingsResponse SaveSettings(SaveSettingsRequest request);

        [OperationContract]
        UpdateInvoiceAddressDetailsResponse UpdateInvoiceAddressDetails(UpdateInvoiceAddressDetailsRequest request);

        [OperationContract]
        InvoicePaidResponse InvoicePaid(InvoicePaidRequest request);

        [OperationContract]
        GetInvoiceDetailResponse GetInvoiceDetail(GetInvoiceDetailRequest request);

        [OperationContract]
        GetInvoiceItemsResponse GetInvoiceItems(GetInvoiceItemsRequest request);

        [OperationContract]
        ConfirmInvoiceResponse ConfirmInvoice(ConfirmInvoiceRequest request);

        [OperationContract]
        GetCurrencyListResponse GetCurrencyList(GetCurrencyListRequest request);

        [OperationContract]
        GetBankListResponse GetBankList(GetBankListRequest request);

        [OperationContract]
        UpdateInvoiceStatusResponse UpdateInvoiceStatus(UpdateInvoiceStatusRequest request);
    }
}