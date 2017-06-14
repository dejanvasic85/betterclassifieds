using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicketFactory
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;

        public EventBookingTicketFactory(IEventRepository eventRepository, IDateService dateService)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
        }

        public IEnumerable<EventBookingTicket> CreateFromReservation(EventTicketReservation reservation,
            DateTime createdDate, DateTime createdDateUtc)
        {
            if (!reservation.EventTicketId.HasValue)
                throw new ArgumentNullException(nameof(reservation), "EventTicketId cannot be null in the reservation");

            var eventTicket = _eventRepository.GetEventTicketDetails(reservation.EventTicketId.GetValueOrDefault());

            for (int i = 0; i < reservation.Quantity; i++)
            {
                yield return new EventBookingTicket
                {
                    EventTicketId = eventTicket.EventTicketId.GetValueOrDefault(),
                    TicketName = eventTicket.TicketName,
                    CreatedDateTime = createdDate,
                    CreatedDateTimeUtc = createdDateUtc,
                    Price = reservation.Price,
                    TransactionFee = reservation.TransactionFee,
                    TotalPrice = reservation.TotalPriceWithTxnFee,
                    GuestEmail = reservation.GuestEmail,
                    GuestFullName = reservation.GuestFullName,
                    EventGroupId = reservation.EventGroupId,
                    IsPublic = reservation.IsPublic,
                    SeatNumber = reservation.SeatNumber,
                    TicketFieldValues = reservation?.TicketFields?
                        .Select(r => new EventBookingTicketField { FieldName = r.FieldName, FieldValue = r.FieldValue })?
                        .ToList()
                };
            }
        }

        public EventBookingTicket CreateFromExisting(EventBookingTicket currentTicket, string guestFullName,
            string guestEmail, bool isPublic, int? eventGroupId, IEnumerable<EventBookingTicketField> fields,
            string username)
        {
            Guard.NotNull(currentTicket);

            return new EventBookingTicket
            {
                // Clone existing
                EventBookingId = currentTicket.EventBookingId,
                EventTicketId = currentTicket.EventTicketId,
                IsActive = true,
                IsPublic = isPublic,
                CreatedDateTime = _dateService.Now,
                CreatedDateTimeUtc = _dateService.UtcNow,
                Price = currentTicket.Price,
                TicketName = currentTicket.TicketName,
                TotalPrice = currentTicket.TotalPrice,
                TransactionFee = currentTicket.TransactionFee,
                SeatNumber = currentTicket.SeatNumber,

                // New 
                GuestEmail = guestEmail,
                GuestFullName = guestFullName,
                EventGroupId = eventGroupId,
                LastModifiedBy = username,
                LastModifiedDate = _dateService.Now,
                LastModifiedDateUtc = _dateService.UtcNow,
                TicketFieldValues = fields?.ToList()
            };
        }
    }
}