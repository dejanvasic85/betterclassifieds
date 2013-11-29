using System;

namespace Paramount.DomainModel.Business.Betterclassifieds.Enums
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OnlineAdTypeAttribute : Attribute
    {
        public string OnlineAdName { get; set; }
    }
}