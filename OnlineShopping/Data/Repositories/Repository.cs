using OnlineShopping.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;


namespace OnlineShopping.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<TEntity> _set;

        public Repository()
        {
            _context = new ApplicationDbContext();
            _set = _context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _set.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _set.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteAll(IEnumerable<TEntity> entities)
        {
            _set.RemoveRange(entities);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _set;
        }

        public TEntity GetById(params object[] id)
        {
            return _set.Find(id);
        }

        public void Update(TEntity entity)
        {
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}