﻿using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class TicketFeeCalculatorTests
    {
        [Test]
        [TestCase(10, 3.9, 30, .69, 10.69)] // $10 ticket
        [TestCase(0, 3.9, 30, 0, 0)]  // Free
        public void GetTotalTicketPrice(decimal mockTicketPrice, decimal mockFeePercentage,
            decimal mockFeeCents, decimal expectedFee, decimal expectedPriceIncFee)
        {
            var eventTicketMock = new EventTicketMockBuilder().WithPrice(mockTicketPrice).Build();
            var clientConfig = new Mock<IClientConfig>();
            clientConfig.Setup(prop => prop.EventTicketFeePercentage).Returns(mockFeePercentage);
            clientConfig.Setup(prop => prop.EventTicketFeeCents).Returns(mockFeeCents);

            var calculator = new TicketFeeCalculator(clientConfig.Object);
            var result = calculator.GetTotalTicketPrice(eventTicketMock);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Fee, Is.EqualTo(expectedFee));
            Assert.That(result.PriceIncludingFee, Is.EqualTo(expectedPriceIncFee));
            Assert.That(result.OriginalPrice, Is.EqualTo(mockTicketPrice));
        }
        
        [Test]
        public void GetOrganiserOwedAmount_ReturnsAmount()
        {
            var eventTicketMock = new EventTicketMockBuilder().WithPrice(10).Build();
            var clientConfig = new Mock<IClientConfig>();
            clientConfig.Setup(prop => prop.EventTicketFeePercentage).Returns((decimal)3.9);
            clientConfig.Setup(prop => prop.EventTicketFeeCents).Returns(30);


            var calculator = new TicketFeeCalculator(clientConfig.Object);
            var result = calculator.GetFeeTotalForOrganiserForAllTicketSales(11000, 200); // 55 per ticket

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo((decimal)489));
        }
    }
}
