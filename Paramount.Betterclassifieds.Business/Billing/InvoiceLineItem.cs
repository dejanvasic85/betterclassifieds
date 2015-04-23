namespace Paramount.Betterclassifieds.Business
{
    public class InvoiceLineItem
    {
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal ItemTotal { get; set; }
    }
}