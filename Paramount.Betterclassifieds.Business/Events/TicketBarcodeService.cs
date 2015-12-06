namespace Paramount.Betterclassifieds.Business.Events
{
    public interface ITicketBarcodeService
    {
        string Generate(EventModel eventModel, EventBookingTicket eventTicket);
    }

    public class TicketBarcodeService : ITicketBarcodeService
    {
        public string Generate(EventModel eventModel, EventBookingTicket eventTicket)
        {
            Guard.NotNull(eventModel);
            Guard.NotNull(eventTicket);
            return string.Format("{0}-{1}-{2}", eventModel.EventId, eventTicket.EventTicketId, eventTicket.EventBookingTicketId);
        }
    }
}
