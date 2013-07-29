using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterClassified.UI.Presenters
{
    using Views;
    using Models;
    using Repository;

    public class UserBookingsPresenter : Controller<IMyBookingsView>
    {
        private readonly IBookingRepository BookingRepository;

        public UserBookingsPresenter(IMyBookingsView view, IBookingRepository bookingRepository)
            : base(view)
        {
            this.BookingRepository = bookingRepository;
        }

        public void Load()
        {
            if (View.IsExtensionComplete)
            {
                View.DisplayExtensionCompleteAlert();
            }

            LoadBookings(UserBookingViewType.Current);
        }

        public void CancelBooking(int adBookingId)
        {
            this.BookingRepository.CancelAndExpireBooking(adBookingId);
            this.View.DisplayBookingCancelledAlert();
            Load();
        }

        public void ChangeView(UserBookingViewType viewType)
        {
            View.HideAlerts();
            View.SelectedViewType = viewType;
            LoadBookings(viewType);
        }

        public void LoadBookings(UserBookingViewType viewType)
        {
            List<UserBookingModel> bookings = BookingRepository.GetBookingsForUser(View.LoggedInUserName)
                .OrderByDescending(b => b.AdBookingId)
                .ToList();

            if (viewType == UserBookingViewType.Current)
                bookings = bookings.Where(bk => bk.EndDate > DateTime.Today && bk.StartDate <= DateTime.Today).ToList();

            if (viewType == UserBookingViewType.Expired)
                bookings = bookings.Where(bk => bk.EndDate < DateTime.Today).ToList();

            if (viewType == UserBookingViewType.Scheduled)
                bookings = bookings.Where(bk => bk.StartDate > DateTime.Today).ToList();

            this.View.DisplayBookings(bookings);

            if (bookings.Any(b => b.AboutToExpire))
            {
                this.View.DisplayExpiringBookingsWarning();
            }
        }
    }
}