using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintHeadingCharge : IPrintChargeableItem
    {
        public PrintAdChargeItem Calculate(RateModel rateModel, LineAdModel lineAdModel, int editions = 1)
        {
            Guard.NotNull(rateModel);

            var price = lineAdModel.AdHeader.HasValue()
                ? rateModel.BoldHeading.GetValueOrDefault()
                : 0;

            var quantity = lineAdModel.AdHeader.HasValue() ? 1 : 0;

            return new PrintAdChargeItem(price, "Print Heading", editions, quantity);
        }
    }
}