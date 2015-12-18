using System;

namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventModelMockBuilder
    {
        public EventModelMockBuilder WithPastClosedDate()
        {
            WithBuildStep(p => p.ClosingDateUtc = DateTime.UtcNow.AddDays(-2));
            WithBuildStep(p => p.ClosingDate = DateTime.Now.AddDays(-2));
            return this;
        }

        public EventModelMockBuilder WithFutureClosedDate()
        {
            WithBuildStep(p => p.ClosingDateUtc = DateTime.UtcNow.AddDays(2));
            WithBuildStep(p => p.ClosingDate = DateTime.Now.AddDays(2));
            return this;
        }
    }
}