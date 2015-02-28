using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Pricing
{
    [TestFixture]
    public class OnlineAdBaseChargeTests
    {
        [Test]
        public void Calculate_RateIsNull_ThrowsArgumentException()
        {
            var charge = new OnlineBasePriceCharge();

            Assert.Throws<ArgumentNullException>(() => charge.Calculate(null, null));
        }

        [Test]
        public void Calculate_OnlineAd_IsNull_Returns_Zero()
        {
            // arrange
            var charge = new OnlineBasePriceCharge();
            var rate = RateMocks.Create().WithBaseRate(10);
            var onlineAd = new OnlineAdModel();

            // act
            var result = charge.Calculate(rate, onlineAd);

            // assert
            Assert.That(result.Price, Is.EqualTo(10));
            Assert.That(result.Item, Is.EqualTo("Online Ad"));
        }
    }
}