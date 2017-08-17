using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Location;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class DebugLocationService : ILocationService
    {
        private readonly ILogService _logService;

        public DebugLocationService(ILogService logService)
        {
            _logService = logService;
        }

        public TimeZoneResult GetTimezone(decimal latitude, decimal longitude)
        {
            _logService.Warn($"Using the DebugLocationService! lat: {latitude} , long {longitude}");

            return new TimeZoneResult
            {
                TimeZoneName = "Australian Eastern Standard Time",
                TimeZoneId = "Australia/Sydney",
                DstOffset = 0,
                RawOffset = 36000
            };
        }
    }
}