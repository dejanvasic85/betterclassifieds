using System;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests
{
    internal class EventTicketReservationMockBuilder : MockBuilder<EventTicketReservationMockBuilder, EventTicketReservation>
    {
        public EventTicketReservationMockBuilder WithStatus(EventTicketReservationStatus status)
        {
            return WithBuildStep(prop => prop.Status = status);
        }

        public EventTicketReservationMockBuilder WithQuantity(int quantity)
        {
            return WithBuildStep(prop => prop.Quantity = quantity);
        }

        public EventTicketReservationMockBuilder WithExpiryDateUtc(DateTime dateTime)
        {
            return WithBuildStep(prop => prop.ExpiryDateUtc = dateTime);
        }
    }
}