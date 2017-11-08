using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventBookingTicketFactoryTests : TestContext<EventBookingTicketFactory>
    {
        private Mock<IEventRepository> _eventRepository;
        private Mock<IDateService> _dateService;
        private Mock<ITicketFeeCalculator> _ticketFeeCalculator;


        [SetUp]
        public void Setup()
        {
            _eventRepository = CreateMockOf<IEventRepository>();
            _dateService = CreateMockOf<IDateService>();
            _ticketFeeCalculator = CreateMockOf<ITicketFeeCalculator>();
        }

        [Test]
        public void CreateFromExisting_Clones_AndReturnsNew()
        {
            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockEventPromo = new EventPromoCodeMockBuilder().Build();
            var mockReservation = new EventTicketReservationMockBuilder().Default().Build();
            var mockReservations = new[]
            {
                mockReservation
            };
            var createdDate = DateTime.Now;
            var createUtcDate = DateTime.UtcNow;
            var mockTicket = new EventTicketMockBuilder().Default().Build();

            _eventRepository.SetupWithVerification(call =>
                call.GetEventTicketDetails(It.IsAny<int>(), It.IsAny<bool>()), mockTicket);

            var mockTicketPrice = new TicketPrice(100, 100, 0);

            _ticketFeeCalculator.SetupWithVerification(call => call.GetTotalTicketPrice(
                It.Is<decimal>(t => t == 55),
                It.Is<EventPromoCode>(p => p == mockEventPromo),    
                It.Is<bool>(includeFee => includeFee == mockEvent.IncludeTransactionFee)),
                mockTicketPrice);

            // Act
            var factory = BuildTargetObject();
            var result = factory.CreateFromReservation(mockEvent, mockReservation, mockEventPromo, createdDate, createUtcDate);
            result.IsNotNull();
            var eventBookingTicket = result.Single();

            // Assert
            ((int?)eventBookingTicket.EventTicketId).IsEqualTo(mockTicket.EventTicketId);
            eventBookingTicket.TicketImage.IsEqualTo(mockTicket.TicketImage);
            eventBookingTicket.TicketName.IsEqualTo(mockTicket.TicketName);
            eventBookingTicket.IsPublic.IsEqualTo(mockReservation.IsPublic);
            eventBookingTicket.TransactionFee.IsEqualTo(mockTicketPrice.Fee);

        }
    }
}
