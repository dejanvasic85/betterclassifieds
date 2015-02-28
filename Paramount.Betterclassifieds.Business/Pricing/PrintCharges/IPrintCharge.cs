using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business
{
    public interface IPrintCharge
    {
        PrintAdChargeItem Calculate(RateModel rateModel, LineAdModel booking, int editions = 1);
    }
}