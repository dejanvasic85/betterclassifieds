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