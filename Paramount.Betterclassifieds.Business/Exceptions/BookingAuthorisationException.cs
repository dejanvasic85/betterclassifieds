using System;

namespace Paramount.Betterclassifieds.Business
{
    public class BookingAuthorisationException : Exception
    {
        public BookingAuthorisationException(string msg) :
            base(msg)
        { }
    }
}