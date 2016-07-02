﻿using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Tests.Functional.Features.Events;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    using Mocks;

    /// <summary>
    /// Used directly by the tests to setup and assert scenarios
    /// </summary>
    internal interface ITestDataRepository
    {
        // Categories
        int AddCategoryIfNotExists(string subCategory, string parentCategory, string categoryAdType = "");
        int? GetCategoryIdForTitle(string categoryName);

        // Publications
        int AddPublicationIfNotExists(string publicationName, string publicationType = Constants.PublicationType.Newspaper, string frequency = Constants.FrequencyType.Weekly, int? frequencyValue = 3);
        int AddPublicationAdTypeIfNotExists(string publicationName, string adTypeCode);
        void AddEditionsToPublication(string publicationName, int numberOfEditions);

        // Ads
        AdBookingContext GetAdBookingContextByReference(string bookReference);
        int GetOnlineAdForBookingId(int adId);
        int DropCreateOnlineAd(string adTitle, string categoryName, string subCategoryName, string username);
        void DropOnlineAdIfExists(string adTitle);

        // Users
        void DropUserIfExists(string username);
        bool RegistrationExistsForEmail(string email);
        Guid? AddUserIfNotExists(string username, string password, string email, RoleType roleType);
        void DropUserNetwork(string userId, string userNetworkEmail);
        int AddUserNetworkIfNotExists(string username, string email, string fullName);

        // Emails / Notifications
        List<Email> GetSentEmailsFor(string email);

        // Location Area
        void AddLocationIfNotExists(string parentLocation, params string[] areas);

        // Rates
        void AddOnlineRateForCategoryIfNotExists(decimal price, string categoryName);
        void AddPrintRateForCategoryIfNotExists(string categoryName);

        // Events
        int AddEventIfNotExists(int adBookingId);
        void AddEventTicketType(int eventId, string ticketName, decimal price, int availableQuantity);
        void SetEventIncludeTransactionFee(int eventId, bool include);
        EventBookingData GetEventBooking(int eventId);
        List<EventBookingTicketData> GetPurchasedTickets(int eventBookingId);
        List<EventBookingTicketData> GetPurchasedTicketsForEvent(int eventId);
        int AddEventInvitationIfNotExists(int eventId, int userNetworkId);
        void AddEventGroup(int eventId, string groupName, string ticketName, int maxGuests);

        // Address
        int AddAddress(object address);
        
    }
}