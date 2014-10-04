namespace Paramount.Betterclassifieds.Business
{
    public interface IBookingSessionIdentifier
    {
        string Id { get; }
        void SetId(string value);
    }
}