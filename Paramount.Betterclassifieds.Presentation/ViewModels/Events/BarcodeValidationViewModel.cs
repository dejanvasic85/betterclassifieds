using Humanizer;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class BarcodeValidationViewModel
    {
        public static BarcodeValidationViewModel FromResult(EventBookingTicketValidationResult validationResult)
        {
            var alertType = "";
            switch (validationResult.ValidationType)
            {
                case EventBookingTicketValidationType.Success:
                    alertType = "success";
                    break;

                case EventBookingTicketValidationType.PartialSuccess:
                    alertType = "warning";
                    break;

                case EventBookingTicketValidationType.Failed:
                    alertType = "danger";
                    break;
            }

            return new BarcodeValidationViewModel
            {
                StatusMessage = validationResult.ValidationMessage,
                StatusResult = validationResult.ValidationType.Humanize(),
                AlertType = alertType
            };
        }

        public string StatusResult { get; set; }
        public string StatusMessage { get; set; }
        public string AlertType { get; set; }
    }
}