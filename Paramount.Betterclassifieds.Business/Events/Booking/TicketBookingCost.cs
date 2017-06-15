using System;

namespace Paramount.Betterclassifieds.Business.Events.Booking
{
    public class TicketBookingCost
    {
        public TicketBookingCost()
            : this(Decimal.MinValue, Decimal.MinValue, Discount.MinValue, Decimal.MinValue)
        {

        }

        public TicketBookingCost(decimal cost, decimal transactionFee, Discount discount, decimal totalCostCost)
        {
            Cost = cost;
            TransactionFee = transactionFee;
            Discount = discount;
            TotalCost = totalCostCost;
        }

        public decimal Cost { get; }
        public decimal TransactionFee { get; }
        public Discount Discount { get; }
        public decimal TotalCost { get; }

        public static implicit operator decimal(TicketBookingCost cost)
        {
            return cost.TotalCost;
        }
    }
}