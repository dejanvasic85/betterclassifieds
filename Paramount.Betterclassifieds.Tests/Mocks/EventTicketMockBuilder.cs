using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventTicketMockBuilder
    {
        public EventTicketMockBuilder WithEventTicketReservations(EventTicketReservationMockBuilder eventTicketReservationMockBuilder, int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                WithBuildStep(prop => prop.EventTicketReservations.Add(eventTicketReservationMockBuilder.Build()));
            }
            return this;
        }
    }
}