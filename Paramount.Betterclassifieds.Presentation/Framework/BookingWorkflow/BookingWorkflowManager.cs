namespace Paramount.Betterclassifieds.Presentation
{
    public interface IBookingStep
    {
        string ViewName { get; }
        string ActionName { get; }
    }

    public class ConfirmationStep : IBookingStep
    {
        public string ViewName => "Step3";
        public string ActionName => ViewName;
    }

    public class CategorySelectionStep : IBookingStep
    {
        public string ViewName => "Step1";
        public string ActionName => ViewName;
    }

    public class DesignOnlineAdStep : IBookingStep
    {
        public string ViewName => "Step2";
        public string ActionName => ViewName;
    }

    public class DesignEventStep : IBookingStep
    {
        public string ViewName => "Step2_Event";
        public string ActionName => "Step2";
    }

    public class DesignEventTicketingStep : IBookingStep
    {
        public string ViewName => "Step2_EventTickets";
        public string ActionName => "EventTickets";
    }

    public class SuccessStep : IBookingStep
    {
        public string ViewName => "Success";
        public string ActionName => ViewName;
    }
}