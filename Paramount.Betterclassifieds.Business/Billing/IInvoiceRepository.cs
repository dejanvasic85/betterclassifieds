using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public interface IInvoiceRepository
    {
        List<InvoiceGroup> GetInvoiceData(int bookingId);
    }
}