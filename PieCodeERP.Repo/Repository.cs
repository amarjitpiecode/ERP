using Microsoft.EntityFrameworkCore;
using PieCodeERP.Repo.Interface;
using System.Linq.Expressions;

namespace PieCodeERP.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ERPContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ERPContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IList<T> GetAll()
        {
            return entities.ToList();
        }

         
        public IQueryable<T> GetAllAsQuerable()
        {
            return entities.AsQueryable();
        }

        public T GetByPredicate(Func<T, bool> predicate)
        {
            return entities.FirstOrDefault(predicate);
        }
        public T GetByPredicateWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = entities;
            if (includes != null)
            {
                foreach (Expression<Func<T, object>> include in includes)
                    query = query.Include(include);
            }
            return query.FirstOrDefault(predicate);
        }
        public IList<T> GetListByPredicate(Func<T, bool> predicate)
        {
            return entities.Where(predicate).ToList();
        }

        public IList<T> GetListByPredicateWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = entities;
            if (includes != null)
            {
                foreach (Expression<Func<T, object>> include in includes)
                    query = query.Include(include);
            }
            return query.Where(predicate).ToList();
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}
