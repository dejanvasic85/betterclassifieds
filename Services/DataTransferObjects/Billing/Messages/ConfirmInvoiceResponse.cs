using System;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class ConfirmInvoiceResponse: BaseResponse
    {
        public bool Success { get; set; }
    }
}