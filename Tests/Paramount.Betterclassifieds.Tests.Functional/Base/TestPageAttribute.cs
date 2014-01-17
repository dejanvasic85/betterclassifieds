using System;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class TestPageAttribute : Attribute
    {
        public string RelativeUrl { get; set; }
        public string Title { get; set; }
    }
}