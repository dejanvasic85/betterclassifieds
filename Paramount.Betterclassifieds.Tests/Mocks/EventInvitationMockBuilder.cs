namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventInvitationMockBuilder
    {
        public EventInvitationMockBuilder Default()
        {
            return WithEventId(123)
                .WithEventInvitationId(1)
                .WithUserNetworkId(1000);
        }
    }
}