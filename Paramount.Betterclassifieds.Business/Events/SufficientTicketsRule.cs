namespace Paramount.Betterclassifieds.Business.Events
{
    public class SufficientTicketsRule : IBusinessRule<RemainingTicketsWithRequestInfo, EventTicketReservationStatus>
    {
        public RuleResult<EventTicketReservationStatus> IsSatisfiedBy(RemainingTicketsWithRequestInfo target)
        {
            if (target.TicketsRemaining == 0)
            {
                return new RuleResult<EventTicketReservationStatus> { Result = EventTicketReservationStatus.SoldOut };
            }

            if (target.TicketsRemaining < target.TicketsRequested)
            {
                return new RuleResult<EventTicketReservationStatus> { Result = EventTicketReservationStatus.RequestTooLarge };
            }

            return new RuleResult<EventTicketReservationStatus> { IsSatisfied = true, Result = EventTicketReservationStatus.Reserved };
        }
    }
}
