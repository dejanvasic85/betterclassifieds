namespace Paramount.Betterclassifieds.Business.Managers
{
    public interface IApplicationConfig
    {
        string BaseUrl { get; }
        string DslImageUrlHandler { get; }
        string ClientCode { get; }
        string ConfigurationContext { get; }
        bool UseHttps { get; }
        string ImageCacheDirectory { get; }
        int MaxImageUploadBytes { get; }
        string[] AcceptedImageFileTypes { get; }
    }
}