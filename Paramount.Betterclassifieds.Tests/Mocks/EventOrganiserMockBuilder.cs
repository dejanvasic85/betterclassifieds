using System;

namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventOrganiserMockBuilder
    {
        public EventOrganiserMockBuilder Default()
        {
            WithEventId(123);
            WithEventOrganiserId(999);
            WithIsActive(true);
            WithLastModifiedBy("admin");
            WithLastModifiedDate(DateTime.Now);
            WithLastModifiedDateUtc(DateTime.UtcNow);
            WithUserId("user123");
            return this;
        }
    }
}