namespace Paramount.Betterclassifieds.Business
{
    public interface IInvoiceRepository
    {
        Invoice GetInvoiceDataForBooking(int bookingId);
    }
}