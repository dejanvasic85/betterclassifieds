using System;
using System.Collections.Generic;
using Paramount.DomainModel.Business.OnlineClassies.Models;

namespace Paramount.DomainModel.Business.Repositories
{
    public interface IBookingRepository
    {
        IAdBookingModel GetBooking(int id, bool withLineAd = false);
        List<IBookEntryModel> GetBookEntriesForBooking(int adBookingId);
        List<IUserBookingModel> GetBookingsForUser(string username);
        int AddBookingExtension(IAdBookingExtensionModel extension);
        IAdBookingExtensionModel GetBookingExtension(int extensionId);
        void UpdateExtesion(int extensionId, int? status);
        void UpdateBooking(int adBookingId, DateTime? newEndDate = null, decimal? totalPrice = null);
        void AddBookEntries(IBookEntryModel[] bookEntries);
        void CancelAndExpireBooking(int adBookingId);
    }
}
