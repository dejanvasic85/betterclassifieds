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
        int AddCategoryIfNotExists(string name, string parent);
        int? GetCategoryIdForTitle(string categoryName);

        // Publications
        void AddPublicationIfNotExists(string publicationName);

        // Ads
        int DropAndAddOnlineAd(string adTitle, string categoryName, string subCategoryName);
        void DropOnlineAdIfExists(string adTitle);
        void AddAdTypeIfNotExists(string lineAdCode);

        // Users
        ITestDataManager DropUserIfExists(string username);
        bool UserExists(string username);

        // Emails / Notifications
        List<Email> GetSentEmailsFor(string email);


    }
}