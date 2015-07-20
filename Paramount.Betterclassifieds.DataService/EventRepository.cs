using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Paramount.Betterclassifieds.DataService
{
    public class EventRepository : IRepository<EventModel>
    {
        public IQueryable<EventModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<EventModel> GetAllIncluding(params Expression<Func<EventModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EventModel> FindItemsBy(Expression<Func<EventModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EventModel> FindItemsIncluding(Expression<Func<EventModel, bool>> predicate, params Expression<Func<EventModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public void Add(EventModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(EventModel entity)
        {
            throw new NotImplementedException();
        }

        public void Update(EventModel entity)
        {
            throw new NotImplementedException();
        }

        public void Attach(EventModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
