namespace Paramount.Betterclassifieds.Business
{
    public interface ICategoryAdRepositoryFactory
    {
        ICategoryAdRepository<ICategoryAd> Create(Booking.IBookingCart bookingCart);
    }
}