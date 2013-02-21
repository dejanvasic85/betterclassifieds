using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            else if (BookingController.AdBookCart != null)
            {
                return BookingController.AdBookCart;
            }
            else if (BookingController.SpecialBookCart != null)
            {
                return BookingController.SpecialBookCart;
            }

            // Throw an exception to be handled by the UI
            if (!supressExceptions)
                throw new BookingExpiredException();
            else
                return null;
        }

        public static int GetHeaderCharacterLimit()
        {
            var headingLimit = GeneralRoutine.GetAppSetting("LineAds", "BoldHeadingLimit");

            return Convert.ToInt32(headingLimit);
        }

    }
}
