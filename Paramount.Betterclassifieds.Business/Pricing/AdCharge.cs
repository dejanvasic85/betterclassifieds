namespace Paramount.Betterclassifieds.Business
{
    public class AdCharge
    {
        public AdCharge(decimal price, string item)
        {
            Price = price;
            Item = item;
        }

        public decimal Price { get; set; }
        public string Item { get; set; }

        public static implicit operator decimal(AdCharge adCharge)
        {
            return adCharge.Price;
        }

        public static decimal operator +(AdCharge charge1, AdCharge charge2)
        {
            return charge1.Price + charge2.Price;
        }
    }
}