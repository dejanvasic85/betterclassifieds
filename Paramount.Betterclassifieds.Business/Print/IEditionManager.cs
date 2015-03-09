using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Print
{
    using System;

    public interface IEditionManager
    {
        void RemoveEditionAndExtendBookings(DateTime edition);

        List<DateTime> GetUpcomingEditions(params int[] publications);

        IEnumerable<int> GetAvailableInsertions();
    }
}
