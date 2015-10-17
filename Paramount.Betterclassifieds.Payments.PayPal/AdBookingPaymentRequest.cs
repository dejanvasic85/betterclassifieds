using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Payments.pp
{
    public class AdBookingPaymentRequest : IPaymentRequest
    {
        public string PayReference { get; set; }

        public string PayerId { get; set; }

        public BookingOrderResult BookingOrderResult { get; set; }

        public string ReturnUrl { get; set; }

        public string CancelUrl { get; set; }

        public decimal Total
        {
            get { return this.BookingOrderResult.Total; }
        }
    }

}