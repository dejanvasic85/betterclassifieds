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
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventAccessTests : TestContext<EventAccess>
    {
        private Mock<IEventRepository> _eventRepository;
        private Mock<IBookingManager> _bookingManager;
        private Mock<ISearchService> _searchService;

        [SetUp]
        public void SetupDependencies()
        {
            _eventRepository = CreateMockOf<IEventRepository>();
            _bookingManager = CreateMockOf<IBookingManager>();
            _searchService = CreateMockOf<ISearchService>();
        }

        [Test]
        public void IsUserAuthorisedForAd_HasAccess_ReturnsTrue()
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
            var result = access.IsUserAuthorisedForAd("foobar", ad);

            result.IsTrue();
        }

        [Test]
        public void IsUserAuthorisedForAd_HasAccessInBooking_ReturnsTrue()
        {
            var ad = new AdBookingModelMockBuilder()
                .WithUserId("foobar")
                .Build();

            var access = BuildTargetObject();
            var result = access.IsUserAuthorisedForAd("foobar", ad);

            result.IsTrue();
        }

        [Test]
        public void IsUserAuthorisedForAd_AccessIsNotActive_ReturnsFalse()
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
            var result = access.IsUserAuthorisedForAd("foobar", ad);

            result.IsFalse();
        }

        [Test]
        public void IsUserAuthorisedForAd_NoMatchingUser_ReturnsFalse()
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
            var result = access.IsUserAuthorisedForAd("foobar", ad);

            result.IsFalse();
        }

        [Test]
        public void IsUserAuthorisedForAd_NullOrganisers_ReturnsFalse()
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
            var result = access.IsUserAuthorisedForAd("foobar", ad);

            result.IsFalse();
        }

        [Test]
        public void IsUserAuthorisedForAd_AdBookingIsNull_ThrowsArgException()
        {
            var access = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => access.IsUserAuthorisedForAd("blah", null));
        }

        [Test]
        public void IsUserAuthorisedForAd_UserNull_ThrowsArgException()
        {
            var access = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => access.IsUserAuthorisedForAd(null, new AdBookingModel()));
        }

        [Test]
        public void IsUserAuthorisedForAd_OnlineAdIdNotAvailable_ThrowsException()
        {
            var access = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => access.IsUserAuthorisedForAd("foobar", new AdBookingModelMockBuilder().Build()));
        }

        [Test]
        public void GetAdsForUser_NullUsername_ThrowsArgumentException()
        {
            var access = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => access.GetOnlineAdsForUser(null));
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
            var results = access.GetOnlineAdsForUser("foobar").ToArray();

            results.Length.IsEqualTo(3);
            results[0].IsEqualTo(1);
            results[1].IsEqualTo(2);
            results[2].IsEqualTo(3);

        }
    }
}
