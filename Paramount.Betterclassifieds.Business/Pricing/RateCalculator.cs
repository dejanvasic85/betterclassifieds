using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    using Print;
    using Repository;

    public interface IRateCalculator
    {
        decimal Calculate(int ratecardId, LineAdModel lineAd, bool isOnlineAd, int editions = 1);

        BookingOrderResult Calculate(IAdRateContext adRateContext, int? editionOverride = null);
    }

    public class RateCalculator : IRateCalculator
    {
        private readonly IRateRepository _rateRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IOnlineChargeableItem[] _onlineChargeableItems;
        private readonly IPrintChargeableItem[] _printChargeableItems;
        private readonly IClientConfig _clientConfig;

        public RateCalculator(IRateRepository rateRepository, IPublicationRepository publicationRepository, IOnlineChargeableItem[] onlineChargeableItems, IPrintChargeableItem[] printChargeableItems, IClientConfig clientConfig)
        {
            _rateRepository = rateRepository;
            _publicationRepository = publicationRepository;
            _onlineChargeableItems = onlineChargeableItems;
            _printChargeableItems = printChargeableItems;
            _clientConfig = clientConfig;
        }

        public decimal Calculate(int ratecardId, LineAdModel lineAd, bool isOnlineAd, int editions = 1)
        {
            // Fetch the ratecard by the baseRate
            var rateModel = _rateRepository.GetRatecard(ratecardId);
            decimal price = 0;

            // Calculate line ad price
            if (lineAd != null)
            {
                int wordCount = lineAd.NumOfWords;
                if (wordCount > rateModel.FreeWords)
                {
                    price += (wordCount - rateModel.FreeWords) * rateModel.RatePerWord.GetValueOrDefault();
                }
                price += !string.IsNullOrEmpty(lineAd.AdHeader) ? rateModel.BoldHeading.GetValueOrDefault() : 0;
                price += lineAd.IsSuperBoldHeading ? rateModel.LineAdSuperBoldHeading.GetValueOrDefault() : 0;
                price += lineAd.UsePhoto ? rateModel.PhotoCharge.GetValueOrDefault() : 0;
                price += lineAd.IsColourBoldHeading ? rateModel.LineAdColourHeading.GetValueOrDefault() : 0;
                price += lineAd.IsColourBorder ? rateModel.LineAdColourBorder.GetValueOrDefault() : 0;
                price += lineAd.IsColourBackground ? rateModel.LineAdColourBackground.GetValueOrDefault() : 0;
            }

            // Add the cost for the online bundle
            if (isOnlineAd)
                price += rateModel.OnlineEditionBundle.GetValueOrDefault();

            // Ensure min / max values
            if (price < rateModel.MinCharge)
                price = rateModel.MinCharge.GetValueOrDefault();

            if (price > rateModel.MaxCharge)
                price = rateModel.MaxCharge.GetValueOrDefault();

            return price;
        }

        /// <summary>
        /// Constructs the booking rate result that contains calculated line items for each publication 
        /// </summary>
        public BookingOrderResult Calculate(IAdRateContext adRateContext, int? editionOverride = null)
        {
            // Fetch the user details so that we can construct the booking order result
            var bookingRate = new BookingOrderResult(_clientConfig, adRateContext);

            Guard.NotNullIn(adRateContext, adRateContext.CategoryId, adRateContext.SubCategoryId);

            // Online rates
            var onlineAdRate = _rateRepository.GetOnlineRateForCategories(adRateContext.SubCategoryId, adRateContext.CategoryId);
            if (onlineAdRate == null)
            {
                throw new SetupException("No available online rate has been setup.");
            }

            bookingRate.AddOnlineRate(_onlineChargeableItems
                .Select(c => c.Calculate(onlineAdRate, adRateContext.OnlineAdModel))
                .ToArray());

            if (!adRateContext.IsLineAdIncluded)
                return bookingRate;

            // Print Rates
            var printRates = _rateRepository.GetRatesForPublicationCategory(adRateContext.Publications, adRateContext.SubCategoryId);
            foreach (var printRate in printRates)
            {
                var publicationName = _publicationRepository.GetPublication(printRate.PublicationId).Title;

                bookingRate
                    .AddPublicationWithRates(publicationName, printRate.PublicationId, printRate.RatecardId, _printChargeableItems.Select(pr => pr.Calculate(printRate, adRateContext.LineAdModel, editionOverride ?? adRateContext.PrintInsertions.GetValueOrDefault()))
                    .ToArray());
            }

            return bookingRate;
        }
    }
}