using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    public interface IPrintChargeableItem
    {
        PrintAdChargeItem Calculate(RateModel rateModel, LineAdModel booking, int editions = 1);
    }
}