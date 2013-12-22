using System;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Paramount.Betterclassifieds.Repository;

namespace Paramount.Betterclassifieds.DataLayer
{
    public abstract class EntityRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext, new()
        where TEntity : class
    {
        private readonly TContext _context = new TContext();

        protected TContext Context { get { return _context; } }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] properties)
        {
            var setOfT = _context.Set<TEntity>().AsQueryable();
            return properties.Aggregate(setOfT, (current, prop) => current.Include(prop).AsQueryable());
        }

        public virtual IQueryable<TEntity> FindItemsBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> FindItemsIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        {
            var items = _context.Set<TEntity>().Where(predicate);
            return properties.Aggregate(items, (current, prop) => current.Include(prop).AsQueryable());
        }

        public virtual void Add(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Attach(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public virtual void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEntityValidationException)
            {
                Debug.WriteLine(dbEntityValidationException); // Debug purposes only
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}