using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TrueNorthChallenge.Contracts.Repositories.Generic
{
    public interface IGenericRepository<T>
    {
        ICollection<T> GetAll();
        ICollection<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T GetSingle(Guid pk);
        ICollection<T> FindBy(Expression<Func<T, bool>> predicate);
        ICollection<T> FindByIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void Add(IEnumerable<T> entityEnumerable);
        void Edit(T entity);
        void Edit(IEnumerable<T> entityEnumerable);
        void Delete(T entity);
        void Delete(IEnumerable<T> entityEnumerable);
        void Detach(T entity);
        void Detach(IEnumerable<T> entityEnumerable);
    }
}
