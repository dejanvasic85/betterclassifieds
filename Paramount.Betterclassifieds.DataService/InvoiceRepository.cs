namespace Paramount.Betterclassifieds.DataService
{
    using AutoMapper;
    using System.Linq;
    using Classifieds;
    using System.Collections.Generic;

    public class InvoiceRepository : Business.IInvoiceRepository, IMappingBehaviour
    {

        public List<Business.InvoiceGroup> GetInvoiceData(int bookingId)
        {
            var result = new List<Business.InvoiceGroup>();
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var groupData = context.AdBookingOrders.Where(o => o.AdBookingId == bookingId).ToList();

                foreach (var adBookingOrder in groupData)
                {
                    var group = this.Map<AdBookingOrder, Business.InvoiceGroup>(adBookingOrder);

                    var itemData = context.AdBookingOrderItems.Where(i => i.AdBookingOrderId == adBookingOrder.AdBookingOrderId).ToList();

                    group.InvoiceLineItems.AddRange(this.MapList<AdBookingOrderItem, Business.InvoiceLineItem>(itemData));

                    result.Add(group);
                }
            }
            return result;
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<AdBookingOrder, Business.InvoiceGroup>();
            configuration.CreateMap<AdBookingOrderItem, Business.InvoiceLineItem>();
        }
    }
}