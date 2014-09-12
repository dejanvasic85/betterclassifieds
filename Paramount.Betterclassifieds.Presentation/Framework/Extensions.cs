using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Paramount.Betterclassifieds.Presentation.Framework
{
    public static class Extensions
    {
        // Converts to a json model
        public static string ToJsonString(this object model)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(model, Formatting.None, serializerSettings);
        }
    }
}