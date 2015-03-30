namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class PriceSummaryView
    {
        public OnlinePriceSummary OnlinePrice { get; set; }
        public decimal BookingTotal { get; set; }
        public PublicationPriceSummary[] PublicationPrices { get; set; }
    }

    public class PublicationPriceSummary
    {
        public string Publication { get; set; }
        public decimal PublicationTotal { get; set; }
        public PrintSummaryItemView[] Items { get; set; }
    }

    public class OnlinePriceSummary
    {
        public string Name { get; set; }
        public decimal OnlineTotal { get; set; }

        public OnlineSummaryItemView[] Items { get; set; }
    }
}