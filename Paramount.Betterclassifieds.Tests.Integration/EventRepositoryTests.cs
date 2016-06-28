using NUnit.Framework;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Events;

namespace Paramount.Betterclassifieds.Payments.Stripe.Tests
{
    [TestFixture]
    public class EventRepositoryTests
    {
        [Test]
        public void GetEventGroups()
        {
            var repository = new EventRepository(new DbContextFactory());
            var result = repository.GetEventGroups(11, null).Result;
            
        }

        [Test]
        public void GetEventTicketsForGroupId()
        {
            var repository = new EventRepository(new DbContextFactory());
            var result = repository.GetEventTicketsForGroup(1).Result;
        }
    }
}