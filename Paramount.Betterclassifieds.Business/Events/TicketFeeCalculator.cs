namespace Paramount.Betterclassifieds.Business.Events
{
    public class TicketFeeCalculator
    {
        private readonly IClientConfig _clientConfig;

        public TicketFeeCalculator(IClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
        }

        public TicketPrice GetTotalTicketPrice(decimal originalTicketPrice)
        {
            if (originalTicketPrice <= 0)
                return new TicketPrice();

            var fee = (originalTicketPrice * GetEventTicketFeePercentage()) + GetEventTicketFeeCents();
            var priceIncludingFee = originalTicketPrice + fee;
            return new TicketPrice(originalTicketPrice, priceIncludingFee, fee);
        }

        public TicketPrice GetTotalTicketPrice(EventTicket ticket)
        {
            Guard.NotNull(ticket);
            return GetTotalTicketPrice(ticket.Price);
        }

        public decimal GetFeeTotalForOrganiserForAllTicketSales(decimal totalTicketSaleAmount, int totalTicketSales)
        {
            if (totalTicketSaleAmount == 0)
                return 0;

            var totalFees = totalTicketSaleAmount * GetEventTicketFeePercentage();
            totalFees += totalTicketSales * GetEventTicketFeeCents();
            return totalFees;
        }

        private decimal GetEventTicketFeePercentage()
        {
            return (_clientConfig.EventTicketFeePercentage / 100);
        }

        private decimal GetEventTicketFeeCents()
        {
            return (_clientConfig.EventTicketFeeCents / 100);
        }
    }
}
