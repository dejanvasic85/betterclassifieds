namespace Paramount.Betterclassifieds.Business
{
    public interface ILineItem
    {
        decimal Price { get; set; }
        string Name { get; set; }
        int Quantity { get; set; }
        string Currency { get; }
        decimal ItemTotal { get; }

    }
}