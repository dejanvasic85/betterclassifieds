using System;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class SeatAvailabilityRule : IBusinessRule<SeatRequest, EventTicketReservationStatus>
    {
        public RuleResult<EventTicketReservationStatus> IsSatisfiedBy(SeatRequest target)
        {
            Guard.NotNull(target);
            Guard.NotNull(target.DesiredSeat);

            var ruleResult = new RuleResult<EventTicketReservationStatus>();

            if (target.DesiredSeat.NotAvailableToPublic.GetValueOrDefault() == false)
            {
                ruleResult.Result = EventTicketReservationStatus.InvalidRequest;
                return ruleResult;
            }

            if (target.BookedTickets != null &&
                target.BookedTickets.Any(t => t.SeatNumber.Equals(target.DesiredSeat.SeatNumber, StringComparison.OrdinalIgnoreCase)))
            {
                ruleResult.Result = EventTicketReservationStatus.SoldOut;
                return ruleResult;
            }

            if (target.ReservedTickets != null &&
                target.ReservedTickets.Any(r => r.SessionId.DoesNotEqual(target.CurrentRequestId) &&
                                                r.Status == EventTicketReservationStatus.Reserved))
            {
                ruleResult.Result = EventTicketReservationStatus.SoldOut;
                return ruleResult;
            }

            ruleResult.IsSatisfied = true;
            ruleResult.Result = EventTicketReservationStatus.Reserved;

            return ruleResult;
        }
    }
}