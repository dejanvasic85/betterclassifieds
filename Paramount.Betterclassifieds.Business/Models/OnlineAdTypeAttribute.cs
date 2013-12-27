using System;

namespace Paramount.Betterclassifieds.Business.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OnlineAdTypeAttribute : Attribute
    {
        public string OnlineAdName { get; set; }
    }
}