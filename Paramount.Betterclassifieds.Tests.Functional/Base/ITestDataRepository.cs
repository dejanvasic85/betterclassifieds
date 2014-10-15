using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    using Mocks;

    /// <summary>
    /// Used directly by the tests to setup and assert scenarios
    /// </summary>
    public interface ITestDataRepository : IDisposable
    {
        // Categories
        int AddCategoryIfNotExists(string subCategory, string parentCategory);
        int? GetCategoryIdForTitle(string categoryName);

        // Publications
        int AddPublicationIfNotExists(string publicationName, string publicationType = Constants.PublicationType.Newspaper, string frequency = Constants.FrequencyType.Weekly, int? frequencyValue = 3);
        int AddPublicationAdTypeIfNotExists(string publicationName, string adTypeCode);
        void AddEditionsToPublication(string publicationName, int numberOfEditions);
        
        // Ads
        int DropCreateOnlineAd(string adTitle, string categoryName, string subCategoryName);
        void DropOnlineAdIfExists(string adTitle);

        // Users
        void DropUserIfExists(string username);
        bool RegistrationExistsForEmail(string email);
        Guid? AddUserIfNotExists(string username, string password, string email, RoleType roleType);

        // Emails / Notifications
        List<Email> GetSentEmailsFor(string email);
        
        // Location Area
        void AddLocationIfNotExists(string parentLocation, params string[] areas);
    }
}