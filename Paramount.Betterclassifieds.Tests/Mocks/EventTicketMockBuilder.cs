using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests
{
    internal class EventTicketMockBuilder : MockBuilder<EventTicketMockBuilder, EventTicket>
    {
        public EventTicketMockBuilder WithRemainingQuantity(int quantity)
        {
            return WithBuildStep(prop => prop.RemainingQuantity = quantity);
        }

        public EventTicketMockBuilder WithEventTicketReservations(EventTicketReservationMockBuilder eventTicketReservationMockBuilder , int howMany )
        {
            for (int i = 0; i < howMany; i++)
            {
                WithBuildStep(prop => prop.EventTicketReservations.Add(eventTicketReservationMockBuilder.Build()));
            }
            return this;
        }
    }
}