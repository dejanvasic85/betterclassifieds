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
            var result = calculator.GetTotalTicketPrice(eventTicketMock, true);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Fee, Is.EqualTo(expectedFee));
            Assert.That(result.Total, Is.EqualTo(expectedPriceIncFee));
            Assert.That(result.OriginalPrice, Is.EqualTo(mockTicketPrice));
        }

        [Test]
        public void GetTotalTicketPrice_WithDiscount()
        {
            var clientConfig = new Mock<IClientConfig>();
            clientConfig.Setup(prop => prop.EventTicketFeePercentage).Returns(0);
            clientConfig.Setup(prop => prop.EventTicketFeeCents).Returns(50);

            var eventPromo = new EventPromoCode
            {
                DiscountPercent = 20
            };
            var price = 100;

            var calculator = new TicketFeeCalculator(clientConfig.Object);
            var result = calculator.GetTotalTicketPrice(100, eventPromo, true);

            result.OriginalPrice.IsEqualTo(100);
            result.Total.IsEqualTo(80.5M);
            result.DiscountPercent.IsEqualTo(20);
            result.DiscountAmount.IsEqualTo(20);
            result.PriceAfterDiscount.IsEqualTo(80);
        }

        [Test]
        public void GetTotalTicketPrice_WithFullPriceDiscount_ExpectsNoFee()
        {
            var clientConfig = new Mock<IClientConfig>();
            clientConfig.Setup(prop => prop.EventTicketFeePercentage).Returns(5);
            clientConfig.Setup(prop => prop.EventTicketFeeCents).Returns(30);

            var eventPromo = new EventPromoCode
            {
                DiscountPercent = 100
            };
            var price = 100;

            var calculator = new TicketFeeCalculator(clientConfig.Object);
            var result = calculator.GetTotalTicketPrice(100, eventPromo, true);

            result.OriginalPrice.IsEqualTo(100);
            result.Total.IsEqualTo(0);
            result.DiscountPercent.IsEqualTo(100);
            result.DiscountAmount.IsEqualTo(100);
            result.PriceAfterDiscount.IsEqualTo(0);
            result.Fee.IsEqualTo(0);
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
