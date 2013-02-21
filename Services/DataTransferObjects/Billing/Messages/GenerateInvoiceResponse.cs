using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GenerateInvoiceResponse : BaseResponse
    {
        public Guid InvoiceId { get; set; }
    }
}
