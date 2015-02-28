using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Pricing
{
    [TestFixture]
    public class PrintWordChargeTests
    {
        [Test]
        public void Calculate_RateIsNull_ThrowsArgumentException()
        {
            var charge = new PrintWordCharge();

            Assert.Throws<ArgumentNullException>(() => charge.Calculate(null, null, 0));
        }

        [Test]
        public void Calculate_NoWords_Returns_Zero()
        {
            // arrange
            var lineAd = new LineAdModel {AdText = ""};
            var rate = PrintRateMocks.Create();

            // act
            var result = new PrintWordCharge().Calculate(rate, lineAd, 0);

            // assert
            Assert.That(result, Is.TypeOf<PrintAdChargeItem>());
            Assert.That(result.Name, Is.EqualTo("Print Words"));
            Assert.That(result.Quantity, Is.EqualTo(0));
        }

        [Test]
        public void Calculate_FiveWords_FiveDollarsPerWord_Returns_25_Total()
        {
            // arrange
            var lineAd = new LineAdModel { AdText = "one two three four five" };
            var rate = PrintRateMocks.Create().WithWordRate(5);

            // act
            var result = new PrintWordCharge().Calculate(rate, lineAd, editions: 1);

            // assert
            Assert.That(result, Is.TypeOf<PrintAdChargeItem>());
            Assert.That(result.Name, Is.EqualTo("Print Words"));
            Assert.That(result.Quantity, Is.EqualTo(5));
            Assert.That(result.Total, Is.EqualTo(25));
        }

        [Test]
        public void Calculate_FiveWords_FiveDollarsPerWord_MultiPublications()
        {
            // arrange
            var lineAd = new LineAdModel { AdText = "one two three four five" };
            var rate = PrintRateMocks.Create().WithWordRate(5);

            // act
            var result = new PrintWordCharge().Calculate(rate, lineAd, editions: 1);

            // assert
            Assert.That(result, Is.TypeOf<PrintAdChargeItem>());
            Assert.That(result.Name, Is.EqualTo("Print Words"));
            Assert.That(result.Quantity, Is.EqualTo(5));
            Assert.That(result.Total, Is.EqualTo(25));
        }
    }
}