using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    using Repository;
    using Print;
    using Booking;

    public interface IRateCalculator
    {
        decimal Calculate(int ratecardId, LineAdModel lineAd, bool isOnlineAd, int editions = 1);

        PriceBreakdown GetPriceBreakDown(BookingCart bookingCart);
    }

    public class RateCalculator : IRateCalculator
    {
        private readonly IRateRepository _rateRepository;
        private readonly IOnlineCharge[] _onlineCharges;
        private readonly IPrintCharge[] _printCharges;

        public RateCalculator(IRateRepository rateRepository, IOnlineCharge[] onlineCharges, IPrintCharge[] printCharges)
        {
            _rateRepository = rateRepository;
            _onlineCharges = onlineCharges;
            _printCharges = printCharges;
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

        public PriceBreakdown GetPriceBreakDown(BookingCart bookingCart)
        {
            Guard.NotNullIn(bookingCart, bookingCart.CategoryId, bookingCart.SubCategoryId);

            var onlineAdRate = _rateRepository.GetOnlineRateForCategories(bookingCart.SubCategoryId, bookingCart.CategoryId);
            if (onlineAdRate == null)
            {
                throw new SetupException("No available online rate has been setup.");
            }

            var breakDown = new PriceBreakdown();
            breakDown.AddRange(_onlineCharges.Select(c => c.Calculate(onlineAdRate, bookingCart)).ToArray());

            if (!bookingCart.IsLineAdIncluded)
                return breakDown;

            var printRates = _rateRepository.GetRatesForPublicationCategory(bookingCart.Publications, bookingCart.SubCategoryId);
            foreach (var printRate in printRates)
            {
                breakDown.AddRange(_printCharges.Select(pr => pr.Calculate(printRate, bookingCart)).ToArray());
            }

            return breakDown;
        }
    }
}