using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintSuperBoldHeadingCharge : IPrintCharge
    {
        public PrintAdChargeItem Calculate(RateModel rateModel, LineAdModel lineAdModel, int editions = 1)
        {
            Guard.NotNull(rateModel);

            var price = lineAdModel.IsSuperBoldHeading
                ? rateModel.LineAdSuperBoldHeading.GetValueOrDefault()
                : 0;

            return new PrintAdChargeItem(price, "Print Super Bold Heading", editions);
        }
    }
}