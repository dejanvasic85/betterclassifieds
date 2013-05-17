
using Paramount.ApplicationBlock.Configuration;

namespace BetterClassified.UI.Repository
{
    public interface IConfigSettings
    {
        int RestrictedEditionCount { get; }
        int RestrictedOnlineDaysCount { get; }
        int NumberOfDaysAfterLastEdition { get; }
        bool IsOnlineAdFree { get; }
        string BaseUrl { get; }
    }

    public class ConfigSettings : IConfigSettings
    {
        // Todo - read from database
        public int RestrictedEditionCount { get { return 10; } }
        public int RestrictedOnlineDaysCount { get { return 30; } }
        public int NumberOfDaysAfterLastEdition { get { return 6; } }
        public bool IsOnlineAdFree { get { return true; } }
        public string BaseUrl
        {
            get { return ConfigManager.ReadAppSetting<string>("BaseUrl"); }
        }
    }
}