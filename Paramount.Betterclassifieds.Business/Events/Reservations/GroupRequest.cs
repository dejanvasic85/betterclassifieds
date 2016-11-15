namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class GroupRequest
    {
        public EventGroup EventGroup { get; }
        public int Quantity { get; }

        public GroupRequest(EventGroup eventGroup, int quantity)
        {
            EventGroup = eventGroup;
            Quantity = quantity;
        }
    }
}