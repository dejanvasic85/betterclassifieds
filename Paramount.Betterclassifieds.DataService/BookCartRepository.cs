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

            var cart = Collection.FindOneByIdAs<BookingCart>(id);
            if (cart == null || cart.Completed)
                return null;

            return cart;
        }

        public BookingCart Save(BookingCart bookingCart)
        {
            Collection.Save(bookingCart);
            return bookingCart;
        }
    }
}