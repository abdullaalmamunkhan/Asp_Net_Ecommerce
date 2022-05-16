using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer.GenericRepo
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression);
        IQueryable<T> GetInclude(Expression<Func<T, bool>> expression);
        Task<T> Details(Expression<Func<T, bool>> predicate);
        Task<bool> IsExist(Expression<Func<T, bool>> expression);
        Task DeleteRange(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task Delete(T entity);
        Task Edit(T entity, List<object> avoidedProperties);
        Task<int> SaveChanges();
        void Dispose();
        T DetailSync(Expression<Func<T, bool>> predicate);
        void EditSync(T entity, List<object> avoidedProperties);
        void SaveChangesSync();
        void Update(T entity);
    }
}
