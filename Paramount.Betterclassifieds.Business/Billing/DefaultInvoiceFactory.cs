using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public class DefaultInvoiceFactory : IInvoiceFactory
    {
        public Invoice CreateInvoice(List<InvoiceGroup> invoiceGroups)
        {
            return new Invoice
            {
                InvoiceGroups = invoiceGroups
            };
        }
    }
}