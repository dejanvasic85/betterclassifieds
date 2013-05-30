namespace BetterClassified.UI.Models
{
    public enum BookingStatusType
    {
        Booked = 1,
        Expired = 2,
        Cancelled = 3,
        Unpaid = 4
    }

    public enum PaymentType
    {
        None = 0,
        CreditCard = 1,
        PayPal = 2
    }
}