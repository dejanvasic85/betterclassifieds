namespace Paramount.Betterclassifieds.Business.Events
{
    public enum EventBookingTicketValidationType
    {
        Success,        // As per name
        PartialSuccess, // Indicates that the ticket already has a validated flag
        Failed,         // Indicates that the ticket does not exist for that event
    }
}