using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventGuestServiceTests : TestContext<EventGuestService>
    {
        private Mock<IEventRepository> _eventRepository;

        [SetUp]
        public void SetupDependencies()
        {
            _eventRepository = CreateMockOf<IEventRepository>();
        }

        [Test]
        public void GetFirstNames_Returns_ListOfNames()
        {
            var guests = new List<EventGuestDetails>
            {
                new EventGuestDetails{GuestFullName = "George Lewis Costanza"},
                new EventGuestDetails{GuestFullName = "Jerry Seinfeld"},
                new EventGuestDetails{GuestFullName = "Jerry Seinfeld"},
                new EventGuestDetails{GuestFullName = "Kramer"},
                new EventGuestDetails{GuestFullName = " Kramer  "},
                new EventGuestDetails{GuestFullName = "Elaine  Benez"},
                new EventGuestDetails{GuestFullName = ""}
            };

            var service = BuildTargetObject();

            var result = service.GetFirstNames(guests).ToList();

            result.Count.IsEqualTo(6);
            result.ElementAt(0).GuestName.IsEqualTo("George C");
            result.ElementAt(1).GuestName.IsEqualTo("Jerry S");
            result.ElementAt(2).GuestName.IsEqualTo("Jerry S");
            result.ElementAt(3).GuestName.IsEqualTo("Kramer");
            result.ElementAt(4).GuestName.IsEqualTo("Kramer");
            result.ElementAt(5).GuestName.IsEqualTo("Elaine B");
        }
    }
}
