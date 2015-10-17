namespace Paramount.Betterclassifieds.Business.Payment
{
    /// <summary>
    /// Copy of the paypal Item object properties
    /// </summary>
    public class PayPalChargeableItem
    {
        public PayPalChargeableItem(string name, decimal price, string currency, int quantity, string sku)
        {
            Name = name;
            Price = price.ToString("N");
            Currency = currency;
            Quantity = quantity.ToString();
            Sku = sku;
        }

        public string Name { get; private set; }
        public string Price { get; private set; }
        public string Currency { get; private set; }
        public string Quantity { get; private set; }
        public string Sku { get; private set; }
    }
}