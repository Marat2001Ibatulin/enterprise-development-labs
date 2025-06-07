using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.services
{
    public interface IRepository<TEntity, TKey>
    where TEntity : class
    {
        Task<IList<TEntity>> GetAll();
        Task<TEntity?> Get(TKey key);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(TKey key);
    }
}
