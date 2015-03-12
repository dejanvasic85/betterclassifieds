using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.DataService
{
    public class BookCartRepository : MongoRepository<BookingCart>, IBookCartRepository
    {
        public BookCartRepository()
            : base("bookings")
        {
            BsonClassMap.RegisterClassMap<BookingCart>(c =>
            {
                c.AutoMap();
                c.MapMember(member => member.StartDate).SetSerializationOptions(new DateTimeSerializationOptions { DateOnly = true });
                c.MapMember(member => member.EndDate).SetSerializationOptions(new DateTimeSerializationOptions { DateOnly = true });
                c.MapMember(member => member.PrintFirstEditionDate).SetSerializationOptions(new DateTimeSerializationOptions { DateOnly = true });
            });
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