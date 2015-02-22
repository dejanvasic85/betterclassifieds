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
    }
}