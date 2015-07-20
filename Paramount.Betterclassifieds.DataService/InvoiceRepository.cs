namespace Paramount.Betterclassifieds.DataService
{
    using AutoMapper;
    using System.Linq;
    using Classifieds;

    public class InvoiceRepository : Business.IInvoiceRepository, IMappingBehaviour
    {
        private readonly IDbContextFactory _dbContextFactory;

        public InvoiceRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Business.Invoice GetInvoiceDataForBooking(int bookingId)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var summaryData = context.AdBookingOrderSummaries.FirstOrDefault(bk => bk.AdBookingId == bookingId);
                if (summaryData == null)
                    return null;

                var invoice = this.Map<AdBookingOrderSummary, Business.Invoice>(summaryData);

                var groupData = context.AdBookingOrders.Where(o => o.AdBookingId == bookingId).ToList();

                // Fetch all the invoice line items (grouped)
                foreach (var adBookingOrder in groupData)
                {
                    var group = this.Map<AdBookingOrder, Business.InvoiceGroup>(adBookingOrder);

                    var itemData = context.AdBookingOrderItems.Where(i => i.AdBookingOrderId == adBookingOrder.AdBookingOrderId).ToList();

                    group.InvoiceLineItems.AddRange(this.MapList<AdBookingOrderItem, Business.InvoiceLineItem>(itemData));

                    invoice.InvoiceGroups.Add(group);
                }
                return invoice;
            }
        }



        public void OnRegisterMaps(IConfiguration configuration)
        {
            // From data
            configuration.CreateMap<AdBookingOrder, Business.InvoiceGroup>();
            configuration.CreateMap<AdBookingOrderItem, Business.InvoiceLineItem>();
            configuration.CreateMap<AdBookingOrderSummary, Business.Invoice>();
        }
    }
}