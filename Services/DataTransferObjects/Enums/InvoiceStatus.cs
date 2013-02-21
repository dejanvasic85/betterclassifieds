using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Enums
{
    public enum InvoiceStatus
    {
        Created,
        SubmitForPayment,
        Paid,
        UnPaid,
        Failed,
        Cancelled
    }
}
