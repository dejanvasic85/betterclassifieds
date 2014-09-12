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

            return Collection.FindOneByIdAs<BookingCart>(id);
        }

        public BookingCart SaveBookingCart(BookingCart bookingCart)
        {
            Collection.Save(bookingCart);
            return bookingCart;
        }
    }
}