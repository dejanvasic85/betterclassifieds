namespace Paramount.Betterclassifieds.Business
{
    using System;

    // Todo - these should actually all have a get and set, and have an associated audiatable object
    public interface IClientConfig
    {
        int RestrictedEditionCount { get; }
        int RestrictedOnlineDaysCount { get; }
        int NumberOfDaysAfterLastEdition { get; }
        string FacebookAppId { get; }
        int SearchResultsPerPage { get; }
        int SearchMaxPagedRequests { get; }
        Address ClientAddress { get; }
        Tuple<string, string> ClientAddressLatLong { get; }
        string ClientPhoneNumber { get; }
        string[] SupportEmailList { get; }
        int? MaxOnlineImages { get; }
        string PublisherHomeUrl { get; }
        bool EnableRegistrationEmailVerification { get; }
        int PrintImagePixelsWidth { get; }
        int PrintImagePixelsHeight { get; }
        int PrintImageResolution { get; }
        string ClientName { get; }
        int EventTicketReservationExpiryMinutes { get; }
        int EventMaxTicketsPerBooking { get; }
        decimal EventTicketFeePercentage { get; }        
        decimal EventTicketFeeCents { get; }
        bool IsPrintEnabled { get; }
        bool EnablePayPalPayments { get; }
        bool EnableCreditCardPayments { get; }
        string EmailFromAddress { get; }
        string EmailDomain { get; }
    }
}