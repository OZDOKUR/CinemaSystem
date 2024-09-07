using Core.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.Repositories
{
    public class GenericRepository<T> : IEntityRepository<T> where T : class
    {
        protected readonly Context _context = new Context();
        DbSet<T> _object;
        
        public GenericRepository()
        {
            _object = _context.Set<T>();
        }
        public void Add(T entity)
        {
            var addEntity = _context.Entry(entity);
            addEntity.State = EntityState.Added;
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            var deleteEntity = _context.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public T GetById(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public T GetById(int id)
        {
            return _object.Find(id);
        }

        public List<T> List()
        {

            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter);
        }

        public void Update(T entity)
        {
            var updateEntity = _context.Entry(entity);
            updateEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

}
