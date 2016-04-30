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
            var fee = (originalTicketPrice * GetEventTicketFeePercentage()) + GetEventTicketFeeCents();

            var priceIncludingFee = originalTicketPrice + fee;

            return new TicketPrice
            {
                Fee = fee,
                OriginalPrice = originalTicketPrice,
                PriceIncludingFee = priceIncludingFee
            };
        }

        public TicketPrice GetTotalTicketPrice(EventTicket ticket)
        {
            Guard.NotNull(ticket);
            return GetTotalTicketPrice(ticket.Price);
        }

        public EventPaymentSummary GetOrganiserOwedAmount(decimal totalTicketSaleAmount, int totalTicketSales)
        {
            var totalFees = totalTicketSaleAmount * GetEventTicketFeePercentage();
            totalFees += totalTicketSales * GetEventTicketFeeCents();
            var owedAmount = totalTicketSaleAmount - totalFees;

            return new EventPaymentSummary
            {
                EventOrganiserOwedAmount = owedAmount,
                EventOrganiserFeesTotalFeesAmount = totalFees
            };
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
