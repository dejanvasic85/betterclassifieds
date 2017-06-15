using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Events.Booking;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingFactory
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;

        public EventBookingFactory(IEventRepository eventRepository, IDateService dateService)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
        }

        public EventBooking Create(int eventId,
            EventPromoCode eventPromo,
            ApplicationUser applicationUser,
            IEnumerable<EventTicketReservation> currentReservations)
        {
            var reservations = currentReservations.ToList();
            var bookingCost = new EventTicketReservationCalculator(reservations, eventPromo).Calculate();

            var eventBooking = new EventBooking
            {
                EventId = eventId,
                CreatedDateTimeUtc = _dateService.Now,
                CreatedDateTime = _dateService.UtcNow,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Email = applicationUser.Email,
                Phone = applicationUser.Phone,
                PostCode = applicationUser.Postcode,
                UserId = applicationUser.Username,
                PromoCode = eventPromo?.PromoCode,
                DiscountPercent = eventPromo?.DiscountPercent,
                DiscountAmount = bookingCost.Discount.DiscountValue,
                Cost = bookingCost.Cost,
                TransactionFee = bookingCost.TransactionFee,
                TotalCost = bookingCost.TotalCost,
                Status = bookingCost.TotalCost > 0
                    ? EventBookingStatus.PaymentPending
                    : EventBookingStatus.Active
            };

            // Add the ticket bookings
            var eventBookingTicketFactory = new EventBookingTicketFactory(_eventRepository, _dateService);
            eventBooking.EventBookingTickets.AddRange(
                reservations.SelectMany(r => eventBookingTicketFactory.CreateFromReservation(r, eventPromo, _dateService.Now, _dateService.UtcNow)));
            
            return eventBooking;
        }
    }
}