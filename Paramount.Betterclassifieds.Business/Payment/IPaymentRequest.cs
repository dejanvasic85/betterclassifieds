namespace Paramount.Betterclassifieds.Business.Payment
{
    public interface IPaymentRequest
    {
        string PayReference { get; set; }
        string PayerId { get; set; }
        string ReturnUrl { get; set; }
        string CancelUrl { get; set; }
        decimal Total { get; }
    }

    public interface IPaymentRequest<TChargeable> : IPaymentRequest 
        where TChargeable : class 
    {
        
    }

    public class ChargeableItem
    {
        public string Name { get; private set; }
        public string Price { get; private set; }
        public string Currency { get; private set; }
        public string Quantity { get; private set; }
        public string Sku { get; private set; }
    }
}