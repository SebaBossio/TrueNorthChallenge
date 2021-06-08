using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.Contracts.Repositories.Generic;
using TrueNorthChallenge.DBEntities.Generic;

namespace TrueNorthChallenge.DAL.Repositories.Generic
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        internal TrueNorthContext _context = null;
        internal bool _asNoTracking = false;

        internal GenericRepository(TrueNorthContext context, bool asNoTracking = false)
        {
            _context = context;
            _asNoTracking = asNoTracking;
        }

        public virtual ICollection<T> GetAll()
        {
            IQueryable<T> q = _context.Set<T>();
            if (_asNoTracking)
            {
                q = q.AsNoTracking();
            }
            return q.ToList();
        }

        public virtual ICollection<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            if (_asNoTracking)
            {
                query = query.AsNoTracking();
            }
            return query.ToList();
        }
        public T GetSingle(Guid pk)
        {
            return _context.Set<T>().Find(pk);
        }
        public virtual ICollection<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> q = _context.Set<T>().Where(predicate);
            if (_asNoTracking)
            {
                q = q.AsNoTracking();
            }
            return q.ToList();
        }
        public virtual ICollection<T> FindByIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> q = _context.Set<T>().Where(predicate);
            foreach (var includeProperty in includeProperties)
            {
                q = q.Include(includeProperty);
            }
            if (_asNoTracking)
            {
                q = q.AsNoTracking();
            }
            return q.ToList();
        }

        public virtual void Add(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            (dbEntityEntry.Entity as IEntityBase).CTS = DateTime.Now;
            _context.Set<T>().Add(entity);
        }
        public virtual void Add(IEnumerable<T> entityEnumerable)
        {
            foreach (var entity in entityEnumerable)
            {
                Add(entity);
            }
        }

        public virtual void Edit(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            (dbEntityEntry.Entity as IEntityBase).MTS = DateTime.Now;
            if (dbEntityEntry.State != EntityState.Added)
            {
                dbEntityEntry.State = EntityState.Modified;
            }
        }
        public virtual void Edit(IEnumerable<T> entityEnumerable)
        {
            foreach (var entity in entityEnumerable)
            {
                Edit(entity);
            }
        }

        public virtual void Delete(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }
        public virtual void Delete(IEnumerable<T> entityEnumerable)
        {
            foreach (var entity in entityEnumerable)
            {
                var dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
        }

        public virtual void Detach(T entity)
        {
            var dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Detached;
        }
        public virtual void Detach(IEnumerable<T> entityEnumerable)
        {
            foreach (var entity in entityEnumerable)
            {
                var dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Detached;
            }
        }
    }
}
