using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.DataService;

namespace Paramount.Betterclassifieds.Tests.Integration
{
    [TestFixture]
    public class SearchServiceIntegrationTests
    {
        [Test]
        public void GetCurrentEvents()
        {
            var searchService = new SearchService(new DbContextFactory());
            var events = searchService.GetEvents();
        }
    }
}
