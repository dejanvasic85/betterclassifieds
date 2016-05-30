namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class TicketingPricesExplainedViewModel
    {
        public decimal EventTicketFeePercentage { get; set; }
        public decimal EventTicketFeeCents { get; set; }

        public decimal Example1Fee { get; set; }
        public decimal Example1TicketPrice => 100;
        public decimal Example1TotalTicketPrice => Example1Fee + Example1TicketPrice;

        public decimal Example2Fee { get; set; }
        public decimal Example2TicketPrice => 50;

        public decimal Example3Fee { get; set; }
        public decimal Example3TicketPrice => 300;
        public decimal ExampleTotalTicketSales { get; set; }
        public decimal ExampleTotalTicketQuantitySold { get; set; }
        public decimal ExampleTotalFeeForOrganiser { get; set; }
        public decimal ExampleTotalAmountForOrganiser { get; set; }
        
    }
}