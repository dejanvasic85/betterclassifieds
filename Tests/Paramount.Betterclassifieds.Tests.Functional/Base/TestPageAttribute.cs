using System;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TestPageAttribute : Attribute
    {
        public string RelativeUrl { get; set; }
        public string Title { get; set; }
    }
}