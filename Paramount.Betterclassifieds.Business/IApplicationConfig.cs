using System.IO;

namespace Paramount.Betterclassifieds.Business
{
    public interface IApplicationConfig
    {
        string DslImageUrlHandler { get; }
        bool UseHttps { get; }
        string ImageCacheDirectory { get; }
        DirectoryInfo ImageCropDirectory { get; }
        int MaxImageUploadBytes { get; }
        string[] AcceptedImageFileTypes { get; }
        bool IsPaymentEnabled { get; }
        string Brand { get; }
        string Environment { get; }
        string GoogleTimezoneApiUrl { get; }
        string GoogleTimezoneApiKey { get; }
        string StripeApiKey { get; }
        string StripePublishableKey { get; }
    }
}