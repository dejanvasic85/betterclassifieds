using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;

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
            var createdDate = DateTime.Now;
            var createdDateUtc = DateTime.UtcNow;
            var applicationUser = new ApplicationUserMockBuilder().Default().Build();
            var mockRepository = new Mock<IEventRepository>();

            // act
            var factory = new EventBookingFactory(mockRepository.Object);

            var result = factory.Create(
                eventId,
                applicationUser,
                eventTicketReservations,
                createdDate,
                createdDateUtc);

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
            var createdDate = DateTime.Now;
            var createdDateUtc = DateTime.UtcNow;
            var applicationUser = new ApplicationUserMockBuilder().Default().Build();

            // act
            var factory = new EventBookingFactory(mockRepository.Object);

            var result = factory.Create(
                eventId,
                applicationUser,
                eventTicketReservations,
                createdDate,
                createdDateUtc);

            Assert.That(result, Is.TypeOf<EventBooking>());
            Assert.That(result.EventBookingTickets, Is.Not.Null);
            Assert.That(result.EventBookingTickets.Count, Is.EqualTo(4));   // Two reservations * Two Qty
            Assert.That(result.TotalCost, Is.EqualTo(24));                  // Two reservations * Price of 10 + $2 txnfee
            Assert.That(result.TransactionFee, Is.EqualTo(4));
            Assert.That(result.Cost, Is.EqualTo(20));
            Assert.That(result.Status, Is.EqualTo(EventBookingStatus.PaymentPending));
            
        }
    }
}