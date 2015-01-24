namespace Paramount.Betterclassifieds.Business.Booking
{
    public interface IBookingSessionIdentifier
    {
        string Id { get; }
        void SetId(string value);
    }
}