using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicketFactory
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;
        private readonly TicketFeeCalculator _ticketFeeCalculator;

        public EventBookingTicketFactory(IEventRepository eventRepository, IDateService dateService, TicketFeeCalculator ticketFeeCalculator)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
            _ticketFeeCalculator = ticketFeeCalculator;
        }

        public IEnumerable<EventBookingTicket> CreateFromReservation(EventTicketReservation reservation,
            EventPromoCode eventPromo, DateTime createdDate, DateTime createdDateUtc)
        {
            if (!reservation.EventTicketId.HasValue)
                throw new ArgumentNullException(nameof(reservation), "EventTicketId cannot be null in the reservation");

            var eventTicket = _eventRepository.GetEventTicketDetails(reservation.EventTicketId.GetValueOrDefault());
            var cost = _ticketFeeCalculator.GetTotalTicketPrice(reservation.Price.GetValueOrDefault(), eventPromo);

            for (int i = 0; i < reservation.Quantity; i++)
            {
                yield return new EventBookingTicket
                {
                    EventTicketId = eventTicket.EventTicketId.GetValueOrDefault(),
                    TicketName = eventTicket.TicketName,
                    CreatedDateTime = createdDate,
                    CreatedDateTimeUtc = createdDateUtc,
                    Price = cost.OriginalPrice,
                    TransactionFee = cost.Fee,
                    DiscountPercent = cost.DiscountPercent,
                    DiscountAmount = cost.DiscountAmount,
                    TotalPrice = cost.Total,
                    GuestEmail = reservation.GuestEmail,
                    GuestFullName = reservation.GuestFullName,
                    EventGroupId = reservation.EventGroupId,
                    IsPublic = reservation.IsPublic,
                    SeatNumber = reservation.SeatNumber,
                    TicketFieldValues = reservation?.TicketFields?
                        .Select(r => new EventBookingTicketField { FieldName = r.FieldName, FieldValue = r.FieldValue })
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
                DiscountAmount = currentTicket.DiscountAmount,
                DiscountPercent = currentTicket.DiscountPercent,
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