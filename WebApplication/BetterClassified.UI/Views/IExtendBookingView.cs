namespace BetterClassified.UI.Views
{
    using System.Collections.Generic;
    using Models;

    public interface IExtendBookingView : IBaseView
    {
        int AdBookingId { get; }
        
        void DataBindInsertionList(IEnumerable<int> insertions);
        void DataBindEditions(IEnumerable<PublicationEditionModel> editions);
    }
}