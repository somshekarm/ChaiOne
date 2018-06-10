using ManifestDbContext;
using ManifestModels;
using System.Collections.Generic;
using System;

namespace ManifestRepository
{
    public class AppServerRepository : Repository<AppServer>, IAppServerRepository
    {
        private ManifestUnitOfWork manifestUnitOfWork;

        public AppServerRepository(ManifestUnitOfWork manifestUnitofWork) : base(manifestUnitofWork)
        {
            this.manifestUnitOfWork = manifestUnitofWork;
        }

        public void AddAppServers(List<AppServer> appServers)
        {
            this.manifestUnitOfWork.AddAppServers(appServers);
        }

        public List<AppServer> GetAppServer()
        {
            return manifestUnitOfWork.GetAppServers();
        }
    }
}
