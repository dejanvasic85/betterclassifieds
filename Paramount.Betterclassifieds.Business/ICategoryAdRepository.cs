namespace Paramount.Betterclassifieds.Business
{
    public interface ICategoryAdRepository<in TModel> where TModel : ICategoryAd
    {
        void Add(TModel model);
    }
}