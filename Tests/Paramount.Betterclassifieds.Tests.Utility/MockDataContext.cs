using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Moq;

namespace Paramount.Betterclassifieds.Tests
{
    public static class MockDataContext
    {
        public static Mock<IDataContext> CreateMockOf<IDataContext>( StringBuilder sb ) where IDataContext : class, IDisposable
        {
            Mock<IDataContext> mock = new Mock<IDataContext>(MockBehavior.Strict);

            mock.Setup( call => call.Dispose())
                .Callback(() =>
                                                              {
                                                                  StackTrace stack = new StackTrace();
                                                                  sb.AppendLine(stack.ToString());
                                                              })
                                                              ;

            return mock;
        }
    }
}
