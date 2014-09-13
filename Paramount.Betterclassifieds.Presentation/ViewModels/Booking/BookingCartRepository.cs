namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public interface IBookingCartRepository
    {
        BookingCart GetBookingCart(string id);
        BookingCart SaveBookingCart(BookingCart bookingCart);
    }

    public class BookingCartRepository : MongoRepository<BookingCart>, IBookingCartRepository
    {
        public BookingCartRepository() : base("bookings")
        { }

        public BookingCart GetBookingCart(string id)
        {
            if (id.IsNullOrEmpty())
                return null;

            var cart = Collection.FindOneByIdAs<BookingCart>(id);
            if (cart == null || cart.Completed)
                return null;

            return cart;
        }

        public BookingCart SaveBookingCart(BookingCart bookingCart)
        {
            Collection.Save(bookingCart);
            return bookingCart;
        }
    }
}