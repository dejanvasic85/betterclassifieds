using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventBookingFactoryTests
    {
        [Test]
        public void Create_WithNoReservations_ReturnsNew_EventBooking()
        {
            // arrange
            var eventId = 10;
            var eventTicketReservations = new List<EventTicketReservation>();
            var applicationUser = new ApplicationUserMockBuilder().Default().Build();
            var mockRepository = new Mock<IEventRepository>();
            var mockDateService = new Mock<IDateService>();
            var clientConfig = new Mock<IClientConfig>();
            clientConfig.Setup(prop => prop.EventTicketFeePercentage).Returns(2);
            clientConfig.Setup(prop => prop.EventTicketFeeCents).Returns(30);
            var feeCalculator = new TicketFeeCalculator(clientConfig.Object);
            mockDateService.SetupNow().SetupNowUtc();

            // act
            var factory = new EventBookingFactory(mockRepository.Object, mockDateService.Object, feeCalculator);

            var result = factory.Create(
                eventId,
                null,
                applicationUser,
                eventTicketReservations);

            Assert.That(result, Is.TypeOf<EventBooking>());
            Assert.That(result.EventId, Is.EqualTo(eventId));
            Assert.That(result.EventBookingId, Is.EqualTo(0));
            Assert.That(result.CreatedDateTime, Is.Not.Null);
            Assert.That(result.CreatedDateTimeUtc, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(EventBookingStatus.Active));
            Assert.That(result.EventBookingTickets, Is.Not.Null);
            Assert.That(result.EventBookingTickets.Count, Is.EqualTo(0));
            Assert.That(result.TotalCost, Is.EqualTo(0));

            // Ensure all user information
            Assert.That(result.UserId, Is.EqualTo(applicationUser.Username));
            Assert.That(result.Email, Is.EqualTo(applicationUser.Email));
            Assert.That(result.FirstName, Is.EqualTo(applicationUser.FirstName));
            Assert.That(result.LastName, Is.EqualTo(applicationUser.LastName));
            Assert.That(result.Phone, Is.EqualTo(applicationUser.Phone));
            Assert.That(result.PostCode, Is.EqualTo(applicationUser.Postcode));

        }
        

        [Test]
        public void Create_WithReservations_ReturnsNew_EventBooking_WithTicketsAndCost()
        {
            // arrange
            var eventId = 10;
            var reservationMockBuilder = new EventTicketReservationMockBuilder()
                .WithEventTicketId(299)
                .WithEventTicket(new EventTicketMockBuilder().Build())
                .WithGuestFullName("guest name")
                .WithQuantity(2)
                .WithTransactionFee(2)
                .WithPrice(10);
            var mockRepository = new Mock<IEventRepository>();
            var mockEventTicket = new EventTicketMockBuilder().WithEventTicketId(1).WithTicketName("ticket-123").Build();
            mockRepository.Setup(call => call.GetEventTicketDetails(It.IsAny<int>(), It.IsAny<bool>())).Returns(mockEventTicket);

            var eventTicketReservations = new List<EventTicketReservation>
            {
                reservationMockBuilder.Build(),
                reservationMockBuilder.Build()
            };
            var mockDateService = new Mock<IDateService>();
            mockDateService.SetupNow().SetupNowUtc();
            var applicationUser = new ApplicationUserMockBuilder().Default().Build();
            var clientConfig = new Mock<IClientConfig>();
            clientConfig.Setup(prop => prop.EventTicketFeePercentage).Returns(2);
            clientConfig.Setup(prop => prop.EventTicketFeeCents).Returns(30);
            var feeCalculator = new TicketFeeCalculator(clientConfig.Object);

            // act
            var factory = new EventBookingFactory(mockRepository.Object, mockDateService.Object, feeCalculator);

            var result = factory.Create(
                eventId,
                new EventPromoCode{ PromoCode = "promo123", DiscountPercent = 30}, 
                applicationUser,
                eventTicketReservations);

            Assert.That(result, Is.TypeOf<EventBooking>());
            Assert.That(result.EventBookingTickets, Is.Not.Null);
            Assert.That(result.EventBookingTickets.Count, Is.EqualTo(4));   // Two reservations * Two Qty
            Assert.That(result.TotalCost, Is.EqualTo(14.58));                  // Two reservations * Price of 10 + $2 txnfee
            Assert.That(result.TransactionFee, Is.EqualTo(0.58));
            Assert.That(result.Cost, Is.EqualTo(20));
            Assert.That(result.Status, Is.EqualTo(EventBookingStatus.PaymentPending));
            Assert.That(result.PromoCode, Is.EqualTo("promo123"));
            Assert.That(result.DiscountPercent, Is.EqualTo(30));
            Assert.That(result.DiscountAmount, Is.EqualTo(6));
        }

    }
}