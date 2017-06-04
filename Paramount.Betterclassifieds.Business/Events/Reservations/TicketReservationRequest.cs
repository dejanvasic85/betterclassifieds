namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class TicketReservationRequest
    {
        public TicketReservationRequest(int eventTicketId, int? eventGroupId, int selectedQuantity, string orderRequestId, string seatNumber)
        {
            EventTicketId = eventTicketId;
            EventGroupId = eventGroupId;
            SelectedQuantity = selectedQuantity;
            SeatNumber = seatNumber;
            OrderRequestId = orderRequestId;
        }

        public string OrderRequestId { get; }
        public int EventTicketId { get; }
        public int? EventGroupId { get; }
        public int SelectedQuantity { get; }
        public string SeatNumber { get; }

    }
}