using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    /// <summary>
    /// Object that is used to calculate the price of an Ad
    /// </summary>
    public interface IAdRateContext
    {
        bool IsLineAdIncluded { get; }
        OnlineAdModel OnlineAdModel { get; }
        int[] Publications { get; }
        LineAdModel LineAdModel { get; }
        int? SubCategoryId { get; }
        string BookingReference { get; }
        string PaymentReference { get; }
        int? CategoryId { get; }
        int? PrintInsertions { get; }

        void UpdateByPricingFactors(PricingFactors pricingFactors);
    }
}