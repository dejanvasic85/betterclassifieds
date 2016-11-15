using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using Paramount.Betterclassifieds.Tests.Functional.Mocks.Models;

namespace Paramount.Betterclassifieds.Tests.Functional.Base
{
    /// <summary>
    /// Used directly by the tests to setup and assert scenarios
    /// </summary>
    internal interface ITestDataRepository
    {
        // Client Config
        void SetClientConfig(string settingName, string settingValue);

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
        void DropUserIfExists(string username, string email);
        bool RegistrationExistsForEmail(string email);
        Guid? DropCreateUser(string username, string password, string email, RoleType roleType);
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
        EventBookingData GetEventBooking(int eventId);
        List<EventBookingTicketData> GetPurchasedTickets(int eventBookingId);
        List<EventBookingTicketData> GetPurchasedTicketsForEvent(int eventId);
        string GetEventBookingStatus(int eventId, string guestEmail);
        bool GetEventBookingTicketStatus(int eventId, string guestEmail);
        int AddEventIfNotExists(int adBookingId);
        void AddEventTicketType(int eventId, string ticketName, decimal price, int availableQuantity);
        int AddEventInvitationIfNotExists(int eventId, int userNetworkId);
        void AddEventGroup(int eventId, string groupName, string ticketName, int maxGuests);
        int AddGuestToEvent(string username, string guestFullName, string guestEmail, string ticketName, int eventId);
        void SetEventIncludeTransactionFee(int eventId, bool include);
        void SetEventGroupsRequired(int eventId);

        // Address
        int AddAddress(object address);

    }
}