namespace Paramount.Betterclassifieds.Business.Events
{
    public enum EventBookingTicketValidationType
    {
        Valid,          // As per name
        ValidDuplicate, // Indicates that the ticket already has a validated flag
        NotValid,       // Indicates that the ticket does not exist for that event
    }
}