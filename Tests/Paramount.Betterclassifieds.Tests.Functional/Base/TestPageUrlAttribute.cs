using System;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class TestPageUrlAttribute : Attribute
    {
        public string RelativeUrl { get; set; }
    }
}