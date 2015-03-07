using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class PriceSummaryViewConverter : ITypeConverter<BookingRateResult, PriceSummaryView>
    {
        public PriceSummaryView Convert(ResolutionContext context)
        {
            var source = (BookingRateResult)context.SourceValue;
            var summary = new PriceSummaryView
            {
                BookingTotal = source.Total,
                OnlinePrice = new OnlinePriceSummary
                {
                    Name = source.OnlineBookingAdRate.Name,
                    OnlineTotal = source.OnlineBookingAdRate.Total,
                    Items = source.OnlineBookingAdRate.GetItems().Select(r => new OnlineSummaryItemView
                    {
                        ItemTotal = r.ItemTotal,
                        Quantity = r.Quantity,
                        Name = r.Name,
                        Price = r.Price
                    }).ToArray()
                },
            };

            // Get out if there's no print stuff
            if (source.PrintRates.Count == 0)
                return summary;

            summary.PublicationPrices = source
                .PrintRates
                .Select(publicationRate => new PublicationPriceSummary
                {
                    Publication = publicationRate.Name,
                    PublicationTotal = publicationRate.Total,
                    Items = publicationRate.GetItems().OfType<PrintAdChargeItem>().Select(p => new PrintSummaryItemView
                        {
                            Editions = p.Editions,
                            Name = p.Name,
                            Price = p.Price,
                            Quantity = p.Quantity,
                            ItemTotal = p.ItemTotal
                        }).ToArray()
                })
                .ToArray();


            return summary;
        }
    }
}