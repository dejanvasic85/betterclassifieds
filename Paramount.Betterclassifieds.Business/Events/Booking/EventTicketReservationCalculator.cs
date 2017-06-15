using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events.Booking
{
    public class EventTicketReservationCalculator
    {
        public IEnumerable<EventTicketReservation> Reservations { get; }
        public EventPromoCode EventPromoCode { get; }

        public EventTicketReservationCalculator(EventTicketReservation reservation, EventPromoCode eventPromo)
        {
            Guard.NotNull(reservation);

            Reservations = new List<EventTicketReservation>{reservation};
            EventPromoCode = eventPromo;
        }

        public EventTicketReservationCalculator(IEnumerable<EventTicketReservation> reserations, EventPromoCode promoCode)
        {
            Reservations = reserations;
            EventPromoCode = promoCode;
        }

        public TicketBookingCost Calculate()
        {
            if (Reservations == null)
                return new TicketBookingCost();

            var reservations = Reservations.ToArray();

            var cost = reservations.Sum(r => r.Price.GetValueOrDefault());
            var fee = reservations.Sum(r => r.TransactionFee.GetValueOrDefault());
            var total = (cost + fee);
            var discount = Discount.MinValue;

            if (EventPromoCode != null && !EventPromoCode.IsDisabled.GetValueOrDefault())
            {
                discount = new Discount(total, EventPromoCode.DiscountPercent.GetValueOrDefault());
                total = discount.AmountAfterDiscount;
            }

            return new TicketBookingCost(cost, fee, discount, total);
        }
    }
}
