using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    public class Repository<T> where T : class
    {
        private readonly DbContext _context;
        public T Entity { get; private set; }

        public Repository(DbContext context)
        {
            _context = context;
        }

        public T AddOrUpdate(Expression<Func<T, bool>> predicate, T entity)
        {
            if (_context.Set<T>().Any(predicate))
            {
                return _context.Set<T>().First(predicate);
            }
            _context.Entry(entity).State = EntityState.Added;
            _context.Set<T>().Add(entity);
            Entity = entity;

            return entity;
        }
    }
}