using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using Moq;

namespace Paramount.Betterclassifieds.Tests.Mocks
{
    public static class MockFactory
    {
        public static Mock<T> CreateMockOf<T>(this MockRepository mockRepository, IUnityContainer container = null,
            List<Action> verifications = null, MockBehavior defaultBehaviour = MockBehavior.Strict) where T : class
        {
            var mock = mockRepository.Create<T>(defaultBehaviour);

            if (container != null)
                container.RegisterInstance(typeof(T), mock.Object);

            if (verifications != null)
                mock.VerifyWith(verifications);

            return mock;
        }

        public static Mock<T> VerifyWith<T>(this Mock<T> mock, List<Action> verifications) where T : class
        {
            verifications.Add(mock.Verify);
            return mock;
        }

        public static Mock<T> SetupWithVerification<T>(this Mock<T> mock, Expression<Action<T>> call) where T : class
        {
            mock.Setup(call).Verifiable();
            return mock;
        }

        public static Mock<T> SetupWithVerification<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> call, TResult result) where T : class
        {
            mock.Setup(call).Returns(result).Verifiable();
            return mock;
        }
    }
}
