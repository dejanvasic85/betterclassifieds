using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Tests
{
    internal class ApplicationUserMockBuilder : MockBuilder<ApplicationUserMockBuilder, ApplicationUser>
    {
        public ApplicationUserMockBuilder WithEmail(string email)
        {
            return WithBuildStep(s => s.Email = email);
        }
    }
}