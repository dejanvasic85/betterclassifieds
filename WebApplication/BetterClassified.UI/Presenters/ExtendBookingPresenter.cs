namespace BetterClassified.UI.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Views;

    public class ExtendBookingPresenter : Controller<IExtendBookingView>
    {
        private readonly Repository.IBookingRepository bookingRepository;
        private readonly Repository.IConfigSettings configSettings;
        private readonly ExtensionManager extensionManager;

        public ExtendBookingPresenter(IExtendBookingView view, Repository.IBookingRepository bookingRepository, ExtensionManager extensionManager, Repository.IConfigSettings configSettings)
            : base(view)
        {
            this.bookingRepository = bookingRepository;
            this.extensionManager = extensionManager;
            this.configSettings = configSettings;
        }

        public void Load()
        {
            // Fetch the original booking
            AdBookingModel booking = bookingRepository.GetBooking(View.AdBookingId);

            // Ensure booking exists and belongs to the user
            if (booking == null)
            {
                View.DisplayBookingDoesNotExist();
                return;
            }

            // Ensure that the booking belongs to the logged in user!
            if (!View.LoggedInUserName.Equals(booking.UserId))
            {
                View.DisplayBookingDoesNotExist();
                return;
            }

            // Ensure the booking has not already expired
            if (booking.IsExpired)
            {
                // Get out and display a message that the booking has expired
                View.DisplayExpiredBookingMessage();
                return;
            }

            // Load the screen as per usual (depending on the type of booking)
            View.DataBindOptions(booking.BookingType == BookingType.Bundled
                ? Enumerable.Range(1, configSettings.RestrictedEditionCount)
                : Enumerable.Range(1, configSettings.RestrictedOnlineDaysCount));

            // Load for a single edition first
            Load(insertions: 1, booking: booking);
        }

        public void Load(int insertions, AdBookingModel booking = null)
        {
            // Fetch the original booking
            if (booking == null)
                booking = bookingRepository.GetBooking(View.AdBookingId);

            // Check whether the booking is online only
            if (booking.BookingType == BookingType.Bundled)
            {
                // Fetch and display the list of editions
                List<PublicationEditionModel> editions = extensionManager.ExtensionDates(View.AdBookingId, insertions).ToList();

                // Fetch the online end date
                DateTime onlineAdEndDate = editions
                    .SelectMany(e => e.Editions)
                    .OrderBy(date => date.EditionDate)
                    .Last()
                    .EditionDate
                    .AddDays(configSettings.NumberOfDaysAfterLastEdition);

                // Fetch price per edition
                decimal pricePerEdition = editions.Sum(s => s.Editions.First().EditionAdPrice);
                decimal totalPrice = pricePerEdition * insertions;
                View.IsOnlineOnly = false;
                View.DataBindEditions(editions, onlineAdEndDate, pricePerEdition, totalPrice);
            }
            else
            {
                View.SetupOnlineOnlyView();
                View.IsOnlineOnly = true;
                View.DataBindEditions(null, booking.EndDate.AddDays(insertions), booking.TotalPrice, booking.TotalPrice);
            }
        }

        public void ProcessExtension()
        {
            var extension = extensionManager.CreateExtension(View.AdBookingId,
                    View.SelectedInsertionCount,
                    View.LoggedInUserName,
                    View.TotalPrice,
                    View.IsPaymentRequired ? ExtensionStatus.Pending : ExtensionStatus.Complete,
                    View.IsOnlineOnly);

            if (View.IsPaymentRequired)
            {
                // Save the extension id for completing later and redirect to the payment processing views
                View.NavigateToPayment(extension.AdBookingExtensionId);
            }
            else
            {
                // Extend the booking details
                extensionManager.Extend(extension);
                View.NavigateToBookings(true);
            }
        }
    }
}
