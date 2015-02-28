using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Tests.Mocks
{
    internal static class PrintRateMocks
    {
        public static RateModel Create()
        {
            return new RateModel();
        }

        public static RateModel WithHeadingAmount(this RateModel rate, decimal amount)
        {
            rate.BoldHeading = amount;
            return rate;
        }
    }
}