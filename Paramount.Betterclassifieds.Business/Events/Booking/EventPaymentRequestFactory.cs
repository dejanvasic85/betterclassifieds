using System;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventPaymentRequestFactory
    {
        public EventPaymentRequest Create(int eventId, PaymentType paymentType, decimal requestedAmount, string requestedByUser, DateTime createdDate, DateTime createdDateUtc)
        {
            return new EventPaymentRequest
            {
                EventId = eventId,
                CreatedByUser = requestedByUser,
                PaymentMethod = paymentType,
                RequestedAmount = requestedAmount,
                CreatedDate = createdDate,
                CreatedDateUtc = createdDateUtc
            };
        }
    }
}