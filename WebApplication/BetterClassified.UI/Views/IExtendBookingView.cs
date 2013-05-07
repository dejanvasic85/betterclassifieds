﻿using System;

namespace BetterClassified.UI.Views
{
    using System.Collections.Generic;
    using Models;

    public interface IExtendBookingView : IBaseView
    {
        int AdBookingId { get; }
        int SelectedInsertionCount { get; }
        decimal TotalPrice { get; set; }
        bool IsPaymentRequired { get; }
        bool IsOnlineOnly { get; set; }
        
        void DataBindOptions(IEnumerable<int> insertions);
        void DataBindEditions(IEnumerable<PublicationEditionModel> editions, DateTime dateTime, decimal pricePerEdition, decimal totalPrice);
        void SetupOnlineOnlyView();
        void DisplayBookingDoesNotExist();
        void NavigateToPayment(int extensionId);
        void NavigateToBookings(bool successful);
    }
}