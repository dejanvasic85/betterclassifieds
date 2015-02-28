using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintPhotoCharge : IPrintCharge
    {
        public PrintAdChargeItem Calculate(RateModel rateModel, LineAdModel lineAdModel, int editions = 1)
        {
            Guard.NotNull(rateModel);

            var price = lineAdModel.AdImageId.HasValue()
                ? rateModel.PhotoCharge.GetValueOrDefault()
                : 0;

            return new PrintAdChargeItem(price, "Print Photo", editions);
        }
    }
}