using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class TicketFeeCalculatorTests
    {
        [Test]
        public void GetTotalTicketPrice()
        {
            var eventTicketMock = new EventTicketMockBuilder().WithPrice(10).Build();
            var clientConfig = new Mock<IClientConfig>();
            clientConfig.Setup(prop => prop.EventTicketFeePercentage).Returns((decimal)3.9);
            clientConfig.Setup(prop => prop.EventTicketFeeCents).Returns(30);

            var calculator = new TicketFeeCalculator(clientConfig.Object);
            var result = calculator.GetTotalTicketPrice(eventTicketMock);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Fee, Is.EqualTo((decimal).69));
            Assert.That(result.PriceIncludingFee, Is.EqualTo((decimal)10.69));
            Assert.That(result.OriginalPrice, Is.EqualTo(10));
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
