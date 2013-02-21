using System;
using System.Collections.Generic;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetInvoiceItemsResponse : BaseResponse
    {
        public List<InvoiceItemEntity> InvoiceItems { get; set; }
    }
}