namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class GroupQuantityRequestRule : IBusinessRule<GroupRequest>
    {
        public bool IsSatisfiedBy(GroupRequest target)
        {
            Guard.NotNullIn(target, target.EventGroup);
            if (target.EventGroup.MaxGuests == null)
                return true;

            return (target.EventGroup.MaxGuests - target.EventGroup.GuestCount) >= target.Quantity;
        }
    }
}