using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventPaymentRequestFactoryTests
    {
        [Test]
        public void Create_ReturnsNew_EventPaymentRequest()
        {
            const int eventId = 10;
            const string username = "fooBarr";
            const decimal amount = 120;
            const PaymentType paymentType = PaymentType.PayPal;

            // act
            var factory = new EventPaymentRequestFactory();
            var result = factory.Create(eventId, paymentType, amount, username, DateTime.Now, DateTime.UtcNow);

            Assert.That(result, Is.TypeOf<EventPaymentRequest>());
            Assert.That(result.EventPaymentRequestId, Is.EqualTo(0));
            Assert.That(result.EventId, Is.EqualTo(10));
            Assert.That(result.RequestedAmount, Is.EqualTo(amount));
            Assert.That(result.PaymentMethod, Is.EqualTo(paymentType));

            // Ensure the processed properties are empty/null
            Assert.That(result.IsPaymentProcessed, Is.Null);
            Assert.That(result.PaymentProcessedBy, Is.Null);
            Assert.That(result.PaymentProcessedDate, Is.Null);
            Assert.That(result.PaymentProcessedDateUtc, Is.Null);
        }
    }
}
