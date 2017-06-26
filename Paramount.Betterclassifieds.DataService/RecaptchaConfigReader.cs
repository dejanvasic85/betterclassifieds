using System.Configuration;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public static class RecaptchaConfigReader
    {
        public static RecaptchaConfig GetFromConfigSettings(string siteSectionName)
        {
            var config = ConfigurationManager.AppSettings.Get("Google.:" + siteSectionName);
            if (config == null)
            {
                throw new ConfigurationErrorsException($"Recaptcha for {siteSectionName} has not been configured.");
            }

            var keySecretCombo = config.Split(':');
            if (keySecretCombo.Length != 2)
            {
                throw new ConfigurationErrorsException($"Recaptcha for {siteSectionName} does not have a valid key and secret pair.");
            }

            return new RecaptchaConfig(
                key: keySecretCombo[0],
                secret: keySecretCombo[1]);
        }
    }
}