using Paramount.Betterclassified.Utilities.Configuration;
using Paramount.Utility;

namespace BetterClassified.UI
{
    public class ImageThumbCommand : DslThumbCommand
    {
        public override string GetEntityCode()
        {
            return CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode);
        }

        public override string GetDslHandlerUrl()
        {
            return BetterclassifiedSetting.DslImageUrlHandler;
        }

        public override int GetThumbWidth()
        {
            return BetterclassifiedSetting.DslThumbWidth;
        }

        public override int GetThumbHeight()
        {
            return BetterclassifiedSetting.DslThumbHeight;
        }

        public override int GetResolution()
        {
            return BetterclassifiedSetting.DslDefaultResolution;
        }
    }
}