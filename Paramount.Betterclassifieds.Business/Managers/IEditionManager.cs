namespace Paramount.Betterclassifieds.Business.Managers
{
    using System;

    public interface IEditionManager
    {
        void RemoveEditionAndExtendBookings(DateTime edition);
    }
}
