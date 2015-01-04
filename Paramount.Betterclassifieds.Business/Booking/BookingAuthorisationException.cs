using System;

namespace Paramount.Betterclassifieds.Business.Booking
{
    public class BookingAuthorisationException : Exception
    {
        public BookingAuthorisationException(string msg) :
            base(msg)
        { }
    }
}