using System;

namespace Paramount.Betterclassifieds.Business
{
    [Obsolete("We no longer need to store this in our database or perform logic. Please remove!")]
    public enum BookingType
    {
        Bundled,
        Regular,
        Special
    }
}