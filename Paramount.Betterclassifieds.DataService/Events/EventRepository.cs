using System.Data.Entity;
using System.Linq;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventRepository : IEventRepository, ICategoryAdRepository<ICategoryAd>
    {
        private readonly IDbContextFactory _dbContextFactory;

        public EventRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public EventModel GetEventDetails(int onlineAdId)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                return context.Events
                    .Include(e => e.Tickets)
                    .SingleOrDefault(e => e.OnlineAdId == onlineAdId);
            }
        }

        public void Add(ICategoryAd model)
        {
            using (var context = _dbContextFactory.CreateEventContext())
            {
                context.Events.Add(model as EventModel);
                context.SaveChanges();
            }
        }
    }
}
