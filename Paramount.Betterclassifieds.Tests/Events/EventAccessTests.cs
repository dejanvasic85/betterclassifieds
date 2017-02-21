using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventAccessTests : TestContext<EventAccess>
    {
        private Mock<IEventRepository> _eventRepository;

        [SetUp]
        public void SetupDependencies()
        {
            _eventRepository = CreateMockOf<IEventRepository>();
        }

        [Test]
        public void IsUserAuthorisedForAdId_HasAccess_ReturnsTrue()
        {
            var ad = new AdBookingModelMockBuilder()
                .WithAds(new List<IAd>()
                {
                    new OnlineAdModel {OnlineAdId = 1}
                }).Build();

            var mockEventDetails = new EventModelMockBuilder()
                .Default()
                .WithOrganiser(new EventOrganiserMockBuilder()
                .Default()
                .WithUserId("foobar").Build())
                .Build();

            _eventRepository.SetupWithVerification(call =>
                call.GetEventDetailsForOnlineAdId(It.IsAny<int>(), It.IsAny<bool>())
                , result: mockEventDetails);

            var access = BuildTargetObject();
            var result = access.IsUserAuthorisedForAdId("foobar", ad);

            result.IsTrue();
        }

        [Test]
        public void IsUserAuthorisedForAdId_AccessIsNotActive_ReturnsFalse()
        {
            var ad = new AdBookingModelMockBuilder()
                .WithAds(new List<IAd>()
                {
                    new OnlineAdModel {OnlineAdId = 1}
                }).Build();

            var mockEventDetails = new EventModelMockBuilder()
                .Default()
                .WithOrganiser(new EventOrganiserMockBuilder().Default().WithIsActive(false).WithUserId("foobar").Build())
                .Build();

            _eventRepository.SetupWithVerification(call =>
                call.GetEventDetailsForOnlineAdId(It.IsAny<int>(), It.IsAny<bool>())
                , result: mockEventDetails);

            var access = BuildTargetObject();
            var result = access.IsUserAuthorisedForAdId("foobar", ad);

            result.IsFalse();
        }

        [Test]
        public void IsUserAuthorisedForAdId_NoMatchingUser_ReturnsFalse()
        {
            var ad = new AdBookingModelMockBuilder()
                .WithAds(new List<IAd>()
                {
                    new OnlineAdModel {OnlineAdId = 1}
                }).Build();

            var mockEventDetails = new EventModelMockBuilder()
                .Default()
                .WithOrganiser(new EventOrganiserMockBuilder().Default().WithUserId("someone-else").Build())
                .Build();

            _eventRepository.SetupWithVerification(call =>
                call.GetEventDetailsForOnlineAdId(It.IsAny<int>(), It.IsAny<bool>())
                , result: mockEventDetails);

            var access = BuildTargetObject();
            var result = access.IsUserAuthorisedForAdId("foobar", ad);

            result.IsFalse();
        }

        [Test]
        public void IsUserAuthorisedForAdId_NullOrganisers_ReturnsFalse()
        {
            var ad = new AdBookingModelMockBuilder()
                .WithAds(new List<IAd>()
                {
                    new OnlineAdModel {OnlineAdId = 1}
                }).Build();

            var mockEventDetails = new EventModelMockBuilder()
                .Default()
                .WithEventOrganisers(null)
                .Build();

            _eventRepository.SetupWithVerification(call =>
                call.GetEventDetailsForOnlineAdId(It.IsAny<int>(), It.IsAny<bool>())
                , result: mockEventDetails);

            var access = BuildTargetObject();
            var result = access.IsUserAuthorisedForAdId("foobar", ad);

            result.IsFalse();
        }

        [Test]
        public void IsUserAuthorisedForAdId_AdBookingIsNull_ThrowsArgException()
        {
            var access = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => access.IsUserAuthorisedForAdId("blah", null));
        }

        [Test]
        public void IsUserAuthorisedForAdId_UserNull_ThrowsArgException()
        {
            var access = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => access.IsUserAuthorisedForAdId(null, new AdBookingModel()));
        }

        [Test]
        public void IsUserAuthorisedForAdId_OnlineAdIdNotAvailable_ThrowsException()
        {
            var access = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => access.IsUserAuthorisedForAdId("foobar", new AdBookingModelMockBuilder().Build()));
        }

        [Test]
        public void GetAdsForUser_NullUsername_ThrowsArgumentException()
        {
            var access = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => access.GetAdsForUser(null));
        }

        [Test]
        public void GetAdsForUser_CallsRepository_ReturnsOnlineAdIds()
        {
            var builder = new EventModelMockBuilder().Default();
            var mockResults = new[]
            {
                builder.WithOnlineAdId(1).Build(),
                builder.WithOnlineAdId(2).Build(),
                builder.WithOnlineAdId(3).Build()
            };

            _eventRepository.SetupWithVerification(call => call.GetEventsForOrganiser(It.IsAny<string>()), result: mockResults);

            var access = BuildTargetObject();
            var results = access.GetAdsForUser("foobar").ToArray();

            results.Length.IsEqualTo(3);
            results[0].IsEqualTo(1);
            results[1].IsEqualTo(2);
            results[2].IsEqualTo(3);

        }
    }
}
