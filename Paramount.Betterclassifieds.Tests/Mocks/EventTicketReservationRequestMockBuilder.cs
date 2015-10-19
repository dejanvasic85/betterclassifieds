using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests
{
    internal class EventTicketReservationRequestMockBuilder :
        MockBuilder<EventTicketReservationRequestMockBuilder, EventTicketReservationRequest>
    {
        public EventTicketReservationRequestMockBuilder WithQuantity(int quantity)
        {
            return WithBuildStep(prop => prop.Quantity = quantity);
        }

        public EventTicketReservationRequestMockBuilder WithEventTicket(EventTicketMockBuilder eventTicketMockBuilder)
        {
            return WithBuildStep(prop => prop.EventTicket = eventTicketMockBuilder.Build());
        }
    }
}