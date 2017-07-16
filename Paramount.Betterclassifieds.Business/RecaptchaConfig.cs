namespace Paramount.Betterclassifieds.Business
{
    public class RecaptchaConfig
    {
        public RecaptchaConfig(string key, string secret)
        {
            Key = key;
            Secret = secret;
        }
        public string Key { get; }
        public string Secret { get; }
    }
}