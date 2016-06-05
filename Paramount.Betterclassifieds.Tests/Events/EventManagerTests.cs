using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Location;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    internal class EventManagerTests : TestContext<EventManager>
    {
        [Test]
        public void GetEventDetailsForOnlineAdId_CallsRepository()
        {
            // arrange
            var mockEvent = new EventModelMockBuilder().Default().Build();
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetailsForOnlineAdId(10, true), mockEvent);

            // act
            var result = BuildTargetObject().GetEventDetailsForOnlineAdId(10, true);

            // assert
            result.IsEqualTo(mockEvent);
        }

        [Test]
        public void CreateEventBooking_CallsRepository_AfterFactory()
        {
            // arrange
            var mockUser = new ApplicationUserMockBuilder().Build();

            _dateServiceMock.SetupNow().SetupNowUtc();
            _eventRepositoryMock.SetupWithVerification(call => call.CreateBooking(It.IsAny<EventBooking>()));

            // act
            var manager = BuildTargetObject();
            manager.CreateEventBooking(10, mockUser, new List<EventTicketReservation>());
        }

        [Test]
        public void CreateEventBooking_WithEventId_AsZero_ThrowsArgException()
        {
            Assert.Throws<ArgumentException>(() => BuildTargetObject().CreateEventBooking(
                eventId: 0,
                applicationUser: new ApplicationUserMockBuilder().Build(),
                currentReservations: new List<EventTicketReservation>()));
        }

        [Test]
        public void CreateEventBooking_WithUser_AsNull_ThrowsArgException()
        {
            Assert.Throws<ArgumentNullException>(() => BuildTargetObject().CreateEventBooking(
                eventId: 10,
                applicationUser: null,
                currentReservations: new List<EventTicketReservation>()));
        }

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
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBooking(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>()), result: mockEventBooking);
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
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingTicketsForEvent(
                It.Is<int>(param => param == 10)),
                mockTickets);

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
            var mockBookingTickets = new[]
             {
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(10).WithTotalPrice(10).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(20).WithTotalPrice(20).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(30).WithTotalPrice(30).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(40).WithTotalPrice(40).Build(),
            };
            var mockEvent = new EventModelMockBuilder()
                .Default()
                .WithIncludeTransactionFee(false)
                .Build();

            _clientConfig.SetupWithVerification(call => call.EventTicketFeePercentage, 0);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeeCents, 0);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingTicketsForEvent(It.IsAny<int>()), mockBookingTickets);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);

            // act
            var result = BuildTargetObject().BuildPaymentSummary(eventId);

            // assert
            Assert.That(result.TotalTicketSalesAmount, Is.EqualTo(100));
            Assert.That(result.EventOrganiserOwedAmount, Is.EqualTo(100));
        }

        [Test]
        public void BuildPaymentSummary_WithZeroFee_AndThirtyCents_OrganiserPays()
        {
            // arrange
            var eventId = 100;
            var mockBookingTickets = new[]
             {
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(10).WithTotalPrice(10).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(20).WithTotalPrice(20).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(30).WithTotalPrice(30).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(40).WithTotalPrice(40).Build(),
            };
            var mockEvent = new EventModelMockBuilder()
                .Default()
                .WithIncludeTransactionFee(false)
                .Build();

            _clientConfig.SetupWithVerification(call => call.EventTicketFeePercentage, 0);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeeCents, 50);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingTicketsForEvent(It.IsAny<int>()), mockBookingTickets);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);

            // act
            var result = BuildTargetObject().BuildPaymentSummary(eventId);

            // assert
            Assert.That(result.TotalTicketSalesAmount, Is.EqualTo(100));
            Assert.That(result.EventOrganiserOwedAmount, Is.EqualTo(98));
        }

        [Test]
        public void BuildPaymentSummary_WithOneAndHalfPercentFee()
        {
            // arrange
            var eventId = 100;
            var mockBookingTickets = new[]
             {
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(10).WithTotalPrice(10).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(20).WithTotalPrice(20).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(30).WithTotalPrice(30).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(40).WithTotalPrice(40).Build(),
            };
            var mockEvent = new EventModelMockBuilder()
               .Default()
               .WithIncludeTransactionFee(false)
               .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeePercentage, (decimal)1.5);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeeCents, 0);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingTicketsForEvent(It.IsAny<int>()), mockBookingTickets);

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
            var mockBookingTickets = new[]
             {
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(10).WithTotalPrice(10).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(20).WithTotalPrice(20).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(30).WithTotalPrice(30).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(40).WithTotalPrice(40).Build(),
            };
            var mockEvent = new EventModelMockBuilder()
               .Default()
               .WithIncludeTransactionFee(false)
               .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeePercentage, 100);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeeCents, 0);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingTicketsForEvent(It.IsAny<int>()), mockBookingTickets);

            // act
            var result = BuildTargetObject().BuildPaymentSummary(eventId);

            // assert
            Assert.That(result.TotalTicketSalesAmount, Is.EqualTo(100));
            Assert.That(result.EventOrganiserOwedAmount, Is.EqualTo(0));
        }

        [Test]
        public void BuildPaymentSummary_WithConsumerAbsorbingTheFee_OrganiserGetsEverything()
        {
            // arrange
            var eventId = 100;

            var mockBookingTickets = new[]
            {
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(10).WithTotalPrice(10).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(20).WithTotalPrice(20).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(30).WithTotalPrice(30).Build(),
                new EventBookingTicketMockBuilder().WithEventBookingTicketId(1).WithPrice(40).WithTotalPrice(40).Build(),
            };

            var mockEvent = new EventModelMockBuilder()
               .Default()
               .WithIncludeTransactionFee(true)
               .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingTicketsForEvent(It.IsAny<int>()), mockBookingTickets);

            // act
            var result = BuildTargetObject().BuildPaymentSummary(eventId);

            // assert
            Assert.That(result.TotalTicketSalesAmount, Is.EqualTo(100));
            Assert.That(result.EventOrganiserOwedAmount, Is.EqualTo(100));
        }


        [Test]
        public void CreateEventPaymentRequest_CallsRepository_AfterFactoryCreatesObject()
        {
            _dateServiceMock.SetupNow().SetupNowUtc();
            _eventRepositoryMock.SetupWithVerification(call => call.CreateEventPaymentRequest(It.IsAny<EventPaymentRequest>()));

            BuildTargetObject().CreateEventPaymentRequest(eventId: 10,
                paymentType: PaymentType.PayPal,
                requestedAmount: 100,
                requestedByUser: "FooBarr");
        }

        [Test]
        public void CreateEventPaymentRequest_WithPaymentType_None_ThrowsArgException()
        {
            Assert.Throws<ArgumentException>(() => BuildTargetObject()
                .CreateEventPaymentRequest(1, PaymentType.None, 1, "fooBar"));
        }

        [Test]
        public void CreateEventPaymentRequest_WithPaymentType_CreditCard_ThrowsArgException()
        {
            Assert.Throws<ArgumentException>(() => BuildTargetObject()
                .CreateEventPaymentRequest(1, PaymentType.CreditCard, 1, "fooBar"));
        }

        [Test]
        public void CreateEventPaymentRequest_With_EventIdAsZero_ThrowsArgException()
        {
            Assert.Throws<ArgumentException>(() => BuildTargetObject()
                .CreateEventPaymentRequest(0, PaymentType.None, 0, "fooBar"));
        }

        [Test]
        public void CreateEventPaymentRequest_With_RequestedAmountAsZero_ThrowsArgException()
        {
            Assert.Throws<ArgumentException>(() => BuildTargetObject()
                .CreateEventPaymentRequest(1, PaymentType.None, 0, "fooBar"));
        }

        [Test]
        public void CreateEventTicket_CallsRepository_AfterFactory()
        {
            _eventRepositoryMock.SetupWithVerification(call => call.CreateEventTicket(It.IsAny<EventTicket>()));
            BuildTargetObject().CreateEventTicket(eventId: 10,
                ticketName: "Adult",
                price: 100,
                remainingQuantity: 50);
        }

        [Test]
        public void CreateEventTicket_With_EventIdAsZero_ThrowsArgException()
        {
            Assert.Throws<ArgumentException>(() => BuildTargetObject().CreateEventTicket(eventId: 0,
                ticketName: "Adult",
                price: 100,
                remainingQuantity: 50));
        }

        [Test]
        public void GetEventPaymentRequest_WithNullEventId_ThrowsArgException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                BuildTargetObject().GetEventPaymentRequestStatus(null);
            });
        }

        [Test]
        public void GetEventPaymentRequestStatus_Complete()
        {
            // arrange
            var eventId = 10;
            var mockEvent = new EventModelMockBuilder().WithEventId(eventId)
                .WithClosingDateUtc(DateTime.UtcNow.AddDays(-1))
                .Build();

            var mockEventPaymentRequest = new EventPaymentRequestMockBuilder()
                .WithEventId(eventId)
                .WithIsPaymentProcessed(true)
                .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventPaymentRequestForEvent(It.Is<int>(p => p == eventId)), mockEventPaymentRequest);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);

            // act
            var result = BuildTargetObject().GetEventPaymentRequestStatus(eventId);

            // assert
            Assert.That(result, Is.TypeOf<EventPaymentRequestStatus>());
            Assert.That(result, Is.EqualTo(EventPaymentRequestStatus.Complete));
        }

        [Test]
        public void GetEventPaymentRequestStatus_PaymentPending()
        {
            // arrange
            var eventId = 10;
            var mockEvent = new EventModelMockBuilder().WithEventId(eventId)
                .WithClosingDateUtc(DateTime.UtcNow.AddDays(-1))
                .Build();

            var mockEventPaymentRequest = new EventPaymentRequestMockBuilder()
                .WithEventId(eventId)
                .WithIsPaymentProcessed(false)
                .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventPaymentRequestForEvent(It.Is<int>(p => p == eventId)), mockEventPaymentRequest);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);

            // act
            var result = BuildTargetObject().GetEventPaymentRequestStatus(eventId);

            // assert
            Assert.That(result, Is.TypeOf<EventPaymentRequestStatus>());
            Assert.That(result, Is.EqualTo(EventPaymentRequestStatus.PaymentPending));
        }

        [Test]
        public void GetEventPaymentRequestStatus_NotAvailable_WithNoTickets()
        {
            // arrange
            var eventId = 10;
            var mockEvent = new EventModelMockBuilder()
                .WithEventId(eventId)
                .WithClosingDateUtc(DateTime.UtcNow.AddDays(-1))
                .WithTickets(null).Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventPaymentRequestForEvent(It.Is<int>(p => p == eventId)), null);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);

            // act
            var result = BuildTargetObject().GetEventPaymentRequestStatus(eventId);

            // assert
            Assert.That(result, Is.TypeOf<EventPaymentRequestStatus>());
            Assert.That(result, Is.EqualTo(EventPaymentRequestStatus.NotAvailable));
        }

        [Test]
        public void GetEventPaymentRequestStatus_NotAvailable_WithFreeTickets()
        {
            // arrange
            var eventId = 10;
            var mockEvent = new EventModelMockBuilder()
                .WithEventId(eventId)
                .WithClosingDateUtc(DateTime.UtcNow.AddDays(-1))
                .WithTickets(new List<EventTicket> { new EventTicketMockBuilder().WithEventId(eventId).WithPrice(0).Build() })
                .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventPaymentRequestForEvent(It.Is<int>(p => p == eventId)), null);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);

            // act
            var result = BuildTargetObject().GetEventPaymentRequestStatus(eventId);

            // assert
            Assert.That(result, Is.TypeOf<EventPaymentRequestStatus>());
            Assert.That(result, Is.EqualTo(EventPaymentRequestStatus.NotAvailable));
        }

        [Test]
        public void GetEventPaymentRequestStatus_WithClosedEvent_RequestPending()
        {
            // arrange
            var eventId = 10;
            var mockEvent = new EventModelMockBuilder()
                .WithEventId(eventId)
                .WithClosingDateUtc(DateTime.UtcNow.AddDays(-1))
                .WithTickets(new List<EventTicket> { new EventTicketMockBuilder().WithEventId(eventId).WithPrice(10).Build() })
                .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventPaymentRequestForEvent(It.Is<int>(p => p == eventId)), null);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);
            _dateServiceMock.SetupNowUtc();

            // act
            var result = BuildTargetObject().GetEventPaymentRequestStatus(eventId);

            // assert
            Assert.That(result, Is.TypeOf<EventPaymentRequestStatus>());
            Assert.That(result, Is.EqualTo(EventPaymentRequestStatus.RequestPending));
        }

        [Test]
        public void GetEventPaymentRequestStatus_WithClosedDateAsNull_CloseEventFirst()
        {
            // arrange
            var eventId = 10;
            var mockEvent = new EventModelMockBuilder()
                .WithEventId(eventId)
                .WithClosingDateUtc(null)
                .WithTickets(new List<EventTicket> { new EventTicketMockBuilder().WithEventId(eventId).WithPrice(10).Build() })
                .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventPaymentRequestForEvent(It.Is<int>(p => p == eventId)), null);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);

            // act
            var result = BuildTargetObject().GetEventPaymentRequestStatus(eventId);

            // assert
            Assert.That(result, Is.TypeOf<EventPaymentRequestStatus>());
            Assert.That(result, Is.EqualTo(EventPaymentRequestStatus.CloseEventFirst));
        }

        [Test]
        public void GetEventPaymentRequestStatus_WithClosedDateInFuture_CloseEventFirst()
        {
            // arrange
            var eventId = 10;
            var mockEvent = new EventModelMockBuilder()
                .WithEventId(eventId)
                .WithClosingDateUtc(DateTime.UtcNow.AddDays(10))
                .WithTickets(new List<EventTicket> { new EventTicketMockBuilder().WithEventId(eventId).WithPrice(10).Build() })
                .Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventPaymentRequestForEvent(It.Is<int>(p => p == eventId)), null);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);
            _dateServiceMock.SetupNowUtc();

            // act
            var result = BuildTargetObject().GetEventPaymentRequestStatus(eventId);

            // assert
            Assert.That(result, Is.TypeOf<EventPaymentRequestStatus>());
            Assert.That(result, Is.EqualTo(EventPaymentRequestStatus.CloseEventFirst));
        }

        [Test]
        public void CloseEvent()
        {
            int eventId = 100;
            var mockEvent = new EventModelMockBuilder().WithEventId(eventId).Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);
            _eventRepositoryMock.SetupWithVerification(call => call.UpdateEvent(It.Is<EventModel>(p => p == mockEvent)));

            BuildTargetObject().CloseEvent(eventId);

            Assert.That(mockEvent.ClosingDate, Is.Not.Null);
            Assert.That(mockEvent.ClosingDateUtc, Is.Not.Null);
        }

        [Test]
        public void IsEventEditable_NoBookings_CanEdit()
        {
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingsForEvent(It.IsAny<int>(),
                It.IsAny<bool>(), false), new List<EventBooking>());

            var manager = BuildTargetObject();
            Assert.That(manager.IsEventEditable(10), Is.True);
        }

        [Test]
        public void IsEventEditable_HasOneBooking_CannotEdit()
        {
            var booking = new EventBookingMockBuilder().Build();

            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBookingsForEvent(It.IsAny<int>(),
                It.IsAny<bool>(), false), new List<EventBooking> { booking });

            var manager = BuildTargetObject();
            Assert.That(manager.IsEventEditable(10), Is.False);
        }

        [Test]
        public void EventBookingPaymentCompleted_NullEventBookingId_ThrowsArgException()
        {
            Assert.Throws<ArgumentNullException>(() => BuildTargetObject().ActivateBooking(null, It.IsAny<long>()));
        }

        [Test]
        public void EventBookingPaymentCompleted_WithInvitation_StatusBecomesActive()
        {
            var eventBooking = new EventBookingMockBuilder().Default()
                .WithStatus(EventBookingStatus.PaymentPending)
                .Build();

            var eventInvitation = new EventInvitationMockBuilder().WithEventInvitationId(1234).Build();

            // Calls
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventBooking(It.IsAny<int>(),
                It.IsAny<bool>(), It.IsAny<bool>()), eventBooking);
            _dateServiceMock.SetupWithVerification(call => call.Now, DateTime.Now);
            _dateServiceMock.SetupWithVerification(call => call.UtcNow, DateTime.UtcNow);
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventInvitation(It.IsAny<long>()), eventInvitation);
            _eventRepositoryMock.SetupWithVerification(call => call.UpdateEventBooking(It.Is<EventBooking>(b => b == eventBooking)));
            _eventRepositoryMock.SetupWithVerification(call => call.UpdateEventInvitation(It.Is<EventInvitation>(e => e == eventInvitation)));

            BuildTargetObject().ActivateBooking(100, eventInvitation.EventInvitationId);
            eventBooking.Status.IsEqualTo(EventBookingStatus.Active);
            eventInvitation.ConfirmedDate.IsNotNull();
            eventInvitation.ConfirmedDateUtc.IsNotNull();
        }

        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<IDateService> _dateServiceMock;
        private Mock<IDocumentRepository> _documentRepository;
        private Mock<IClientConfig> _clientConfig;
        private Mock<IBookingManager> _bookingManager;
        private Mock<ILocationService> _locationService;

        [SetUp]
        public void SetupDependencies()
        {
            _eventRepositoryMock = CreateMockOf<IEventRepository>();
            _dateServiceMock = CreateMockOf<IDateService>();
            _clientConfig = CreateMockOf<IClientConfig>();
            _documentRepository = CreateMockOf<IDocumentRepository>();
            _bookingManager = CreateMockOf<IBookingManager>();
            _locationService = CreateMockOf<ILocationService>();
        }
    }
}