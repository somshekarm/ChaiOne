using ManifestDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ManifestUnitOfWork manifestUnitOfWork;
        public Repository(ManifestUnitOfWork dbContext)
        {
            this.manifestUnitOfWork = dbContext;
        }
        public void Add(TEntity entity)
        {
            manifestUnitOfWork.Set<TEntity>().Add(entity);
        }

        public void Find(Func<TEntity, bool> predicate)
        {
            manifestUnitOfWork.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(Guid id)
        {
            return manifestUnitOfWork.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return manifestUnitOfWork.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            manifestUnitOfWork.Set<TEntity>().Remove(entity);
        }
    }
}
