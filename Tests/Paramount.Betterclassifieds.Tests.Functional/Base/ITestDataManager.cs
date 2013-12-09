using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    /// <summary>
    /// Used directly by the tests to setup and assert scenarios
    /// </summary>
    public interface ITestDataManager : IDisposable
    {
        // Categories
        int AddOrUpdateCategory(string name, string parent);

        // Ads
        int AddOrUpdateOnlineAd(string adTitle, string categoryName, string subCategoryName);

        // Users
        ITestDataManager DropUserIfExists(string username);
        bool UserExists(string username);

        // Emails / Notifications
        List<Email> GetSentEmailsFor(string email);
    }
}