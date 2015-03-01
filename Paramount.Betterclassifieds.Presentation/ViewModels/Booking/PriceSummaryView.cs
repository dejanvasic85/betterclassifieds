using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class PriceSummaryView
    {
        public OnlineProductSummary OnlineProduct { get; set; }
        public List<PrintProductSummary> PrintProducts { get; set; }
        public decimal BookingTotal { get; set; }
    }

    public class OnlineProductSummary
    {
        public string Name { get; set; }
        public OnlineSummaryItemView[] OnlineItems { get; set; }
        public decimal ProductTotal { get; set; }
    }

    public class PrintProductSummary
    {
        public string Name { get; set; }
        public PrintSummaryItemView[] PrintItems { get; set; }
        public decimal ProductTotal { get; set; }
    }

    public class PrintSummaryItemView
    {
        public int Editions { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }

    public class OnlineSummaryItemView
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}