using System;
using BetterclassifiedsCore;
using BetterclassifiedsCore.BundleBooking;
using BetterclassifiedsCore.BusinessEntities;

namespace BetterClassified.UIController.Booking
{
    public class BookCartController
    {
        public static IBookCartContext GetCurrentBookCart()
        {
            return GetCurrentBookCart(false);
        }

        public static IBookCartContext GetCurrentBookCart(bool supressExceptions)
        {
            // Check what type of booking controller is available from core
            // If not nothing, then return the cart information
            if (BundleController.IsActive)
            {
                return BundleController.BundleCart;
            }

            if (BookingController.AdBookCart != null)
            {
                return BookingController.AdBookCart;
            }

            // Throw an exception to be handled by the UI
            if (!supressExceptions)
                throw new BookingExpiredException();

            return null;
        }

        public static int GetHeaderCharacterLimit()
        {
            return AppSettingModule.AppKeyReader<int>.ReadFromStore("BoldHeadingLimit", 50);
        }
    }
}
