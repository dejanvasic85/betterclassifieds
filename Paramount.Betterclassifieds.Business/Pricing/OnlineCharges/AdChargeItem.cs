namespace Paramount.Betterclassifieds.Business
{
    public class AdChargeItem : ILineItem
    {
        public AdChargeItem(decimal price, string name, int quantity = 1)
        {
            Quantity = quantity;
            Price = price;
            Name = name;
        }

        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public string Currency
        {
            get { return "AUD"; }
        }

        public decimal Total
        {
            get { return Price * Quantity; }
        }

        public static implicit operator decimal(AdChargeItem adChargeItem)
        {
            return adChargeItem.Price;
        }

        public static decimal operator +(AdChargeItem charge1, AdChargeItem charge2)
        {
            return charge1.Price + charge2.Price;
        }
    }
}