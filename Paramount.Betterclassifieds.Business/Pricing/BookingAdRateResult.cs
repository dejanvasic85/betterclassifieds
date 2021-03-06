﻿using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    /// <summary>
    /// Represents the ad (online or print) with collection of line items (heading, photos etc).
    /// </summary>
    public class BookingAdRateResult
    {
        public BookingAdRateResult()
        {
            Items = new List<ILineItem>();
        }

        public BookingAdRateResult(string name, string referenceNumber, int? rateId, int? publicationId = null)
        {
            Name = name;
            Reference = referenceNumber;
            Items = new List<ILineItem>();
            PublicationId = publicationId;
            RateId = rateId;
        }

        public int? RateId { get; private set; }

        public string Name { get; private set; }

        public int? PublicationId { get; private set; }

        public string Reference { get; private set; }

        private List<ILineItem> Items { get; set; }
        
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

        public decimal OrderTotal
        {
            get { return Items.Count == 0 ? 0 : Items.Sum(i => i.ItemTotal); }
        }
    }
}