using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    public class BookingProduct
    {
        public BookingProduct(string name, string referenceNumber)
        {
            Name = name;
            Reference = referenceNumber;
            Items = new List<ILineItem>();
        }

        public string Name { get; private set; }

        public string Reference { get; private set; }

        private List<ILineItem> Items { get; set; }

        public BookingProduct()
        {
            Items = new List<ILineItem>();
        }

        public void AddItem(ILineItem lineItem)
        {
            if (lineItem.Quantity > 0)
                this.Items.Add(lineItem);
        }

        public void AddRange<T>(T[] charges) where T : ILineItem
        {
            foreach (var adCharge in charges)
            {
                this.AddItem(adCharge);
            }
        }

        public ILineItem[] GetItems()
        {
            return this.Items.ToArray();
        }

        public decimal BookingTotal()
        {
            return Items.Sum(c => c.Total);
        }


    }
}