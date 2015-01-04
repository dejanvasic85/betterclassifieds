using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Tests.BusinessModel
{
    using NUnit.Framework;
    using Microsoft.Practices.Unity;
    using Moq;
    using System;
    using System.Collections.Generic;

    using Business.Managers;
    using Business.Repository;
    using Business.Models;
    using Business;
    using Mocks;

    [TestFixture]
    public class EditionManagerTests
    {
        private MockRepository _mockRepository;
        private List<Action> _verifyList;
        private IUnityContainer _container;

        [TestFixtureSetUp]
        public void InitTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _verifyList = new List<Action>();
            _container = new UnityContainer().RegisterType<IEditionManager, EditionManager>();
        }

        [TestFixtureTearDown]
        public void CleanTest()
        {
            _verifyList.ForEach(verifyMe => verifyMe());
        }

        private IEditionManager ResolveEditionManager()
        {
            return _container.Resolve<IEditionManager>();
        }

        [Test]
        public void RemoveEdition_BookingsExist_RemovesEditionAndExtends()
        {
            // Arrange 
            var editionDate = new DateTime(2013, 12, 25);
            var adBookingModels = new List<AdBookingModel>()
            {
                new AdBookingModel{ AdBookingId = 1},
                new AdBookingModel{ AdBookingId = 2},
                new AdBookingModel{ AdBookingId = 3},
            };

            _mockRepository.CreateMockOf<IEditionRepository>(_container, _verifyList)
                .SetupWithVerification(call => call.DeleteEditionByDate(It.IsAny<DateTime>()));

            _mockRepository.CreateMockOf<IBookingRepository>(_container, _verifyList)
                .SetupWithVerification(call => call.GetBookingsForEdition(It.Is<DateTime>(d => d == editionDate)), result: adBookingModels)
                .SetupWithVerification(call => call.DeleteBookEntriesForBooking(It.IsAny<int>(), It.IsAny<DateTime>()))
                ;

            var bookingManagerMock = _mockRepository.CreateMockOf<IBookingManager>(_container, _verifyList)
                .SetupWithVerification(call => call.Extend(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<bool?>(b => b == null),
                    It.Is<ExtensionStatus>(status => status == ExtensionStatus.Complete),
                    It.Is<int>(price => price == 0),
                    It.Is<string>(username => username == "admin"),
                    It.Is<PaymentType>(payment => payment == PaymentType.None)));

            // Act 
            ResolveEditionManager().RemoveEditionAndExtendBookings(editionDate);

            // Assert (check cleanup method - ensures all verifications have been met)
            // In particular, ensure that bookingManager was called three times
            bookingManagerMock.Verify(call => call.Extend(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<bool?>(b => b == null),
                    It.Is<ExtensionStatus>(status => status == ExtensionStatus.Complete),
                    It.Is<int>(price => price == 0),
                    It.Is<string>(username => username == "admin"),
                    It.Is<PaymentType>(payment => payment == PaymentType.None)), Times.AtLeast(3));
        }

        [Test]
        public void RemoveEdition_NullBookingsFound_BookingManagerNotCalled()
        {
            // Arrange 
            var editionDate = new DateTime(2013, 12, 25);
            var bookings = new List<AdBookingModel>();

            _mockRepository.CreateMockOf<IEditionRepository>(_container, _verifyList)
                .SetupWithVerification(call => call.DeleteEditionByDate(It.IsAny<DateTime>()));

            _mockRepository.CreateMockOf<IBookingRepository>(_container, _verifyList)
                .SetupWithVerification(call => call.GetBookingsForEdition(It.Is<DateTime>(d => d == editionDate)), result: bookings);

            var bookingManagerMock = _mockRepository.CreateMockOf<IBookingManager>(_container, _verifyList);

            // Act 
            ResolveEditionManager().RemoveEditionAndExtendBookings(editionDate);

            // Assert (check cleanup method - ensures all verifications have been met)
            // In particular, ensure that bookingManager was called three times
            bookingManagerMock.Verify(call => call.Extend(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<bool>(b => b == false),
                    It.Is<ExtensionStatus>(status => status == ExtensionStatus.Complete),
                    It.Is<int>(price => price == 0),
                    It.Is<string>(username => username == "admin"),
                    It.Is<PaymentType>(payment => payment == PaymentType.None)), Times.Never);
        }
    }
}
