namespace Paramount.Betterclassifieds.Presentation.ViewModels.Email
{
    public class EventTicketGuestEmail
    {
        public string EventName { get; set; }
        public string EventUrl { get; set; }
        public string BuyerName { get; set; }
        public bool IsGuestTheBuyer { get; set; }
        public string EventStartDateTime { get; set; }
        public string EventLocation { get; set; }
        public string BarcodeImgUrl { get; set; }
        public string SeatNumber { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public string TicketName { get; set; }
    }
}