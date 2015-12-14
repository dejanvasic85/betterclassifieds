using System.Security.Principal;
using Moq;

namespace Paramount.Betterclassifieds.Tests.Mocks
{
    internal static class IdentityMockExtensions
    {
        public static Mock<IPrincipal> SetupUser(this Mock<IPrincipal> mock, string username = "fooBar")
        {
            mock.SetupWithVerification(call => call.Identity, new GenericIdentity(username));
            return mock;
        }
    }
}