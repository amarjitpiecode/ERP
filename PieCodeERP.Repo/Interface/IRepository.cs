using System.Linq.Expressions;

namespace PieCodeERP.Repo.Interface
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        IQueryable<T> GetAllAsQuerable();
        T GetByPredicate(Func<T, bool> predicate);
        T GetByPredicateWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);
        IList<T> GetListByPredicate(Func<T, bool> predicate);
        IList<T> GetListByPredicateWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
