namespace Paramount.Betterclassifieds.Mvc.Modules
{
    public interface IModuleInfo
    {
        string PhysicalPath { get; }

        string Name { get; }

        string VirtualPath { get;}
        string Namespace { get; }
        bool RegisterView { get; }
    }
}
