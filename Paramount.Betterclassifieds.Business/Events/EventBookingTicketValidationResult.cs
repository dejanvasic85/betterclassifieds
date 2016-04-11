namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicketValidationResult
    {
        public EventBookingTicketValidationType ValidationType { get; private set; }
        public string ValidationMessage { get; private set; }

        public EventBookingTicketValidationResult(EventBookingTicketValidationType validationType, string validationMessage)
        {
            ValidationType = validationType;
            ValidationMessage = validationMessage;
        }

        public static EventBookingTicketValidationResult Success()
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.Success, "VALID");
        }

        public static EventBookingTicketValidationResult PartialSuccess()
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.PartialSuccess, 
                "VALID: This ticket has already been validated!");
        }

        public static EventBookingTicketValidationResult BadData()
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.Failed,
                    "NOT VALID: Unknown barcode information.");
        }

        public static EventBookingTicketValidationResult NoSuchEvent(int eventId)
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.Failed,
                $"NO SUCH EVENT: {eventId}");
        }

        public static EventBookingTicketValidationResult NoSuchTicket(int eventId, int ticketId)
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.Failed,
                $"NO SUCH TICKET: Event [{eventId}] Ticket [{ticketId}]");
        }

        public static EventBookingTicketValidationResult NoSuchTicket(int eventId, int ticketId, int ticketBookingId)
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.Failed,
                $"NO SUCH TICKET BOOKING: Event [{eventId}] Ticket [{ticketId}] Ticket Booking [{ticketBookingId}]");
        }
    }
}