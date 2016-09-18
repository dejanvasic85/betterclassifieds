namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventBarcodeManager
    {
        string GenerateBarcodeData(EventModel eventDetails, EventBookingTicket eventBookingTicket);
        string GenerateBase64StringImageData(string barcodeData, int height, int width, int margin = 0);
        string GenerateBase64StringImageData(EventModel eventModel, EventBookingTicket eventTicket, int height, int width, int margin = 0);
        EventBookingTicketValidationResult ValidateTicket(string barcodeData);
    }
}