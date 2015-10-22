using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventManagerTests : TestContext<EventManager>
    {
        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<IDateService> _dateServiceMock;
        private Mock<IClientConfig> _clientConfig;

        [SetUp]
        public void SetupDependencies()
        {
            _eventRepositoryMock = CreateMockOf<IEventRepository>();
            _dateServiceMock = CreateMockOf<IDateService>();
            _clientConfig = CreateMockOf<IClientConfig>();
        }

        [Test]
        public void GetRemainingTicketCount_TicketId_HasNoValue_ThrowsArgumentException()
        {
            var eventManager = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => eventManager.GetRemainingTicketCount(null));
        }

        [Test]
        public void GetRemainingTicketCount_WithTenReserved_WithTwentyRemaining_ReturnsTen()
        {
            // arrange
            var mockTicketDetails = new EventTicketMockBuilder()
                .WithRemainingQuantity(20)
                .WithEventTicketReservations(new EventTicketReservationMockBuilder()
                    .WithStatus(EventTicketReservationStatus.Reserved)
                    .WithExpiryDateUtc(DateTime.Now.AddMinutes(5))
                    .WithQuantity(2), howMany: 5) // 5 reservations with 2 quantities each
                .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventTicketDetails(It.Is<int>(t => t == 10), It.IsAny<bool>()), mockTicketDetails);
            _dateServiceMock.Setup(call => call.UtcNow).Returns(DateTime.Now);

            var eventManager = BuildTargetObject();
            var result = eventManager.GetRemainingTicketCount(10);

            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void ReserveTickets_CancelsExistingReservations()
        {
            // arrange
            var reservationBuilder = new EventTicketReservationMockBuilder();
            var existingReservations = new[]
            {
                reservationBuilder.WithQuantity(5).WithStatus(EventTicketReservationStatus.Reserved).Build(),
                reservationBuilder.WithQuantity(2).WithStatus(EventTicketReservationStatus.RequestTooLarge).Build()
            };

            // Mock to return the above data
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventTicketReservationsForSession(It.IsAny<string>()), existingReservations);
            _eventRepositoryMock.SetupWithVerification(call => call.UpdateEventTicketReservation(It.IsAny<EventTicketReservation>()));

            var eventManager = BuildTargetObject();
            // Pass empty list so that we just test out the above call
            eventManager.ReserveTickets("session123", new List<EventTicketReservationRequest>());

            Assert.That(existingReservations[0].Status == EventTicketReservationStatus.Cancelled);
            Assert.That(existingReservations[1].Status == EventTicketReservationStatus.Cancelled);
        }

        [Test]
        public void ReserveTickets_WithTwoSufficientRequests_CreatesTwoReserved()
        {
            const int expiryMins = 5;
            var eventTicketRequestMockBuilder = new EventTicketReservationRequestMockBuilder();
            var eventTicketMockBuilder = new EventTicketMockBuilder()
                .WithRemainingQuantity(10);


            // arrange repository calls
            _eventRepositoryMock.SetupWithVerification(call => call.CreateEventTicketReservation(It.IsAny<EventTicketReservation>()));
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventTicketDetails(It.IsAny<int>(), It.IsAny<bool>()), eventTicketMockBuilder.Build());
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventTicketReservationsForSession(It.IsAny<string>()), new EventTicketReservation[] { });
            _dateServiceMock.SetupWithVerification(call => call.Now, DateTime.Now);
            _dateServiceMock.SetupWithVerification(call => call.UtcNow, DateTime.UtcNow);
            _clientConfig.SetupWithVerification(call => call.EventTicketReservationExpiryMinutes, expiryMins);


            // act
            var eventManager = BuildTargetObject();
            eventManager.ReserveTickets("session123", new List<EventTicketReservationRequest>
            {
                eventTicketRequestMockBuilder
                    .WithQuantity(1)
                    .WithEventTicket(eventTicketMockBuilder.WithEventTicketId(111).Build())
                    .Build(),

                eventTicketRequestMockBuilder
                    .WithQuantity(2)
                    .WithEventTicket(eventTicketMockBuilder.WithEventId(222).Build())
                    .Build()
            });

            // assert - check teardown on all the repository calls (with verifications)
        }

        [Test]
        public void ReserveTickets_WithNullEventTicket_ThrowsArgumentException()
        {
            const int expiryMins = 5;
            var eventTicketRequestMockBuilder = new EventTicketReservationRequestMockBuilder();


            // arrange repository calls
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventTicketReservationsForSession(It.IsAny<string>()), new EventTicketReservation[] { });

            // act
            var eventManager = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => eventManager.ReserveTickets("session123", new List<EventTicketReservationRequest>
            {
                eventTicketRequestMockBuilder
                    .WithQuantity(1)
                    .Build(),

                eventTicketRequestMockBuilder
                    .WithQuantity(2)
                    .Build()
            }));

            // assert - check teardown on all the repository calls (with verifications)
        }
    }
}