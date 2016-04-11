namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventBarcodeManager
    {
        string GenerateBarcodeData(EventModel eventDetails, EventBookingTicket eventBookingTicket);
        EventBookingTicketValidationResult ValidateTicket(string barcodeData);
    }
}