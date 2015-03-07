namespace Paramount.Betterclassifieds.Business
{
    public class OnlineChargeItem : ILineItem
    {
        public OnlineChargeItem(decimal price, string name, int quantity = 1)
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

        public decimal ItemTotal
        {
            get { return Price * Quantity; }
        }

        public static implicit operator decimal(OnlineChargeItem onlineChargeItem)
        {
            return onlineChargeItem.Price;
        }

        public static decimal operator +(OnlineChargeItem charge1, OnlineChargeItem charge2)
        {
            return charge1.Price + charge2.Price;
        }
    }
}