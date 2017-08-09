using iClassic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace iClassic.Services.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private iClassicEntities _entities;
        public GenericRepository(iClassicEntities entities)
        {
            this._entities = entities;
        }
        public void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            var obj = GetAll().FirstOrDefault(predicate);
            return obj;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public void Insert(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public void Save()
        {
            _entities.SaveChanges();
        }

        public void Update(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}