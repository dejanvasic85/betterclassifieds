using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    using Booking;
    using Print;
    using Repository;

    public interface IRateCalculator
    {
        decimal Calculate(int ratecardId, LineAdModel lineAd, bool isOnlineAd, int editions = 1);

        BookingRateResult Calculate(BookingCart bookingCart, int? editionOverride = null);
    }

    public class RateCalculator : IRateCalculator
    {
        private readonly IRateRepository _rateRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IOnlineChargeableItem[] _onlineChargeableItems;
        private readonly IPrintChargeableItem[] _printChargeableItems;

        public RateCalculator(IRateRepository rateRepository, IPublicationRepository publicationRepository, IOnlineChargeableItem[] onlineChargeableItems, IPrintChargeableItem[] printChargeableItems)
        {
            _rateRepository = rateRepository;
            _publicationRepository = publicationRepository;
            _onlineChargeableItems = onlineChargeableItems;
            _printChargeableItems = printChargeableItems;
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
        public BookingRateResult Calculate(BookingCart bookingCart, int? editionOverride = null)
        {
            var bookingRate = new BookingRateResult(bookingCart.Reference);

            Guard.NotNullIn(bookingCart, bookingCart.CategoryId, bookingCart.SubCategoryId);

            // Online rates
            var onlineAdRate = _rateRepository.GetOnlineRateForCategories(bookingCart.SubCategoryId, bookingCart.CategoryId);
            if (onlineAdRate == null)
            {
                throw new SetupException("No available online rate has been setup.");
            }

            bookingRate.AddOnlineRate(_onlineChargeableItems
                .Select(c => c.Calculate(onlineAdRate, bookingCart.OnlineAdModel))
                .ToArray());

            if (!bookingCart.IsLineAdIncluded)
                return bookingRate;

            // Print Rates
            var printRates = _rateRepository.GetRatesForPublicationCategory(bookingCart.Publications, bookingCart.SubCategoryId);
            foreach (var printRate in printRates)
            {
                var publicationName = _publicationRepository.GetPublication(printRate.PublicationId).Title;

                bookingRate.AddPublicationWithRates(publicationName, _printChargeableItems
                    .Select(pr => pr.Calculate(printRate, bookingCart.LineAdModel, editionOverride ?? bookingCart.PrintInsertions.GetValueOrDefault()))
                    .ToArray());
            }

            return bookingRate;
        }
    }
}