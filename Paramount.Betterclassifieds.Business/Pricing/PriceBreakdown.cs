using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    public class PriceBreakdown
    {
        private readonly Dictionary<string, decimal> _items;

        public decimal Total
        {
            get { return _items.Sum(lineItem => lineItem.Value); }
        }

        public IReadOnlyDictionary<string, decimal> LineItems { get { return _items; } }

        public PriceBreakdown()
        {
            _items = new Dictionary<string, decimal>();
        }

        public void AddItem(AdCharge adCharge)
        {
            if (_items.ContainsKey(adCharge.Item))
            {
                _items[adCharge.Item] += adCharge.Price;
            }
            else
            {
                _items.Add(adCharge.Item, adCharge.Price);
            }
        }

        public void AddRange(AdCharge[] charges)
        {
            foreach (var adCharge in charges)
            {
                this.AddItem(adCharge);
            }
        }
    }
}