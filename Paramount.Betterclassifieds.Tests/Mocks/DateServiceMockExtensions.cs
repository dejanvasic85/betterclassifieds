using System;
using Moq;

namespace Paramount.Betterclassifieds.Tests.Mocks
{
    internal static class DateServiceMockExtensions
    {
        public static Mock<IDateService> SetupNow(this Mock<IDateService> mock)
        {
            mock.SetupWithVerification(call => call.Now, DateTime.Now);
            return mock;
        }

        public static Mock<IDateService> SetupNowUtc(this Mock<IDateService> mock)
        {
            mock.SetupWithVerification(call => call.UtcNow, DateTime.UtcNow);
            return mock;
        }
    }
}