using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace iClassic.Services.Base
{
    public interface IGenericRepository<T> where T : class
    {
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save();
    }
}