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

        public void AddItem(string description, decimal minimumCharge)
        {
            _items.Add(description, minimumCharge);
        }
    }
}