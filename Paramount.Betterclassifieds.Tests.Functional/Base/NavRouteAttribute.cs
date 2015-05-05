using System;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal class NavRouteAttribute : Attribute
    {
        public NavRouteAttribute()
        {
            
        }

        public NavRouteAttribute(string relativeUrl)
        {
            this.RelativeUrl = relativeUrl;
        }

        public string RelativeUrl { get; set; }
        public string Title { get; set; }
    }
}