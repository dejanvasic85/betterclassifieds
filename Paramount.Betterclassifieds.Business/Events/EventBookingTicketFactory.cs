using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicketFactory
    {
        private readonly IDateService _dateService;

        public EventBookingTicketFactory(IDateService dateService)
        {
            this._dateService = dateService;
        }

        public EventBookingTicket CreateFromReservation(EventTicketReservation reservation)
        {
            if(!reservation.EventTicketId.HasValue)
                throw new ArgumentNullException("reservation", "EventTicketId cannot be null in the reservation");

            if(reservation.EventTicket == null)
                throw new ArgumentNullException("reservation", "EventTicket cannot be null in the reservation");

            return new EventBookingTicket
            {
                EventTicketId = reservation.EventTicketId.Value,
                Quantity = reservation.Quantity,
                TicketName = reservation.EventTicket.TicketName,
                CreatedDateTime = _dateService.Now,
                CreatedDateTimeUtc = _dateService.UtcNow
            };
        }
    }
}