using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    internal class EventBarcodeManagerTests : TestContext<EventBarcodeManager>
    {
        [Test]
        public void GenerateBarcodeData_WithValidObjects_ReturnsString()
        {
            // arrange
            var eventModel = new EventModelMockBuilder().WithEventId(3333).Build();
            var eventBookingTicket = new EventBookingTicketMockBuilder()
                .WithEventBookingTicketId(2222)
                .WithEventTicketId(1111).Build();

            // act
            var eventBarcodeManager = BuildTargetObject();
            var result = eventBarcodeManager.GenerateBarcodeData(eventModel, eventBookingTicket);

            Assert.That(result, Is.EqualTo("3333 1111 2222"));
        }

        [Test]
        public void ValidateTicket_WithNull_ThrowsArgumentNullException()
        {
            var eventBarcodeManager = BuildTargetObject();
            Assert.Throws<ArgumentNullException>(() => eventBarcodeManager.ValidateTicket(null));
        }

        [Test]
        [TestCase("BadLength")]
        [TestCase("NotAll AreNumbers 1111")]
        public void ValidateTicket_WithBadBarcodeData_ReturnsFailed(string badData)
        {
            var eventBarcodeManager = BuildTargetObject();
            var result = eventBarcodeManager.ValidateTicket(badData);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ValidationMessage, Is.EqualTo("NOT VALID: Unknown barcode information."));
            Assert.That(result.ValidationType, Is.EqualTo(EventBookingTicketValidationType.Failed));
        }

        [Test]
        public void ValidateTicket_Event_IsNull_ReturnsFailed()
        {
            // arrange
            _eventRepository.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), null);

            // act
            var eventBarcodeManager = BuildTargetObject();
            var result = eventBarcodeManager.ValidateTicket("1111 2222 3333");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ValidationMessage, Is.EqualTo("NO SUCH EVENT: 1111"));
            Assert.That(result.ValidationType, Is.EqualTo(EventBookingTicketValidationType.Failed));
        }

        [Test]
        public void ValidateTicket_EventTicket_IsNull_ReturnsFailed()
        {
            // arrange
            var eventModel = new EventModelMockBuilder()
                .WithEventId(3333)
                .Build();

            _eventRepository.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), eventModel);

            // act
            var eventBarcodeManager = BuildTargetObject();
            var result = eventBarcodeManager.ValidateTicket("1111 2222 3333");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ValidationMessage, Is.EqualTo("NO SUCH TICKET: Event [1111] Ticket [2222]"));
            Assert.That(result.ValidationType, Is.EqualTo(EventBookingTicketValidationType.Failed));
        }

        [Test]
        public void ValidateTicket_EventBookingTicket_IsNull_ReturnsFailed()
        {
            // arrange
            var eventModel = new EventModelMockBuilder()
                .WithEventId(3333)
                .WithCustomTicket(new EventTicketMockBuilder().WithEventTicketId(2222).Build())
                .Build();
            

            _eventRepository.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), eventModel);
            _eventRepository.SetupWithVerification(call => call.GetEventBookingTicket(It.IsAny<int>()), null);

            // act
            var eventBarcodeManager = BuildTargetObject();
            var result = eventBarcodeManager.ValidateTicket("1111 2222 3333");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ValidationMessage, Is.EqualTo("NO SUCH TICKET BOOKING: Event [1111] Ticket [2222] Ticket Booking [3333]"));
            Assert.That(result.ValidationType, Is.EqualTo(EventBookingTicketValidationType.Failed));
        }

        [Test]
        public void ValidateTicket_WithExistingValidation_ReturnsPartialSuccess()
        {
            // arrange
            var eventModel = new EventModelMockBuilder()
                .WithEventId(1111)
                .WithCustomTicket(new EventTicketMockBuilder().WithEventTicketId(2222).Build())
                .Build();
            var eventTicketBooking = new EventBookingTicketMockBuilder().WithEventBookingTicketId(3333).Build();
            var eventTicketBookingValidation = new EventBookingTicketValidationMockBuilder().WithValidationCount(1).WithEventBookingTicketId(3333).Build();


            _eventRepository.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), eventModel);
            _eventRepository.SetupWithVerification(call => call.GetEventBookingTicket(It.IsAny<int>()), eventTicketBooking);
            _eventRepository.SetupWithVerification(call => call.GetEventBookingTicketValidation(It.IsAny<int>()), eventTicketBookingValidation);
            _eventRepository.SetupWithVerification(call => call.UpdateEventBookingTicketValidation(It.IsAny<EventBookingTicketValidation>()));

            // act
            var eventBarcodeManager = BuildTargetObject();
            var result = eventBarcodeManager.ValidateTicket("1111 2222 3333");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ValidationType, Is.EqualTo(EventBookingTicketValidationType.PartialSuccess));
            Assert.That(eventTicketBookingValidation.ValidationCount, Is.EqualTo(2));
        }

        [Test]
        public void ValidateTicket_CreatesNewValidationRecord_ReturnsSuccess()
        {
            // arrange
            var eventModel = new EventModelMockBuilder()
                .WithEventId(1111)
                .WithCustomTicket(new EventTicketMockBuilder().WithEventTicketId(2222).Build())
                .Build();
            var eventTicketBooking = new EventBookingTicketMockBuilder().WithEventBookingTicketId(3333).Build();
            
            _eventRepository.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), eventModel);
            _eventRepository.SetupWithVerification(call => call.GetEventBookingTicket(It.IsAny<int>()), eventTicketBooking);
            _eventRepository.SetupWithVerification(call => call.GetEventBookingTicketValidation(It.IsAny<int>()), null);
            _eventRepository.SetupWithVerification(call => call.CreateEventBookingTicketValidation(It.IsAny<EventBookingTicketValidation>()));

            // act
            var eventBarcodeManager = BuildTargetObject();
            var result = eventBarcodeManager.ValidateTicket("1111 2222 3333");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ValidationType, Is.EqualTo(EventBookingTicketValidationType.Success));
        }

        private Mock<IEventRepository> _eventRepository;

        [SetUp]
        public void Setup()
        {
            _eventRepository = CreateMockOf<IEventRepository>();
        }
    }
}