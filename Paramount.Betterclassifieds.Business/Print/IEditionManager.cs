namespace Paramount.Betterclassifieds.Business.Print
{
    using System;

    public interface IEditionManager
    {
        void RemoveEditionAndExtendBookings(DateTime edition);
    }
}
