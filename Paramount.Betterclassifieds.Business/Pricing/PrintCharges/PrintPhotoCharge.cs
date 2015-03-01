using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintPhotoCharge : IPrintChargeableItem
    {
        public PrintAdChargeItem Calculate(RateModel rateModel, LineAdModel lineAdModel, int editions = 1)
        {
            Guard.NotNull(rateModel);

            var price = lineAdModel.AdImageId.HasValue() ? rateModel.PhotoCharge.GetValueOrDefault() : 0;
            var quantity = lineAdModel.AdImageId.HasValue() ? 1 : 0;

            return new PrintAdChargeItem(price, "Print Photo", editions, quantity);
        }
    }
}