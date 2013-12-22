using System;
using System.Diagnostics;
using System.Text;
using Moq;

namespace Paramount.Betterclassifieds.Tests
{
    public static class MockDataContext
    {
        //public static Mock<TDataContext> CreateMockOf<TDataContext>(StringBuilder sb) where TDataContext : class, IDisposable
        //{
        //    Mock<TDataContext> mock = new Mock<TDataContext>(MockBehavior.Strict);

        //    mock.Setup( call => call.Dispose())
        //        .Callback(() =>
        //        {
        //            StackTrace stack = new StackTrace();
        //            sb.AppendLine(stack.ToString());
        //        });

        //    return mock;
        //}
    }
}
