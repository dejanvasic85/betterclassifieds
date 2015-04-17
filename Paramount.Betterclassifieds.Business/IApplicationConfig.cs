using System.IO;

namespace Paramount.Betterclassifieds.Business
{
    public interface IApplicationConfig
    {
        string BaseUrl { get; }
        string DslImageUrlHandler { get; }
        bool UseHttps { get; }
        string ImageCacheDirectory { get; }
        DirectoryInfo ImageCropDirectory { get; }
        int MaxImageUploadBytes { get; }
        string[] AcceptedImageFileTypes { get; }
        bool IsPaymentEnabled { get; }
        string Brand { get; }
    }
}