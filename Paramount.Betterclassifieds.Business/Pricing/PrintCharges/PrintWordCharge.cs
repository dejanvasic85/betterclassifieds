using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintWordCharge : IPrintCharge
    {
        public PrintAdChargeItem Calculate(RateModel rateModel, LineAdModel lineAdModel, int publications, int editions = 1)
        {
            Guard.NotNull(rateModel);

            var price = rateModel.RatePerWord.GetValueOrDefault();

            return new PrintAdChargeItem(price, "Print Words", publications, editions, lineAdModel.NumOfWords);
        }
    }
}