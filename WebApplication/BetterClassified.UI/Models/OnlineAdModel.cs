using System.Collections.Generic;
using System.Linq;
using Paramount;

namespace BetterClassified.Models
{
    public class OnlineAdModel
    {
        public int OnlineAdId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string HtmlText { get; set; }
        public decimal Price { get; set; }
        public int LocationId { get; set; }
        public int LocationAreaId { get; set; }
        public string ContactName { get; set; }
        public string ContactType { get; set; }
        public string ContactValue { get; set; }
        public int NumOfViews { get; set; }

        public static IEnumerable<string> GetOnlineAdTypeNames()
        {
            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

            return from type in types
                   where type.HasCustomAttribute<OnlineAdTypeAttribute>()
                   select type.GetCustomAttribute<OnlineAdTypeAttribute>().OnlineAdName;
        }
    }
}