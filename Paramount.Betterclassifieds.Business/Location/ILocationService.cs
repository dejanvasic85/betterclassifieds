using System;

namespace Paramount.Betterclassifieds.Business.Location
{
    public interface ILocationService
    {
        TimeZoneResult GetTimezone(decimal latitude, decimal longitude);
    }
}