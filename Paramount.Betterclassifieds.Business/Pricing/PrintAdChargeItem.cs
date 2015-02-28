namespace Paramount.Betterclassifieds.Business
{
    public class PrintAdChargeItem : ILineItem
    {
        public PrintAdChargeItem(decimal price, string name, int publications, int editions, int quantity = 1)
        {
            this.Price = price;
            this.Name = name;
            this.Quantity = quantity;
            this.Publications = publications;
            this.Editions = editions;
        }

        public int Publications { get; set; }
        public int Editions { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Currency { get { return "AUD"; } }
        public decimal Total { get { return Price * Quantity * Editions * Publications; } }
    }
}