using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Events;

namespace Paramount.Betterclassifieds.Tests.Integration
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

        [Test]
        public void CreateEventGroup()
        {
            var repository = new EventRepository(new DbContextFactory());
            repository.CreateEventGroup(new EventGroup
            {
                EventId = 11,
                CreatedDateTime = DateTime.Now,
                CreatedBy = "dejanvasic24",
                CreatedDateTimeUtc = DateTime.UtcNow,
                GroupName = "Group 123",
                MaxGuests = 10,
                AvailableToAllTickets = false
            }, new [] {12, 13} );
        }
    }
}