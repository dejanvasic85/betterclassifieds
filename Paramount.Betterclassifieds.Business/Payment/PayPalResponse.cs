namespace Paramount.Betterclassifieds.Business.Payment
{
    public class PayPalResponse
    {
        public PaymentStatus Status { get; set; }
        public string ApprovalUrl { get; set; }
        public string PaymentId { get; set; }
    }
}