using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.Business.Models
{
    public interface IRateCalculator
    {
        decimal Calculate(int ratecardId, LineAdModel lineAd, bool isOnlineAd, int editions = 1);
    }

    public class RateCalculator : IRateCalculator
    {
        private readonly IRateRepository _rateRepository;

        public RateCalculator(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public decimal Calculate(int ratecardId, LineAdModel lineAd, bool isOnlineAd, int editions = 1)
        {
            // Fetch the ratecard by the baseRate
            RateModel rateModel = _rateRepository.GetRatecard(ratecardId);
            decimal price = 0;
            
            // Calculate line ad price
            if (lineAd != null)
            {
                int wordCount = lineAd.GetWordCount();
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

        public PriceBreakdown Calculate(int subCategoryId)
    }
}