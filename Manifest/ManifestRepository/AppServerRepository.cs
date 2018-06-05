using ManifestDbContext;
using ManifestModels;

namespace ManifestRepository
{
    public class AppServerRepository : Repository<AppServer>, IAppServerRepository
    {
        private ManifestUnitOfWork manifestUnitOfWork;

        public AppServerRepository(ManifestUnitOfWork manifestUnitofWork) : base(manifestUnitofWork)
        {
            this.manifestUnitOfWork = manifestUnitofWork;
        }

        public void AddAppServer(AppServer appServer)
        {
            this.manifestUnitOfWork.AddAppServer(appServer);
        }
    }
}
