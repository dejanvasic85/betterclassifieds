using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Pricing
{
    [TestFixture]
    public class PrintHeadingChargeTests
    {
        [Test]
        public void Calculate_RateIsNull_ThrowsArgumentException()
        {
            var charge = new PrintHeadingCharge();

            Assert.Throws<ArgumentNullException>(() => charge.Calculate(null, null, 0));
        }

        [Test]
        public void Calculate_HeadingIsNotProvided_Returns_ZeroTotal()
        {
            var rate = new RateModelMockBuilder().WithBoldHeading(10).Build();
            var lineAd = new LineAdModel { AdHeader = string.Empty };

            // act
            var result = new PrintHeadingCharge().Calculate(rate, lineAd);

            // assert
            Assert.That(result, Is.TypeOf<PrintAdChargeItem>());
            Assert.That(result.Editions, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Print Heading"));
            Assert.That(result.Price, Is.EqualTo(0));
            Assert.That(result.ItemTotal, Is.EqualTo(0));
        }

        [Test]
        public void Calculate_HeadingProvided_NoPublications_Returns_ZeroTotal()
        {
            var rate = new RateModelMockBuilder().WithBoldHeading(10).Build();
            var lineAd = new LineAdModel { AdHeader = "hey Ya!" };

            // act
            var result = new PrintHeadingCharge().Calculate(rate, lineAd);

            // assert
            Assert.That(result, Is.TypeOf<PrintAdChargeItem>());
            Assert.That(result.Editions, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Print Heading"));
            Assert.That(result.Price, Is.EqualTo(10));
            Assert.That(result.ItemTotal, Is.EqualTo(10));
        }

        [Test]
        public void Calculate_HeadingProvided_TwoPublications_TwoEditions_Returns_Total()
        {
            var rate = new RateModelMockBuilder().WithBoldHeading(10).Build();
            var lineAd = new LineAdModel { AdHeader = "hey Ya!" };

            // act
            var result = new PrintHeadingCharge().Calculate(rate, lineAd, editions: 2);

            // assert
            Assert.That(result, Is.TypeOf<PrintAdChargeItem>());
            Assert.That(result.Editions, Is.EqualTo(2));
            Assert.That(result.Name, Is.EqualTo("Print Heading"));
            Assert.That(result.Price, Is.EqualTo(10));
            Assert.That(result.Quantity, Is.EqualTo(1));
            Assert.That(result.ItemTotal, Is.EqualTo(20));
        }
    }
}