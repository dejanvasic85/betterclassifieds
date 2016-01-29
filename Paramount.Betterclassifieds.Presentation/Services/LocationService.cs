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

        public LocationService(IApplicationConfig config, IDateService dateService)
        {
            _config = config;
            _dateService = dateService;
        }

        public TimeZoneResult GetTimezone(decimal latitude, decimal longitude)
        {
            var url = string.Format("{0}/timezone/json?location={1},{2}&timestamp={3}&key={4}", 
                _config.GoogleTimezoneApiUrl.TrimEnd('/'), latitude, longitude, _dateService.Timestamp, _config.GoogleTimezoneApiKey);

            var request = WebRequest.Create(url);
            var responseStream = request.GetResponse().GetResponseStream();
            if (responseStream == null)
                return null;

            var reader = new StreamReader(responseStream);
            var str = reader.ReadToEnd();
            responseStream.Close();
            reader.Close();
            var googleResponseObj = JsonConvert.DeserializeObject<TimeZoneResult>(str);
            return googleResponseObj;
        }
    }
}