using System;
using System.Configuration;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class SeatAvailabilityRule : IBusinessRule<SeatRequest, EventTicketReservationStatus>
    {
        public RuleResult<EventTicketReservationStatus> IsSatisfiedBy(SeatRequest request)
        {
            Guard.NotNull(request);
            Guard.NotNull(request.DesiredSeat);

            var ruleOutcome = new RuleResult<EventTicketReservationStatus>();
            
            var seat = request.Seat;

            if (seat == null || seat.SeatNumber != request.DesiredSeat)
            {
                ruleOutcome.Result = EventTicketReservationStatus.InvalidRequest;
                return ruleOutcome;
            }

            if (seat.IsBooked || seat.NotAvailableToPublic.GetValueOrDefault())
            {
                ruleOutcome.Result = EventTicketReservationStatus.SoldOut;
                return ruleOutcome;
            }

            ruleOutcome.IsSatisfied = true;
            ruleOutcome.Result = EventTicketReservationStatus.Reserved;

            return ruleOutcome;
        }
    }
}