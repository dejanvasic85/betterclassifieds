using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Events;

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

        public EventModelMockBuilder Default()
        {
            WithEventId(123);
            WithFutureClosedDate();
            WithLocation("1 Event Street, Earth");
            WithLocationLongitude(249);
            WithLocationLatitude(222);
            WithEventStartDate(DateTime.Now.AddDays(29));
            WithEventEndDate(DateTime.Now.AddDays(30));
            WithOnlineAdId(321);

            return this;
        }
    }
}