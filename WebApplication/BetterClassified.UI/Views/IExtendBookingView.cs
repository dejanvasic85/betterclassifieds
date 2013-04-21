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
        
        void DataBindOptions(IEnumerable<int> insertions);
        void DataBindEditions(IEnumerable<PublicationEditionModel> editions, DateTime dateTime, decimal pricePerEdition, decimal totalPrice);
        void SetupOnlineOnlyView();
        void DisplayExpiredBookingMessage();
        void DisplayBookingDoesNotExist();
    }
}