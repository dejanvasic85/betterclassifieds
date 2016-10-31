using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Events.Reservations;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class TicketRequestValidatorTests
    {
        [Test]
        public void IsSufficientTicketsAvailableForRequest_NullRequests_ThrowsArgException()
        {
            var mockEventManager = new Mock<IEventManager>();
            var validator = new TicketRequestValidator(mockEventManager.Object);

            Assert.Throws<ArgumentNullException>(() => validator.IsSufficientTicketsAvailableForRequest(null));
        }
    }
}