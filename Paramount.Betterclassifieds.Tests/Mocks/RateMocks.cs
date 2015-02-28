using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Tests.Mocks
{
    internal static class RateMocks
    {
        public static OnlineAdRate Create()
        {
            return new OnlineAdRate();
        }

        public static OnlineAdRate WithBaseRate(this OnlineAdRate rate, decimal baseRate)
        {
            rate.MinimumCharge = baseRate;
            return rate;
        }
    }
}