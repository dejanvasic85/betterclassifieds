using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    /// <summary>
    /// Used directly by the tests to setup and assert scenarios
    /// </summary>
    public interface ITestDataManager : IDisposable
    {
        // Categories
        int AddCategoryIfNotExists(string subCategory, string parentCategory, params string[] addToPublications);
        int? GetCategoryIdForTitle(string categoryName);

        // Publications
        int AddPublicationIfNotExists(string publicationName, int publicationTypeId = 1, string frequency = Constants.FrequencyType.Weekly, int frequencyValue = 3);
        int AddOnlinePublicationIfNotExists();
        int AddPublicationAdTypeIfNotExists(string publicationName, string adTypeCode);
        void AddEditionsToPublication(string publicationName, int numberOfEditions);
        
        // Ads
        int DropAndAddOnlineAd(string adTitle, string categoryName, string subCategoryName);
        void DropOnlineAdIfExists(string adTitle);

        // Users
        void DropUserIfExists(string username);
        bool UserExists(string username);
        Guid? AddUserIfNotExists(string username, string password, string email);

        // Emails / Notifications
        List<Email> GetSentEmailsFor(string email);
        
    }
}