namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventTicketMockBuilder
    {
        public EventTicketMockBuilder Default()
        {
            return WithEventId(123)
                .WithIsActive(true)
                .WithEventTicketId(1000)
                .WithPrice(10)
                .WithAvailableQuantity(100)
                .WithRemainingQuantity(100)
                .WithTicketName("MockTicket")
                ;
        }

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