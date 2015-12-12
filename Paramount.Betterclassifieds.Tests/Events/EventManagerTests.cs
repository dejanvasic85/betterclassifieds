using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    internal class EventManagerTests : TestContext<EventManager>
    {
        [Test]
        public void GetRemainingTicketCount_TicketId_HasNoValue_ThrowsArgumentException()
        {
            var eventManager = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => eventManager.GetRemainingTicketCount((int?)null));
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
        public void CreateEventTicketsDocument_CallsDocumentRepository_ReturnsDocumentId()
        {
            var mockEventBookingId = 10;
            var mockSentDateTime = DateTime.Now;
            var mockEventBooking = new EventBookingMockBuilder().WithEventBookingId(mockEventBookingId).Build();

            _documentRepository.SetupWithVerification(call => call.Save(It.IsAny<Document>()));
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBooking(It.IsAny<int>(), It.IsAny<bool>()), result: mockEventBooking);
            _eventRepositoryMock.SetupWithVerification(call => call.UpdateEventBooking(It.Is<EventBooking>(eb => eb == mockEventBooking)));

            var eventManager = this.BuildTargetObject();
            var documentId = eventManager.CreateEventTicketsDocument(mockEventBookingId, new byte[0], ticketsSentDate: mockSentDateTime);

            Assert.That(documentId, Is.Not.Null);
            Assert.That(mockEventBooking.TicketsDocumentId, Is.Not.Null);
            Assert.That(mockEventBooking.TicketsSentDate, Is.EqualTo(mockSentDateTime));
            Assert.That(mockEventBooking.TicketsSentDateUtc, Is.Not.Null);
        }

        [Test]
        public void BuildGuestList_ForEvent_ReturnsListOfGuests_WithAllData()
        {
            var eventMock = new EventModelMockBuilder().WithEventId(10).Build();
            var ticketBuilder = new EventBookingTicketMockBuilder()
                .WithEventTicketId(900)
                .WithTicketName("General Admission")
                .WithGuestFullName("Morgan Freeman")
                .WithGuestEmail("fake@email.com");

            var mockTickets = new[]
            {
                ticketBuilder.WithEventBookingTicketId(1).Build(),
                ticketBuilder.WithEventBookingTicketId(2).Build()
            };

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(param => param == 10)), eventMock);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingTicketsForEvent(It.Is<int>(param => param == 10)), mockTickets);

            var result = this.BuildTargetObject().BuildGuestList(10).ToList();


            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].GuestFullName, Is.EqualTo("Morgan Freeman"));
            Assert.That(result[0].GuestEmail, Is.EqualTo("fake@email.com"));
            Assert.That(result[0].BarcodeData, Is.Not.Null);
            Assert.That(result[0].TicketNumber, Is.EqualTo(1));
            Assert.That(result[0].TicketName, Is.EqualTo("General Admission"));
        }

        [Test]
        public void BuildPaymentSummary_NullArg_ThrowsArgException()
        {
            Assert.Throws<ArgumentNullException>(() => BuildTargetObject().BuildPaymentSummary(null));
        }

        [Test]
        public void BuildPaymentSummary_WithZeroFee_OrganiserShouldGetEntireSales()
        {
            // arrange
            var eventId = 100;
            var mockBuilder = new EventBookingMockBuilder().WithEventId(eventId);
            var mockEventBookings = new[]
            {
                mockBuilder.WithEventBookingId(1).WithTotalCost(10).Build(),
                mockBuilder.WithEventBookingId(2).WithTotalCost(20).Build(),
                mockBuilder.WithEventBookingId(3).WithTotalCost(30).Build(),
                mockBuilder.WithEventBookingId(4).WithTotalCost(40).Build()
            };

            _clientConfig.SetupWithVerification(call => call.EventTicketFee, 0);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingsForEvent(It.Is<int>(i => i == eventId), false), mockEventBookings);

            // act
            var result = BuildTargetObject().BuildPaymentSummary(eventId);

            // assert
            Assert.That(result.TotalTicketSalesAmount, Is.EqualTo(100));
            Assert.That(result.EventOrganiserOwedAmount, Is.EqualTo(100));
        }

        [Test]
        public void BuildPaymentSummary_WithOneAndHalfPercentFee()
        {
            // arrange
            var eventId = 100;
            var mockBuilder = new EventBookingMockBuilder().WithEventId(eventId);
            var mockEventBookings = new[]
            {
                mockBuilder.WithEventBookingId(1).WithTotalCost(10).Build(),
                mockBuilder.WithEventBookingId(2).WithTotalCost(20).Build(),
                mockBuilder.WithEventBookingId(3).WithTotalCost(30).Build(),
                mockBuilder.WithEventBookingId(4).WithTotalCost(40).Build()
            };

            _clientConfig.SetupWithVerification(call => call.EventTicketFee, (decimal)1.5);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingsForEvent(It.Is<int>(i => i == eventId), false), mockEventBookings);

            // act
            var result = BuildTargetObject().BuildPaymentSummary(eventId);

            // assert
            Assert.That(result.TotalTicketSalesAmount, Is.EqualTo(100));
            Assert.That(result.EventOrganiserOwedAmount, Is.EqualTo((decimal)98.5));
        }

        [Test]
        public void BuildPaymentSummary_WithHundredPercentFee_ClientGetsNothing()
        {
            // arrange
            var eventId = 100;
            var mockBuilder = new EventBookingMockBuilder().WithEventId(eventId);
            var mockEventBookings = new[]
            {
                mockBuilder.WithEventBookingId(1).WithTotalCost(10).Build(),
                mockBuilder.WithEventBookingId(2).WithTotalCost(20).Build(),
                mockBuilder.WithEventBookingId(3).WithTotalCost(30).Build(),
                mockBuilder.WithEventBookingId(4).WithTotalCost(40).Build()
            };

            _clientConfig.SetupWithVerification(call => call.EventTicketFee, 100);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingsForEvent(It.Is<int>(i => i == eventId), false), mockEventBookings);

            // act
            var result = BuildTargetObject().BuildPaymentSummary(eventId);

            // assert
            Assert.That(result.TotalTicketSalesAmount, Is.EqualTo(100));
            Assert.That(result.EventOrganiserOwedAmount, Is.EqualTo(0));
        }

        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<IDateService> _dateServiceMock;
        private Mock<IDocumentRepository> _documentRepository;
        private Mock<IClientConfig> _clientConfig;

        [SetUp]
        public void SetupDependencies()
        {
            _eventRepositoryMock = CreateMockOf<IEventRepository>();
            _dateServiceMock = CreateMockOf<IDateService>();
            _clientConfig = CreateMockOf<IClientConfig>();
            _documentRepository = CreateMockOf<IDocumentRepository>();
        }
    }
}