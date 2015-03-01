namespace Paramount.Betterclassifieds.Business
{
    using Booking;
    using Print;
    using Repository;
    using System.Linq;
    using System.Collections.Generic;

    public interface IRateCalculator
    {
        decimal Calculate(int ratecardId, LineAdModel lineAd, bool isOnlineAd, int editions = 1);

        List<BookingProduct> Calculate(BookingCart bookingCart);
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
        /// Returns all the calculated line items for the booking cart grouped by publication / online
        /// </summary>
        public List<BookingProduct> Calculate(BookingCart bookingCart)
        {
            var list = new List<BookingProduct>();

            Guard.NotNullIn(bookingCart, bookingCart.CategoryId, bookingCart.SubCategoryId);

            var onlineAdRate = _rateRepository.GetOnlineRateForCategories(bookingCart.SubCategoryId, bookingCart.CategoryId);
            if (onlineAdRate == null)
            {
                throw new SetupException("No available online rate has been setup.");
            }

            var onlinePrices = new BookingProduct("Online", bookingCart.Reference);
            onlinePrices.AddRange(_onlineChargeableItems.Select(c => c.Calculate(onlineAdRate, bookingCart.OnlineAdModel)).ToArray());
            list.Add(onlinePrices);

            if (!bookingCart.IsLineAdIncluded)
                return list;

            var printRates = _rateRepository.GetRatesForPublicationCategory(bookingCart.Publications, bookingCart.SubCategoryId);
            foreach (var printRate in printRates)
            {
                var publicationName = _publicationRepository.GetPublication(printRate.PublicationId).Title;

                var printBreakDown = new BookingProduct(publicationName, bookingCart.Reference);
                printBreakDown.AddRange(_printChargeableItems
                    .Select(pr => pr.Calculate(printRate, bookingCart.LineAdModel, bookingCart.Editions.Length))
                    .ToArray());

                list.Add(printBreakDown);
            }

            return list;
        }
    }
}