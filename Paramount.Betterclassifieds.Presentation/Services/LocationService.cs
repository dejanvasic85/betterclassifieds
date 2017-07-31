using System.Diagnostics;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Location;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class LocationService : ILocationService
    {
        private readonly IApplicationConfig _config;
        private readonly IDateService _dateService;
        private readonly ILogService _logService;

        public LocationService(IApplicationConfig config, IDateService dateService,
            ILogService logService)
        {
            _config = config;
            _dateService = dateService;
            _logService = logService;
        }

        public TimeZoneResult GetTimezone(decimal latitude, decimal longitude)
        {
            Stopwatch sw = new Stopwatch();
            var url = $"{_config.GoogleTimezoneApiUrl.TrimEnd('/')}/timezone/json?location={latitude},{longitude}&timestamp={_dateService.Timestamp}&key={_config.GoogleTimezoneApiKey}";
            _logService.Info("Fetching timezone from " + url);
            var request = WebRequest.Create(url);
            var responseStream = request.GetResponse().GetResponseStream();
            if (responseStream == null)
                return null;

            var reader = new StreamReader(responseStream);
            var str = reader.ReadToEnd();
            responseStream.Close();
            reader.Close();
            _logService.Info("Timezone Response: " + str, sw.Elapsed);
            var googleResponseObj = JsonConvert.DeserializeObject<TimeZoneResult>(str);
            return googleResponseObj;
        }
    }
}