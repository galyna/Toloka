using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Core.Data.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> SaveOrUpdateAll(params T[] entities);
        T SaveOrUpdate(T entity);
        T Delete(T entity);
    }
}
