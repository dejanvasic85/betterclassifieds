using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Tests
{
    internal class AdBookingMockBuilder : MockBuilder<AdBookingMockBuilder, AdBookingModel>
    {
        public AdBookingMockBuilder WithUser(string user)
        {
            return WithBuildStep(s => s.UserId = user);
        }
    }
}