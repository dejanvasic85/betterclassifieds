using System;
using System.Linq;
using System.Linq.Expressions;

namespace Paramount.Betterclassifieds.Business.Repository
{
    /// <summary>
    /// Generic repository that is suitable for an entity in a domain driven design pattern
    /// </summary>
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] properties);
        IQueryable<T> FindItemsBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindItemsIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] properties);

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Attach(T entity);
        void Commit();
    }
}