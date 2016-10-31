using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Events.Reservations;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class GroupQuantityRequestRuleTests
    {
        [Test]
        [TestCase(10, null, 15, true)]
        [TestCase(10, 15, 5, true)]
        [TestCase(10, 15, 4, true)]
        [TestCase(10, 15, 6, false)]
        public void IsSatisfied_PerformsCalculations_ReturnsBool(int currentGuestCount, int? maxGuests, int requestedQty, bool expected)
        {
            var mockGroup = new EventGroupMockBuilder()
                .WithGuestCount(currentGuestCount)
                .WithMaxGuests(maxGuests)
                .Build();
            var mockEventManager = new Mock<IEventManager>();
            mockEventManager.SetupWithVerification(call => call.GetEventGroup(It.IsAny<int>()), Task.FromResult(mockGroup));

            var rule = new GroupQuantityRequestRule();
            var result = rule.IsSatisfiedBy(new GroupRequest(mockGroup, quantity: requestedQty));

            // Assert
            result.IsEqualTo(expected);
        }

    }
}