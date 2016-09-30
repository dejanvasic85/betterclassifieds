namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventBarcodeValidator
    {
        string GetDataForBarcode(int eventId, EventBookingTicket eventBookingTicket);
        EventBookingTicketValidationResult ValidateTicket(string barcodeData);
    }
}