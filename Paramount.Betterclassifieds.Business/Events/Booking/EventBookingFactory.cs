using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingFactory
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;
        private readonly TicketFeeCalculator _ticketFeeCalculator;

        public EventBookingFactory(IEventRepository eventRepository, IDateService dateService, TicketFeeCalculator ticketFeeCalculator)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
            _ticketFeeCalculator = ticketFeeCalculator;
        }

        public EventBooking Create(int eventId,
            EventPromoCode eventPromo,
            ApplicationUser applicationUser,
            IEnumerable<EventTicketReservation> currentReservations)
        {
            var reservations = currentReservations.ToList();
            var bookingCost = _ticketFeeCalculator.GetTotalTicketPrice(reservations, eventPromo);

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
                DiscountAmount = bookingCost.DiscountAmount,
                Cost = bookingCost.OriginalPrice,
                TransactionFee = bookingCost.Fee,
                TotalCost = bookingCost.Total,
                Status = bookingCost.Total > 0
                    ? EventBookingStatus.PaymentPending
                    : EventBookingStatus.Active
            };

            // Add the ticket bookings
            var eventBookingTicketFactory = new EventBookingTicketFactory(_eventRepository, _dateService, _ticketFeeCalculator);
            eventBooking.EventBookingTickets.AddRange(
                reservations.SelectMany(r => eventBookingTicketFactory.CreateFromReservation(r, eventPromo, _dateService.Now, _dateService.UtcNow)));
            
            return eventBooking;
        }
    }
}