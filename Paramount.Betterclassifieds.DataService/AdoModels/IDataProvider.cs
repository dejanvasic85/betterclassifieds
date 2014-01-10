using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Paramount.Betterclassifieds.DataService
{
    public interface IDataProvider<T> where T : class, IDataProvider 
    {
        string ConnectionString { get;}
        IEnumerable<T> AsQueryable();
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> where);
        T Single(Expression<Func<T, bool>> where);
        T First(Expression<Func<T, bool>> where);
        void Delete(T entity); void Add(T entity);
        void Update(T entity);
        string ClientCode { get; }
        void Commit();
    }

    public interface IDataProvider:IDisposable
    {
        void Commit();
        string ConnectionString { get; }
        string ClientCode { get; }
        string ConfigSection { get; }
    }
}