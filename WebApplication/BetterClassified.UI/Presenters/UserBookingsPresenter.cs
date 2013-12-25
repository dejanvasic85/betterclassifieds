using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Repository;

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

            LoadBookings(View.SelectedViewType);
            View.SetViewType(View.SelectedViewType);
        }

        public void CancelBooking(int adBookingId)
        {
            this.BookingRepository.CancelAndExpireBooking(adBookingId);
            this.View.DisplayBookingCancelledAlert();
            Load();
        }

        public void LoadBookings(UserBookingViewType viewType)
        {
            IEnumerable<UserBookingModel> bookings = BookingRepository
                .GetBookingsForUser(View.LoggedInUserName)
                .Where(b => b.StartDate > DateTime.Today.AddMonths(-12))
                .OrderByDescending(b => b.AdBookingId);

            if (viewType == UserBookingViewType.Current)
                bookings = bookings.Where(bk => bk.EndDate > DateTime.Today && bk.StartDate <= DateTime.Today).ToList();

            if (viewType == UserBookingViewType.Expired)
                bookings = bookings.Where(bk => bk.EndDate < DateTime.Today).ToList();

            if (viewType == UserBookingViewType.Scheduled)
                bookings = bookings.Where(bk => bk.StartDate > DateTime.Today).ToList();

            this.View.DisplayBookings(bookings.ToList());

            if (bookings.Any(b => b.AboutToExpire))
            {
                this.View.DisplayExpiringBookingsWarning();
            }
        }
    }
}