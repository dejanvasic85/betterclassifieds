using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketReservationRequest
    {
        public int Quantity { get; set; }
        public EventTicket EventTicket { get; set; }

        public EventTicketFailedReservation ToFailedReservation()
        {
            return new EventTicketFailedReservation
            {
                FailReason = EventTicketReservationFailReason.SoldOut,
                Quantity = this.Quantity,
                TicketId = this.EventTicket.TicketId.GetValueOrDefault(),
                TicketName = this.EventTicket.TicketName
            };
        }

        public EventTicketReservation ToReservation(DateTime currentDate, DateTime currentUtcDate, string sessionId, int expiryMinutes)
        {
            return new EventTicketReservation
            {
                CreatedDate = currentDate,
                CreatedDateUtc = currentUtcDate,
                ExpiryDate = currentDate.AddMinutes(expiryMinutes),
                ExpiryDateUtc = currentUtcDate.AddMinutes(expiryMinutes),
                Quantity = this.Quantity,
                SessionId = sessionId,
                TicketId = this.EventTicket.TicketId.GetValueOrDefault(),
                Active = true
            };
        }
    }
}