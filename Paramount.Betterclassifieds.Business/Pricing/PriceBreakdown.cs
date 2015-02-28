using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    public class PriceBreakdown 
    {
        private List<ILineItem> Items { get; set; }

        public PriceBreakdown()
        {
            Items = new List<ILineItem>();
        }

        public void AddItem(ILineItem adLine)
        {
            this.Items.Add(adLine);
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