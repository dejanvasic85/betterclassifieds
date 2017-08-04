using System;
using System.Collections.Generic;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Events; 

namespace Paramount.Betterclassifieds.Tests.Integration
{
    [TestFixture()]
    public class EventRepositoryTests
    {
        private readonly IEventRepository _repository;

        public EventRepositoryTests()
        {
            _repository = new EventRepository(
                new DbContextFactory());
        }


        [Test]
        public void GetEventGroups()
        {
            var repository = new EventRepository(new DbContextFactory());
            var result = repository.GetEventGroupsAsync(11, null).Result;

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
            }, new[] { 12, 13 });
        }

        [Test]
        public void UpdateEventTicketIncudingFields()
        {
            var originalEventTicket = new EventTicket
            {
                EventId = 25,
                TicketName = "Free Entry",
                RemainingQuantity = 100,
                AvailableQuantity = 100,
                Price = 0,
                EventTicketFields = new List<EventTicketField>
                {
                    new EventTicketField { FieldName = "Weight" },
                    new EventTicketField { FieldName = "Height" }
                }
            };

            var repository = new EventRepository(new DbContextFactory());
            repository.CreateEventTicket(originalEventTicket);


            var ticketToUpdate = new EventTicket
            {
                EventTicketId = originalEventTicket.EventTicketId,
                EventId = originalEventTicket.EventId,
                TicketName = "New Name",
                Price = 1,
                EventTicketFields = new List<EventTicketField>
                {
                    new EventTicketField{FieldName = "* Weight", IsRequired = true},
                    new EventTicketField { FieldName = "Height", IsRequired = true}
                }
            };

            repository.UpdateEventTicketIncudingFields(ticketToUpdate);
        }

        [Test]
        public void CreateEventOrganiser()
        {
            var organiser = new EventOrganiser
            {
                EventId = 1,
                UserId = "shia",
                IsActive = true,
                LastModifiedBy = "admin",
                LastModifiedDate = DateTime.Now,
                LastModifiedDateUtc = DateTime.UtcNow
            };

           _repository.CreateEventOrganiser(organiser);

            Assert.That(organiser.EventOrganiserId, Is.Not.EqualTo(0));
        }

        [Test]
        public void GetEventSeats()
        {
            var seats = _repository.GetEventSeats(8, seatNumber:"A1");

            Assert.That(seats, Is.Not.Null);
        }
    }
}