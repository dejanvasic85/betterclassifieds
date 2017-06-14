using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventPromoCodeTests
    {
        [Test]
        public void ApplyDiscountFor_IsDisabled_ThrowsException()
        {
            var promo = new EventPromoCode
            {
                IsDisabled = true
            };

            Assert.Throws<OperationOnDisabledValueException>(() => promo.ApplyDiscountFor(100));
        }

        [Test]
        public void ApplyDiscountFor_DiscountIsNull_ReturnsNoDiscount()
        {
            var promo = new EventPromoCode
            {
                IsDisabled = false,
                DiscountPercent = null
            };

            var discount = promo.ApplyDiscountFor(100);

            discount.IsNotNull();
            discount.DiscountPercent.IsEqualTo(0);
            discount.DiscountValue.IsEqualTo(0);
            discount.OriginalAmount.IsEqualTo(100);
            discount.AmountAfterDiscount.IsEqualTo(100);
        }

        [Test]
        public void ApplyDiscountFor_HasDiscount()
        {
            var promo = new EventPromoCode
            {
                IsDisabled = false,
                DiscountPercent = 10
            };

            var discount = promo.ApplyDiscountFor(200);

            discount.IsNotNull();
            discount.DiscountPercent.IsEqualTo(10);
            discount.DiscountValue.IsEqualTo(20);
            discount.OriginalAmount.IsEqualTo(200);
            discount.AmountAfterDiscount.IsEqualTo(180);
        }

        [Test]
        public void ApplyDiscountFor_ToString_ReturnsPromoCode()
        {
            var promo = new EventPromoCode
            {
                IsDisabled = false,
                DiscountPercent = 10,
                PromoCode = "promo"
            };

            promo.ToString().IsEqualTo("promo");
        }
    }
}
