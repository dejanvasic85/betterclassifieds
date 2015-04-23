namespace Paramount.Betterclassifieds.Business
{
    public interface IInvoiceService
    {
        Invoice GenerateBookingInvoice(int bookingId);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceFactory _invoiceFactory;

        public InvoiceService(IInvoiceRepository invoiceRepository, IInvoiceFactory invoiceFactory)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceFactory = invoiceFactory;
        }

        public Invoice GenerateBookingInvoice(int bookingId)
        {
            var orders = _invoiceRepository.GetInvoiceData(bookingId);

            // Use the invoice factory to convert the order in to an invoice
            var invoice = _invoiceFactory.CreateInvoice(orders);

            return invoice;
        }
    }
}
