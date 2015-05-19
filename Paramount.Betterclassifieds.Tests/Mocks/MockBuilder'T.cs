using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Tests
{
    internal class MockBuilder<TBuilder, TMock>
        where TBuilder : MockBuilder<TBuilder, TMock>, new()
        where TMock : class, new()
    {
        private IList<Action<TMock>> _buildSteps;

        protected MockBuilder()
        {
            _buildSteps = Enumerable.Empty<Action<TMock>>().ToList();
        }

        protected TBuilder WithBuildStep(params Action<TMock>[] newBuildSteps)
        {
            var builder = new TBuilder { _buildSteps = _buildSteps.AddRange(newBuildSteps).ToList() };
            return builder;
        }

        public TMock Build()
        {
            var mockObject = new TMock();
            foreach (var action in _buildSteps)
            {
                action(mockObject);
            }

            return mockObject;
        }
    }
}