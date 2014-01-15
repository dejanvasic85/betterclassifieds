namespace Paramount.Betterclassifieds.Tests.Functional
{
    public interface IPageFactory
    {
        T Resolve<T>() where T : BasePage;
    }
}