namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class EventRowContract
    {
        public static EventRowContract New(string rowId, params EventSeatBookingContract[] seats)
        {
            return new EventRowContract
            {
                RowId = rowId,
                Seats = seats
            };
        }

        public string RowId { get; set; }
        public EventSeatBookingContract[] Seats { get; set; }
    }
}