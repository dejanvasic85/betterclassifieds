namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class PriceSummaryView
    {
        public OnlineSummaryItemView[] OnlineItems { get; set; }
        public PrintSummaryItemView[] PrintItems { get; set; }
        public decimal BookingTotal { get; set; }
    }
}