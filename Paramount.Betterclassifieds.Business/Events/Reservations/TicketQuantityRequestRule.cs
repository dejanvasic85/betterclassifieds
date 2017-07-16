namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    /// <summary>
    /// Determines whether there is enough tickets available for the requested ticket and quantity
    /// </summary>
    public class TicketQuantityRequestRule : IBusinessRule<TicketQuantityRequest, EventTicketReservationStatus>
    {
        private readonly IEventManager _eventManager;

        public TicketQuantityRequestRule(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public RuleResult<EventTicketReservationStatus> IsSatisfiedBy(TicketQuantityRequest target)
        {
            Guard.NotNullIn(target, target.EventTicket);
            if (target.EventTicket.RemainingQuantity == 0)
            {
                return new RuleResult<EventTicketReservationStatus> { IsSatisfied = false, Result = EventTicketReservationStatus.SoldOut };
            }
            
            int remainingTicketCount = _eventManager.GetRemainingTicketCount(target.EventTicket);

            if (remainingTicketCount < target.Quantity)
            {
                return new RuleResult<EventTicketReservationStatus> { IsSatisfied = false, Result = EventTicketReservationStatus.RequestTooLarge };
            }

            return new RuleResult<EventTicketReservationStatus>
            {
                IsSatisfied = true,
                Result = EventTicketReservationStatus.Reserved
            };
        }
    }
}