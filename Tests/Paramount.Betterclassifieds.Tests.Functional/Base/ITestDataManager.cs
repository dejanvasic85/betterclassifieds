﻿using System;
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
        int AddPublicationIfNotExists(string publicationName, int publicationTypeId = 1, string frequency = Constants.FrequencyType.Weekly, int frequencyValue = 3);
        int AddOnlinePublicationIfNotExists();
        void AddEditionsToPublication(string publicationName, int numberOfEditions);

        // Ads
        int DropAndAddOnlineAd(string adTitle, string categoryName, string subCategoryName);
        void DropOnlineAdIfExists(string adTitle);
        void AddAdTypeIfNotExists(string adTypeCode);

        // Users
        void DropUserIfExists(string username);
        bool UserExists(string username);
        string AddUserIfNotExists(string username, string password, string email);

        // Emails / Notifications
        List<Email> GetSentEmailsFor(string email);
        
    }
}