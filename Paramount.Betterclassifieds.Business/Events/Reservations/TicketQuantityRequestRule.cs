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

        public RuleResult<EventTicketReservationStatus> IsSatisfiedBy(TicketQuantityRequest request)
        {
            Guard.NotNullIn(request, request.EventTicket);
            if (request.EventTicket.RemainingQuantity == 0)
            {
                return new RuleResult<EventTicketReservationStatus> { IsSatisfied = false, Result = EventTicketReservationStatus.SoldOut };
            }
            
            int remainingTicketCount = _eventManager.GetRemainingTicketCount(request.EventTicket);

            if (remainingTicketCount < request.Quantity)
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