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

        public static EventBookingTicketValidationResult Valid()
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.Valid, "VALID");
        }

        public static EventBookingTicketValidationResult ValidDuplicate()
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.ValidDuplicate, 
                "VALID: This ticket has already been validated!");
        }

        public static EventBookingTicketValidationResult BadData()
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.NotValid,
                    "NOT VALID: Unknown barcode information.");
        }

        public static EventBookingTicketValidationResult NoSuchEvent(int eventId)
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.NotValid,
                $"NO SUCH EVENT: {eventId}");
        }

        public static EventBookingTicketValidationResult NoSuchTicket(int eventId, int ticketId)
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.NotValid,
                $"NO SUCH TICKET: Event [{eventId}] Ticket [{ticketId}]");
        }

        public static EventBookingTicketValidationResult NoSuchTicket(int eventId, int ticketId, int ticketBookingId)
        {
            return new EventBookingTicketValidationResult(EventBookingTicketValidationType.NotValid,
                $"NO SUCH TICKET BOOKING: Event [{eventId}] Ticket [{ticketId}] Ticket Booking [{ticketBookingId}]");
        }
    }
}