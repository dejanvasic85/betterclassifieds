﻿namespace Paramount.Betterclassifieds.Business
{
    using Booking;
    public interface IOnlineCharge
    {
        AdCharge Calculate(OnlineAdRate rate, OnlineAdModel onlineAdModel);
    }
}