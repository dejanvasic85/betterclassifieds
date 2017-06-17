using System;
using System.Collections.Generic;
using System.Linq;

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
            return GetTotalTicketPrice(originalTicketPrice, null);
        }

        public TicketPrice GetTotalTicketPrice(IEnumerable<ITicketPriceInfo> data, EventPromoCode eventPromoCode)
        {
            if (data == null)
                return TicketPrice.MinValue;

            var combinedPrice = data.Sum(r => r.Price.GetValueOrDefault());

            return GetTotalTicketPrice(combinedPrice, eventPromoCode);
        }

        public TicketPrice GetTotalTicketPrice(EventTicket ticket)
        {
            Guard.NotNull(ticket);
            return GetTotalTicketPrice(ticket.Price, null);
        }
        
        public TicketPrice GetTotalTicketPrice(decimal originalTicketPrice, EventPromoCode eventPromoCode)
        {
            if (originalTicketPrice <= 0)
            {
                return TicketPrice.MinValue;
            }

            decimal discountPercent = 0;
            if (eventPromoCode != null && !eventPromoCode.IsDisabled.GetValueOrDefault())
            {
                discountPercent = eventPromoCode.DiscountPercent.GetValueOrDefault();
            }

            var discountAmount = originalTicketPrice * (discountPercent / 100);
            var priceAfterDiscount = (originalTicketPrice - discountAmount);
            var fee = (priceAfterDiscount * GetEventTicketFeePercentage()) + GetEventTicketFeeCents();
            var priceIncludingFee = priceAfterDiscount + fee;
            return new TicketPrice(originalTicketPrice, priceIncludingFee, fee, discountPercent, discountAmount, priceAfterDiscount);
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
