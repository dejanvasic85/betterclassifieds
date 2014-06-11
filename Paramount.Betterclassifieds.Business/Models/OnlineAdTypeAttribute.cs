using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Paramount.Betterclassifieds.Business.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OnlineAdTypeAttribute : Attribute
    {
        public string OnlineAdName { get; set; }

        public static Type GetClassTypeForTag(string onlineAdTag)
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.HasCustomAttribute<OnlineAdTypeAttribute>())
                .Single(t => t.GetCustomAttribute<OnlineAdTypeAttribute>().OnlineAdName == onlineAdTag)
                ;
        }
    }
}