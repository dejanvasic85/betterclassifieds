using System.Collections.Generic;
using Paramount.Betterclassifieds.Business;

namespace BetterClassified.UI.Views
{
    public interface IMyBookingsView : IBaseView
    {
        void DisplayBookings(List<UserBookingModel> bookings);
        void DisplayExpiringBookingsWarning();
        void DisplayBookingCancelledAlert();
        void DisplayExtensionCompleteAlert();
        void HideAlerts();

        bool IsExtensionComplete { get; }
        UserBookingViewType SelectedViewType { get; }
        void SetViewType(UserBookingViewType selectedViewType);
    }
}