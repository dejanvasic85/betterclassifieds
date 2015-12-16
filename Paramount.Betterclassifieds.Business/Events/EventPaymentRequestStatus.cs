namespace Paramount.Betterclassifieds.Business.Events
{
    public enum EventPaymentRequestStatus
    {   
        NotAvailable,       // If no tickets or all tickets are free
        CloseEventFirst,    
        RequestPending,     // The event is closed but no request was made for payment
        PaymentPending,
        Complete
    }
}