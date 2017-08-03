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
    internal partial class EventManagerTests
    {
        [Test]
        public void CreateEventBooking_WithNoTickets_CallsRepository_AfterFactory()
        {
            // arrange
            var mockUser = new ApplicationUserMockBuilder().Build();
            var mockEvent = new EventModelMockBuilder().Default().Build();

            _dateServiceMock.SetupNow().SetupNowUtc();
            _eventRepositoryMock.SetupWithVerification(call => call.CreateBooking(It.IsAny<EventBooking>()));
            //_logService.SetupWithVerification(call => call.Info(It.IsAny<string>()));
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeePercentage, 2);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeeCents, 20);

            // act
            var manager = BuildTargetObject();
            var result = manager.CreateEventBooking(10, string.Empty, mockUser, new List<EventTicketReservation>(), null);

            // assert
            result.IsNotNull();
        }

        [Test]
        public void CreateEventBooking_WithReservations_NullBarcodeCreated_ThrowsNullReferenceException()
        {
            // arrange
            var mockUser = new ApplicationUserMockBuilder().Build();
            var mockTicket = new EventTicketMockBuilder().Default().Build();
            var mockEvent = new EventModelMockBuilder().Default().Build();

            var mockReservations = new List<EventTicketReservation>
            {
                new EventTicketReservation
                {
                    Quantity = 1,
                    EventTicketId = 1,
                    GuestEmail = "foo@bar.com",
                    GuestFullName = "Foo Bar" ,
                    Status = EventTicketReservationStatus.Reserved
                }
            };

            _dateServiceMock.SetupNow().SetupNowUtc();
            _eventRepositoryMock.SetupWithVerification(call => call.CreateBooking(It.IsAny<EventBooking>()));
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventTicketDetails(It.IsAny<int>(), It.IsAny<bool>()), mockTicket);
            //_logService.SetupWithVerification(call => call.Info(It.IsAny<string>()));
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeePercentage, 2);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeeCents, 20);

            // act
            var manager = BuildTargetObject();

            Assert.Throws<NullReferenceException>(() => manager.CreateEventBooking(10, string.Empty, mockUser, mockReservations, null));

        }

        [Test]
        public void CreateEventBooking_WithReservations_CreatesBooking_AndBarcodes()
        {
            // arrange
            var mockUser = new ApplicationUserMockBuilder().Build();
            var mockTicket = new EventTicketMockBuilder().Default().Build();
            var mockEvent = new EventModelMockBuilder().Default().Build();

            var mockReservations = new List<EventTicketReservation>
            {
                new EventTicketReservation
                {
                    Quantity = 1,
                    EventTicketId = 1,
                    GuestEmail = "foo@bar.com",
                    GuestFullName = "Foo Bar" ,
                    SeatNumber = "seat-123",
                    Status = EventTicketReservationStatus.Reserved
                }
            };

            _dateServiceMock.SetupNow().SetupNowUtc();
            _eventRepositoryMock.SetupWithVerification(call => call.CreateBooking(It.IsAny<EventBooking>()));
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventTicketDetails(It.IsAny<int>(), It.IsAny<bool>()), mockTicket);
            _eventRepositoryMock.SetupWithVerification(call => call.UpdateEventBookingTicket(It.IsAny<EventBookingTicket>()));
            _logService.SetupWithVerification(call => call.Info(It.IsAny<string>()));
            _barcodeGenerator.SetupWithVerification(call => call.CreateQr(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), result: new byte[0]);
            _eventBarcodeValidator.SetupWithVerification(call => call.GetDataForBarcode(It.IsAny<int>(), It.IsAny<EventBookingTicket>()), "111-222-333");
            _documentRepository.SetupWithVerification(call => call.Create(It.IsAny<Document>()));
            _eventRepositoryMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _eventSeatingService.SetupWithVerification(call => call.BookSeat(
                It.Is<int>(t => t == mockTicket.EventTicketId),
                It.IsAny<int>(),
                It.Is<string>(s => s == "seat-123")));
            _clientConfig.SetupWithVerification(call => call.EventTicketFeePercentage, 2);
            _clientConfig.SetupWithVerification(call => call.EventTicketFeeCents, 20);

            // act
            var manager = BuildTargetObject();

            var result = manager.CreateEventBooking(10, string.Empty, mockUser, mockReservations, barcode => $"http://localhost/barcode/{barcode}");

            result.IsNotNull();
            result.EventBookingTickets.Count.IsEqualTo(1);
            result.EventBookingTickets.Single().BarcodeImageDocumentId.IsNotNull();
        }

        [Test]
        public void CreateEventBooking_WithEventId_AsZero_ThrowsArgException()
        {
            Assert.Throws<ArgumentException>(() => BuildTargetObject().CreateEventBooking(
                eventId: 0,
                promoCode: string.Empty,
                applicationUser: new ApplicationUserMockBuilder().Build(),
                currentReservations: new List<EventTicketReservation>(),
                barcodeUrlCreator: It.IsAny<Func<string, string>>()));
        }

        [Test]
        public void CreateEventBooking_WithUser_AsNull_ThrowsArgException()
        {
            Assert.Throws<ArgumentNullException>(() => BuildTargetObject().CreateEventBooking(
                eventId: 10, 
                promoCode: string.Empty,
                applicationUser: null,
                currentReservations: new List<EventTicketReservation>(),
                barcodeUrlCreator: It.IsAny<Func<string, string>>()));
        }
    }
}
