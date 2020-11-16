using System.Collections.Generic;


namespace OnlineShopping.Data.Repositories
{
    interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(params object[] id);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteAll(IEnumerable<TEntity> entities);
    }
}