namespace Paramount.Betterclassifieds.Business
{
    public class InvoiceLineItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ItemTotal { get; set; }
        public int? Editions { get; set; }
    }
}