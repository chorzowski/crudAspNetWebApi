using System;
using System.Linq;
using System.Linq.Expressions;

namespace NV2
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Delete(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Update(T entity);
    }
}