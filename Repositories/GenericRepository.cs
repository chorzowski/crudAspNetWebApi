using Microsoft.EntityFrameworkCore;
using NV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NV2
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private AdventureWorks2016Context db = null;
        // obiekt reprezentuje kolekcję wszystkich encji w danym kontekście
        // lub może być wynikiem zapytania z bazy danych
        DbSet<T> _objectSet;
        public GenericRepository(AdventureWorks2016Context _db)
        {
            db = _db;
            _objectSet = db.Set<T>();
        }

        public IQueryable<T> FindAll()
        {
            return db.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>()
                .Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            db.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }



    }
}

