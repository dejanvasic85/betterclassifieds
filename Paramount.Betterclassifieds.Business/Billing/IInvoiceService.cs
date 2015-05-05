namespace Paramount.Betterclassifieds.Business
{
    public interface IInvoiceService
    {
        Invoice GenerateBookingInvoice(int bookingId);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IClientConfig _clientConfig;

        public InvoiceService(IInvoiceRepository invoiceRepository, IClientConfig clientConfig)
        {
            _invoiceRepository = invoiceRepository;
            _clientConfig = clientConfig;
        }

        public Invoice GenerateBookingInvoice(int bookingId)
        {
            var invoice = _invoiceRepository.GetInvoiceDataForBooking(bookingId);
            if (invoice == null)
                return null;

            // Map the client (business) details
            invoice.BusinessAddress = _clientConfig.ClientAddress.ToString();
            invoice.BusinessName = _clientConfig.ClientName;
            invoice.BusinessPhone = _clientConfig.ClientPhoneNumber;
            
            return invoice;
        }
    }
}
