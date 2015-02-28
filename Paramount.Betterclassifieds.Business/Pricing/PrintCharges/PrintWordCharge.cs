using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    public class PrintWordCharge : IPrintCharge
    {
        public PrintAdChargeItem Calculate(RateModel rateModel, LineAdModel lineAdModel, int editions = 1)
        {
            Guard.NotNull(rateModel);

            var price = rateModel.RatePerWord.GetValueOrDefault();
            
            // Reduce the quantity based on the number of free words
            var quantity = lineAdModel.NumOfWords;

            return new PrintAdChargeItem(
                price, 
                "Print Words", 
                editions, 
                quantity);
        }
    }
}