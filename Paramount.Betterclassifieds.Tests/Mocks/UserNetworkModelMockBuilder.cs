using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Tests
{
    internal partial class UserNetworkModelMockBuilder
    {
        public UserNetworkModel Default()
        {
            return new UserNetworkModel("user123",
                "friend@email.com",
                "foo bar"
                );
        }
    }
}