using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.NextGen
{
    // Todo - Pricing

    public interface IBookingRepository
    {
    }

    public interface IBookingManager
    {
        List<Category> GetCategories();

        int StartBooking(int categoryId);

        List<Channel> GetPublicationsForCategory(int categoryId);

        void SetChannels(List<int> channels);

        void SetState(BookingState bookingState, int adBookingId);

        BookingState GetState(int adBookingId);

        PriceSummary CalculatePrice(int adBookingId);
        
        PaymentStatus GetPaymentStatus(int adBookingId);

        void SetPaymentStatus(PaymentStatus paymentStatus, int adBookingId);

        PaymentMethod GetPaymentMethod(int adBookingId);

        void SetPaymentMethod(PaymentMethod paymentMethod, int adBookingId);

        void SetAd(Ad adDetails, int adBookingId);

        Ad GetAdPreview(int adBookingId);

        string Submit(int adbookingId);
    }

    public class PriceSummary
    {
    }

    public class Category { }

    public class Channel { }

    public class BookingState { }

    public class PaymentStatus { }

    public class PaymentMethod { }

    public class Ad { }
}
