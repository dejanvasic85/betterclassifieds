using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public interface IInvoiceFactory
    {
        Invoice CreateInvoice(List<InvoiceGroup> invoiceGroups);
    }
}