using System;
using Moq;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Tests.Mocks
{
    internal static class LogServiceMockExtensions
    {
        public static Mock<ILogService> SetupAllCalls(this Mock<ILogService> mock)
        {
            mock.Setup(call => call.Info(It.IsAny<string>()));
            mock.Setup(call => call.Warn(It.IsAny<string>()));
            mock.Setup(call => call.Debug(It.IsAny<string>()));
            mock.Setup(call => call.Error(It.IsAny<string>()));
            mock.Setup(call => call.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            mock.Setup(call => call.Error(It.IsAny<Exception>()));
            return mock;
        }
    }
}