namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventGuestPublicView
    {
        public EventGuestPublicView(string guestName, string groupName)
        {
            this.GuestName = guestName;
            this.GroupName = groupName;
        }

        public string GuestName { get; private set; }
        public string GroupName { get; private set; }
    }
}