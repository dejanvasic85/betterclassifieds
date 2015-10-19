namespace Paramount.Betterclassifieds.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;
    using Moq;
    using NUnit.Framework;
    using Mocks;

    [TestFixture]
    public abstract class TestContext<T> where T : class 
    {
        private MockRepository _mockRepository;
        private List<Action> _verifyList;
        private IUnityContainer _containerBuilder;

        [TestFixtureSetUp]
        public void Initialise()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _verifyList = new List<Action>();
            _containerBuilder = new UnityContainer();
        }

        [TearDown]
        public void Cleanup()
        {
            _verifyList.ForEach(action => action());
            _verifyList.Clear();
        }
        
        protected T BuildTargetObject()
        {
            return _containerBuilder.Resolve<T>();
        }

        protected MockRepository MockRepository
        {
            get { return this._mockRepository; }
        }

        protected List<Action> VerifyList
        {
            get { return this._verifyList; }
        }

        protected IUnityContainer ContainerBuilder
        {
            get { return this._containerBuilder; }
        }

        protected Mock<TMock> CreateMockOf<TMock>() where TMock : class
        {
            return _mockRepository.CreateMockOf<TMock>(_containerBuilder, _verifyList);
        }
    }
}