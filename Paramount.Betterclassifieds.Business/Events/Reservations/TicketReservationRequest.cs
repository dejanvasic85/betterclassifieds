namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class TicketReservationRequest
    {
        public TicketReservationRequest(int eventTicketId, int? eventGroupId, int selectedQuantity)
        {
            EventTicketId = eventTicketId;
            EventGroupId = eventGroupId;
            SelectedQuantity = selectedQuantity;
        }

        public int EventTicketId { get; }
        public int? EventGroupId { get; }
        public int SelectedQuantity { get; }
    }
}