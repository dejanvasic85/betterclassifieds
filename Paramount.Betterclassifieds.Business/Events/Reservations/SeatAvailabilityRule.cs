using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class SeatAvailabilityRule : IBusinessRule<SeatRequest, EventTicketReservationStatus>
    {
        public RuleResult<EventTicketReservationStatus> IsSatisfiedBy(SeatRequest target)
        {
            var seat = target.ReservedSeats.FirstOrDefault(s => s.SeatNumber == target.DesiredSeatNumber);
            var ruleResult = new RuleResult<EventTicketReservationStatus>();

            if (seat == null)
            {
                ruleResult.IsSatisfied = false;
                ruleResult.Result = EventTicketReservationStatus.InvalidRequest;
                return ruleResult;
            }
            

            if (seat.IsAvailable())
            {
                ruleResult.IsSatisfied = true;
                ruleResult.Result = EventTicketReservationStatus.Reserved;
            }
            else
            {
                ruleResult.Result = EventTicketReservationStatus.SoldOut;
            }

            return ruleResult;
        }
    }
}