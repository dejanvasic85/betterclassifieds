using Paramount.Betterclassifieds.Business;
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

    internal class ApplicationUserMockBuilder : MockBuilder<ApplicationUserMockBuilder, ApplicationUser>
    {
        public ApplicationUserMockBuilder WithEmail(string email)
        {
            return WithBuildStep(s => s.Email = email);
        }
    }
}