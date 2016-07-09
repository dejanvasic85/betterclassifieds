using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.DataService
{
    public class BookCartRepository : MongoRepository<BookingCart>, IBookCartRepository
    {
        public BookCartRepository()
            : base("bookings")
        {
            
        }

        public BookingCart GetBookingCart(string id)
        {
            if (id.IsNullOrEmpty())
                return null;

            return Collection.FindOneByIdAs<BookingCart>(id);
        }

        public IBookingCart Save(IBookingCart bookingCart)
        {
            Collection.Save(bookingCart);
            return bookingCart;
        }
    }
}