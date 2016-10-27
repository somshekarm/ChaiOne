using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Find(Func<TEntity, bool> predicate);
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();
    }
}
