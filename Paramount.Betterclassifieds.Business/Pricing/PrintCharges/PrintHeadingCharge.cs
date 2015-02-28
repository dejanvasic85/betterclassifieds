using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintHeadingCharge : IPrintCharge
    {
        public PrintAdChargeItem Calculate(RateModel rateModel, LineAdModel lineAdModel, int publications, int editions = 1)
        {
            Guard.NotNull(rateModel);

            var price = lineAdModel.AdHeader.HasValue()
                ? rateModel.BoldHeading.GetValueOrDefault()
                : 0;

            return new PrintAdChargeItem(price, "Print Heading", publications, editions);
        }

    }
}