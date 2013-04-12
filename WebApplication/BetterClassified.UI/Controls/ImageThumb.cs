using Paramount.Betterclassified.Utilities.Configuration;
using Paramount.Common.UI;
using Paramount.Utility;
using Paramount.DSL.UI;

namespace BetterClassified.UI
{
    public class ImageThumb : DslThumbImage
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