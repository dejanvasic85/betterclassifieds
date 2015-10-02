namespace Paramount.Betterclassifieds.Business.Events
{
    public class RemainingTicketsWithRequestInfo
    {
        public RemainingTicketsWithRequestInfo(int ticketsRequested, int ticketsRemaining)
        {
            TicketsRequested = ticketsRequested;
            TicketsRemaining = ticketsRemaining;
        }

        public int TicketsRequested { get; private set; }
        public int TicketsRemaining { get; private set; }
    }
}