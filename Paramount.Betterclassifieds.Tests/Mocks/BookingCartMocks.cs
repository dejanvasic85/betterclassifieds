using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Tests.Mocks
{
    public static class BookingCartMocks
    {
        public static BookingCart CreateMock(string username = "user-123")
        {
            return new BookingCart("Session-123", username);
        }
    }
}
