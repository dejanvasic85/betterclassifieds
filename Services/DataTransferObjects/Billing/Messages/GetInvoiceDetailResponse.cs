using System;
using System.Collections.Generic;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetInvoiceDetailResponse: BaseResponse
    {
        public InvoiceEntity Invoice { get; set; }
    }
}