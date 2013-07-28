using System.Collections.Generic;

namespace BetterClassified.UI.Views
{
    using Models;

    public interface IMyBookingsView : IBaseView
    {
        void DisplayBookings(List<UserBookingModel> bookings);
        void DisplayExpiringBookingsWarning();
        void DisplayBookingCancelledAlert();
        void DisplayExtensionCompleteAlert();
        void HideAlerts();

        bool IsExtensionComplete { get; }
        UserBookingViewType SelectedViewType { get; set; }
    }
}